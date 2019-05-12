using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using StoreSystem.Data.DbContext;
using StoreSystem.Data.Models;
using StoreSystem.Services;
using StoreSystem.Services.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreSystem.Tests.Services.SaleServiceTests
{
    [TestClass]
    public class GetOfferByID_Should
    {
        [TestMethod]
        public async Task FindProperSale_WhenValidSaleIdIsPassed()
        {
            //Arrange
            var databaseName = System.Reflection.MethodBase.GetCurrentMethod().Name;

            var options = DbSeed.GetOptions(databaseName);

            DbSeed.SeedDatabase(options);

            var validSaleID = 1;

            using (var context = new StoreSystemDbContext(options))
            {
                var dateTimeNowProvider = new Mock<IDateTimeNowProvider>();
                var validDate = new DateTime(2019, 4, 1);
                var sut = new SaleService(context, dateTimeNowProvider.Object);

                //Act
                var actualSale =await sut.GetSaleByIDAsync(validSaleID);

                //Assert
                Assert.AreEqual(validSaleID, actualSale.SaleID);

            }
        }

        [TestMethod]
        public async Task ThrowsArgumentException_WhenInvalidSaleIdIsPassed()
        {
            //Arrange
            var databaseName = System.Reflection.MethodBase.GetCurrentMethod().Name;

            var options = DbSeed.GetOptions(databaseName);

            DbSeed.SeedDatabase(options);

            int invalidSaleID = 100;

            using (var context = new StoreSystemDbContext(options))
            {
                var dateTimeNowProvider = new Mock<IDateTimeNowProvider>();
                var validDate = new DateTime(2019, 4, 1);
                var sut = new SaleService(context, dateTimeNowProvider.Object);

                //Act
                //Assert
                await Assert.ThrowsExceptionAsync<ArgumentException>(() => sut.GetSaleByIDAsync(invalidSaleID));
            }
        }
    }
}
