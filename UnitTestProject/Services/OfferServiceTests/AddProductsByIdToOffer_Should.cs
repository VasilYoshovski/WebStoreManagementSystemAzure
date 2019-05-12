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

namespace StoreSystem.Tests.Services.OfferServiceTests
{
    [TestClass]
    public class AddProductsByIdToOffer_Should
    {
        [TestMethod]
        public async Task AddProductsToOffer_WhenValidParametersArePassed()
        {
            //Arrange
            var databaseName = System.Reflection.MethodBase.GetCurrentMethod().Name;

            var options = DbSeed.GetOptions(databaseName);

            DbSeed.SeedDatabase(options);

            int validOfferID;

            var validProductId1 = 1;
            var validProductId2 = 2;

            using (var getContext = new StoreSystemDbContext(options))
            {
                validOfferID = getContext.Offers.Include(x => x.ProductsInOffer).First(x => x.ProductsInOffer.Count == 0).OfferID;
            }

            using (var context = new StoreSystemDbContext(options))
            {
                var dateTimeNowProvider = new Mock<IDateTimeNowProvider>();
                var validDate = new DateTime(2019, 4, 1);
                var sut = new OfferService(context, dateTimeNowProvider.Object);
                ProductIdQuantityDto[] products = new[]
                {
                    new ProductIdQuantityDto(validProductId1,1),
                    new ProductIdQuantityDto(validProductId2,1),
                };

                //Act
                var isExecuted = await sut.AddProductsByIdToOfferAsync(validOfferID, products);

                //Assert
                Assert.IsTrue(isExecuted);
                Assert.AreEqual(2, context.Offers.Include(x => x.ProductsInOffer).First(x => x.OfferID == validOfferID).ProductsInOffer.Count);

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
                ProductIdQuantityDto[] products = new ProductIdQuantityDto[0];
                var errorText = string.Format(
                                       Consts.ObjectIDNotExist,
                                       nameof(Offer),
                                       invalidOfferID);
                //Act
                //Assert
                Assert.AreEqual(
                    errorText,
                    (await Assert.ThrowsExceptionAsync<ArgumentException>(() => sut.AddProductsByIdToOfferAsync(invalidOfferID, products))).Message);
            }
        }

        [TestMethod]
        public async Task CreateThrowsArgumentExceptionWithProperMessage_WhenInvalidProductQuantityIsPassed()
        {
            //Arrange
            var databaseName = System.Reflection.MethodBase.GetCurrentMethod().Name;

            var options = DbSeed.GetOptions(databaseName);

            DbSeed.SeedDatabase(options);

            int validOfferID;
            var validProductId1 = 1;
            string validProductName;

            using (var getContext = new StoreSystemDbContext(options))
            {
                validOfferID = getContext.Offers.Include(x => x.ProductsInOffer).First(x => x.ProductsInOffer.Count == 0).OfferID;
                validProductName = getContext.Products.Find(validProductId1).Name;
            }

            using (var context = new StoreSystemDbContext(options))
            {
                var dateTimeNowProvider = new Mock<IDateTimeNowProvider>();
                var validDate = new DateTime(2019, 4, 1);
                var sut = new OfferService(context, dateTimeNowProvider.Object);
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
                    (await Assert.ThrowsExceptionAsync<ArgumentException>(() => sut.AddProductsByIdToOfferAsync(validOfferID, products))).Message);

            }
        }
    }
}
