using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using StoreSystem.Data.DbContext;
using StoreSystem.Data.Models;
using StoreSystem.Services;
using StoreSystem.Services.Dto;
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
    public class AddProductsByIdToSale_Should
    {
        [TestMethod]
        public async Task AddProductsToSale_WhenValidParametersArePassed()
        {
            //Arrange
            var databaseName = System.Reflection.MethodBase.GetCurrentMethod().Name;

            var options = DbSeed.GetOptions(databaseName);

            DbSeed.SeedDatabase(options);

            int validSaleID;

            var validProductId1 = 1;
            var validProductId2 = 2;

            using (var getContext = new StoreSystemDbContext(options))
            {
                validSaleID = getContext.Sales.Include(x => x.ProductsInSale).First(x => x.ProductsInSale.Count == 0).SaleID;
            }

            using (var context = new StoreSystemDbContext(options))
            {
                var dateTimeNowProvider = new Mock<IDateTimeNowProvider>();
                var validDate = new DateTime(2019, 4, 1);
                var sut = new SaleService(context, dateTimeNowProvider.Object);
                ProductIdQuantityDto[] products = new[]
                {
                    new ProductIdQuantityDto(validProductId1,1),
                    new ProductIdQuantityDto(validProductId2,1),
                };

                //Act
                var isExecuted = await sut.AddProductsByIdToSaleAsync(validSaleID, products);

                //Assert
                Assert.IsTrue(isExecuted);
                Assert.AreEqual(2, context.Sales.Include(x => x.ProductsInSale).First(x => x.SaleID == validSaleID).ProductsInSale.Count);

            }
        }
        [TestMethod]
        public async Task DecreaseQuantityOfAddedProductsInStock_WhenValidParametersArePassed()
        {
            //Arrange
            var databaseName = System.Reflection.MethodBase.GetCurrentMethod().Name;

            var options = DbSeed.GetOptions(databaseName);

            DbSeed.SeedDatabase(options);

            int validSaleID;
            using (var getContext = new StoreSystemDbContext(options))
            {
                validSaleID = getContext.Sales.Include(x => x.ProductsInSale).First(x => x.ProductsInSale.Count == 0).SaleID;
            }

            var validProductId1 = 1;
            var validProductId2 = 2;
            decimal expectedQuntityProd1 = 1-1;
            decimal expectedQuntityProd2 = 2-1;

            using (var context = new StoreSystemDbContext(options))
            {
                var dateTimeNowProvider = new Mock<IDateTimeNowProvider>();
                var validDate = new DateTime(2019, 4, 1);
                var sut = new SaleService(context, dateTimeNowProvider.Object);
                ProductIdQuantityDto[] products = new[]
                {
                    new ProductIdQuantityDto(validProductId1,1),
                    new ProductIdQuantityDto(validProductId2,1),
                };

                //Act
                var isExecuted = await sut.AddProductsByIdToSaleAsync(validSaleID, products);

                //Assert
                Assert.IsTrue(isExecuted);
                Assert.AreEqual(expectedQuntityProd1, context.Products.Find(1).Quantity);
                Assert.AreEqual(expectedQuntityProd2, context.Products.Find(2).Quantity);

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
                ProductIdQuantityDto[] products = new ProductIdQuantityDto[0];
                var errorText = string.Format(
                                       Consts.ObjectIDNotExist,
                                       nameof(Sale),
                                       invalidSaleID);
                //Act
                //Assert
                Assert.AreEqual(
                    errorText,
                    (await Assert.ThrowsExceptionAsync<ArgumentException>(() => sut.AddProductsByIdToSaleAsync(invalidSaleID, products))).Message);
            }
        }

        [TestMethod]
        public async Task CreateThrowsArgumentExceptionWithProperMessage_WhenInvalidProductQuantityIsPassed()
        {
            //Arrange
            var databaseName = System.Reflection.MethodBase.GetCurrentMethod().Name;

            var options = DbSeed.GetOptions(databaseName);

            DbSeed.SeedDatabase(options);

            int validSaleID;
            var validProductId1 = 1;
            string validProductName;

            using (var getContext = new StoreSystemDbContext(options))
            {
                validSaleID = getContext.Sales.Include(x => x.ProductsInSale).First(x => x.ProductsInSale.Count == 0).SaleID;
                validProductName = getContext.Products.Find(validProductId1).Name;
            }

            using (var context = new StoreSystemDbContext(options))
            {
                var dateTimeNowProvider = new Mock<IDateTimeNowProvider>();
                var validDate = new DateTime(2019, 4, 1);
                var sut = new SaleService(context, dateTimeNowProvider.Object);
                ProductIdQuantityDto[] products = new[]
                {
                    new ProductIdQuantityDto(validProductId1,100),
                };
                var errorText = string.Format(
                       Consts.QuantityNotEnough,
                       validProductName,
                       validProductId1);
                //Act
                //Assert
                Assert.AreEqual(
                    errorText,
                    (await Assert.ThrowsExceptionAsync<ArgumentException>(() => sut.AddProductsByIdToSaleAsync(validSaleID, products))).Message);

            }
        }
    }
}
