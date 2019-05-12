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
    public class GetOfferQuantity_Should
    {
        [TestMethod]
        public async Task ShouldReturnTotalSumOfFiltredSales_WhenValidDatesArePassed()
        {
            //Arrange
            var databaseName = System.Reflection.MethodBase.GetCurrentMethod().Name;

            var options = DbSeed.GetOptions(databaseName);

            DbSeed.SeedDatabase(options);

            var validStartDate = new DateTime(2019, 2, 2);
            var validEndDate = new DateTime(2019, 4, 4);
            decimal expectedTotal = (decimal)((1 * 1 + 1 * 3 * 2) * (1 - 0.1 / 100));

            using (var context = new StoreSystemDbContext(options))
            {
                var dateTimeNowProvider = new Mock<IDateTimeNowProvider>();
                var sut = new SaleService(context, dateTimeNowProvider.Object);

                //Act
                var actualTotal = await sut.GetSaleQuantityAsync(startDate: validStartDate, endDate: validEndDate);

                //Assert
                Assert.AreEqual(expectedTotal, actualTotal);
            }
        }

        [TestMethod]
        public async Task ShouldReturnTotalSumOfFiltredSales_WhenValidClientNameIsPassed()
        {
            //Arrange
            var databaseName = System.Reflection.MethodBase.GetCurrentMethod().Name;

            var options = DbSeed.GetOptions(databaseName);

            DbSeed.SeedDatabase(options);

            var validClientName = "valid client 1";
            decimal expectedTotal = (decimal)((1 * 1 + 1 * 2 + 1 * 3) * (1 - 0.1 / 100));

            using (var context = new StoreSystemDbContext(options))
            {
                var dateTimeNowProvider = new Mock<IDateTimeNowProvider>();
                var sut = new SaleService(context, dateTimeNowProvider.Object);

                //Act
                var actualTotal = await sut.GetSaleQuantityAsync(clientName: validClientName);

                //Assert
                Assert.AreEqual(expectedTotal, actualTotal);
            }
        }


        [TestMethod]
        public async Task ShouldReturnTotalSumOfFiltredSales_WhenValidClientIdIsPassed()
        {
            //Arrange
            var databaseName = System.Reflection.MethodBase.GetCurrentMethod().Name;

            var options = DbSeed.GetOptions(databaseName);

            DbSeed.SeedDatabase(options);

            var validClientId = 1;
            decimal expectedTotal = (decimal)((1 * 1 + 1 * 2 + 1 * 3) * (1 - 0.1/100));

            using (var context = new StoreSystemDbContext(options))
            {
                var dateTimeNowProvider = new Mock<IDateTimeNowProvider>();
                var sut = new SaleService(context, dateTimeNowProvider.Object);

                //Act
                var actualTotal = await sut.GetSaleQuantityAsync(clientID: validClientId);

                //Assert
                Assert.AreEqual(expectedTotal, actualTotal);
            }
        }


        [TestMethod]
        public async Task ShouldReturnTotalSumOfFiltredSales_WhenValidSaleIdIsPassed()
        {
            //Arrange
            var databaseName = System.Reflection.MethodBase.GetCurrentMethod().Name;

            var options = DbSeed.GetOptions(databaseName);

            DbSeed.SeedDatabase(options);

            var validSaleId = 1;
            decimal expectedTotal = (decimal)((1 * 1 + 1 * 2) * (1 - 0.1 / 100));

            using (var context = new StoreSystemDbContext(options))
            {
                var dateTimeNowProvider = new Mock<IDateTimeNowProvider>();
                var sut = new SaleService(context, dateTimeNowProvider.Object);

                //Act
                var actualTotal = await sut.GetSaleQuantityAsync(saleID: validSaleId);

                //Assert
                Assert.AreEqual(expectedTotal, actualTotal);
            }
        }


        [TestMethod]
        public async Task ShouldReturnTotalSumOfAllSales_WhenNoParamsArePassed()
        {
            //Arrange
            var databaseName = System.Reflection.MethodBase.GetCurrentMethod().Name;

            var options = DbSeed.GetOptions(databaseName);

            DbSeed.SeedDatabase(options);

            var validClientName = "valid client 1";
            decimal expectedTotal = (decimal)((1 * 1 + 1 * 2 + 1 * 3) * (1 - 0.1/100));

            using (var context = new StoreSystemDbContext(options))
            {
                var dateTimeNowProvider = new Mock<IDateTimeNowProvider>();
                var sut = new SaleService(context, dateTimeNowProvider.Object);

                //Act
                var actualTotal = await sut.GetSaleQuantityAsync(clientName: validClientName);

                //Assert
                Assert.AreEqual(expectedTotal, actualTotal);
            }
        }

        [TestMethod]
        public async Task ShouldReturnTotalSumOfFiltredSales_WhenValidDatesAndClientNameArePassed()
        {
            //Arrange
            var databaseName = System.Reflection.MethodBase.GetCurrentMethod().Name;

            var options = DbSeed.GetOptions(databaseName);

            DbSeed.SeedDatabase(options);

            var validStartDate = new DateTime(2019, 2, 2);
            var validEndDate = new DateTime(2019, 4, 4);
            var validClientName = "valid client 2";

            decimal expectedTotal = (decimal)((1 * 1 + 1 * 3 ) * (1 - 0.1 / 100));

            using (var context = new StoreSystemDbContext(options))
            {
                var dateTimeNowProvider = new Mock<IDateTimeNowProvider>();
                var sut = new SaleService(context, dateTimeNowProvider.Object);

                //Act
                var actualTotal = await sut.GetSaleQuantityAsync(startDate: validStartDate, endDate: validEndDate, clientName: validClientName);

                //Assert
                Assert.AreEqual(expectedTotal, actualTotal);
            }
        }

    }
}
