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

namespace StoreSystem.Tests.Services.OfferServiceTests
{
    [TestClass]
    public class GetOfferByID_Should
    {
        [TestMethod]
        public async Task FindProperOffer_WhenValidOfferIdIsPassed()
        {
            //Arrange
            var databaseName = System.Reflection.MethodBase.GetCurrentMethod().Name;

            var options = DbSeed.GetOptions(databaseName);

            DbSeed.SeedDatabase(options);

            var validOfferID = 1;

            using (var context = new StoreSystemDbContext(options))
            {
                var dateTimeNowProvider = new Mock<IDateTimeNowProvider>();
                var validDate = new DateTime(2019, 4, 1);
                var sut = new OfferService(context, dateTimeNowProvider.Object);

                //Act
                var actualOffer =await sut.GetOfferByIDAsync(validOfferID);

                //Assert
                Assert.AreEqual(validOfferID, actualOffer.OfferID);

            }
        }

        [TestMethod]
        public async Task ThrowsArgumentException_WhenInvalidOfferIdIsPassed()
        {
            //Arrange
            var databaseName = System.Reflection.MethodBase.GetCurrentMethod().Name;

            var options = DbSeed.GetOptions(databaseName);

            DbSeed.SeedDatabase(options);

            int invalidOfferID = 100;

            using (var context = new StoreSystemDbContext(options))
            {
                var dateTimeNowProvider = new Mock<IDateTimeNowProvider>();
                var validDate = new DateTime(2019, 4, 1);
                var sut = new OfferService(context, dateTimeNowProvider.Object);

                //Act
                //Assert
                await Assert.ThrowsExceptionAsync<ArgumentException>(() => sut.GetOfferByIDAsync(invalidOfferID));
            }
        }
    }
}
