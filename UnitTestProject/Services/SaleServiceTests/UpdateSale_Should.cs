using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using StoreSystem.Data.DbContext;
using StoreSystem.Data.Models;
using StoreSystem.Services;
using StoreSystem.Services.Providers;
using StoreSystem.Services.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreSystem.Tests.Services.SaleServiceTests
{
    [TestClass]
    public class UpdateSale_Should
    {
        [TestMethod]
        public async Task UpdateSale_WhenValidParanetersArePassed()
        {
            //Arrange
            var databaseName = System.Reflection.MethodBase.GetCurrentMethod().Name;

            var options = DbSeed.GetOptions(databaseName);

            DbSeed.SeedDatabase(options);

            int validSaleId;
            DateTime deliveryDate;
            var client = 1;
            var address = 1;
            var city = 1;
            var country = 1;
            var deadline = new DateTime(2019, 5, 10);
            var discount = 0.10m;
            var saleDate = new DateTime(2019, 5, 1);
            using (var getContext = new StoreSystemDbContext(options))
            {
                var sale = getContext.Sales.First();
                validSaleId = sale.SaleID;
                deliveryDate = sale.OrderDate.AddDays(1);
            }

            using (var context = new StoreSystemDbContext(options))
            {
                var dateTimeNowProvider = new Mock<IDateTimeNowProvider>();
                var sut = new SaleService(context, dateTimeNowProvider.Object);

                //Act
                var isExecuted = await sut.UpdateSaleAsync(validSaleId, client, discount, saleDate, (deadline - saleDate).TotalDays, deliveryDate, address, city, country);

                //Assert
                Assert.IsTrue(isExecuted);
                Assert.AreEqual(client, context.Sales.Find(validSaleId).ClientID);
                Assert.AreEqual(discount, context.Sales.Find(validSaleId).ProductDiscount);
                Assert.AreEqual(saleDate, context.Sales.Find(validSaleId).OrderDate);
                Assert.AreEqual(deliveryDate, context.Sales.Find(validSaleId).DeliveryDate);
                Assert.AreEqual(deadline, context.Sales.Find(validSaleId).DeadlineDate);
                Assert.AreEqual(address, context.Sales.Find(validSaleId).AddressID);
                Assert.AreEqual(city, context.Sales.Find(validSaleId).CityID);
                Assert.AreEqual(country, context.Sales.Find(validSaleId).CountryID);

            }
        }

        [TestMethod]
        public async Task ThrowsArgumentException_WhenInvalidSaleIdIsPassed()
        {
            //Arrange
            var databaseName = System.Reflection.MethodBase.GetCurrentMethod().Name;

            var options = DbSeed.GetOptions(databaseName);

            DbSeed.SeedDatabase(options);

            int invalidSaleId = 100;
            var client = 1;
            var address = 1;
            var city = 1;
            var country = 1;
            var deadline = new DateTime(2019, 5, 10);
            var deliveryDate = new DateTime(2019, 5, 8);
            var discount = 0.10m;
            var saleDate = new DateTime(2019, 5, 1);


            using (var context = new StoreSystemDbContext(options))
            {
                var dateTimeNowProvider = new Mock<IDateTimeNowProvider>();
                var sut = new SaleService(context, dateTimeNowProvider.Object);
                var errorText = string.Format(
                                       Consts.ObjectIDNotExist,
                                       nameof(Sale),
                                       invalidSaleId);
                //Act
                //Assert
                Assert.AreEqual(
                    errorText,
                    (await Assert.ThrowsExceptionAsync<ArgumentException>(() =>
                    sut.UpdateSaleAsync(invalidSaleId, client, discount, saleDate, (deadline - saleDate).TotalDays, deliveryDate, address, city, country))).Message
                    );
            }
        }


        [TestMethod]
        public async Task ThrowsArgumentException_WhenInvalidDateIsPassed()
        {
            //Arrange
            var databaseName = System.Reflection.MethodBase.GetCurrentMethod().Name;

            var options = DbSeed.GetOptions(databaseName);

            DbSeed.SeedDatabase(options);

            int validSaleId;
            DateTime deliveryDate = new DateTime(2019, 5, 8);
            var client = 1;
            var address = 1;
            var city = 1;
            var country = 1;
            var discount = 0.10m;
            DateTime saleDate;
            DateTime invalidDeadlineDate;


            using (var getContext = new StoreSystemDbContext(options))
            {
                var sale = getContext.Sales.First();
                validSaleId = sale.SaleID;
                invalidDeadlineDate = sale.OrderDate.AddDays(-1);
                saleDate = sale.OrderDate;
            }

            using (var context = new StoreSystemDbContext(options))
            {
                var dateTimeNowProvider = new Mock<IDateTimeNowProvider>();
                var sut = new SaleService(context, dateTimeNowProvider.Object);
                var errorText = string.Format(
                       Consts.DateError,
                       invalidDeadlineDate,
                       saleDate);
                //Act
                //Assert
                Assert.AreEqual(
                    errorText,
                    (await Assert.ThrowsExceptionAsync<ArgumentException>(() =>
                    sut.UpdateSaleAsync(validSaleId, client, discount, saleDate, (invalidDeadlineDate - saleDate).TotalDays, deliveryDate, address, city, country))).Message
                    );
            }
        }
    }
}