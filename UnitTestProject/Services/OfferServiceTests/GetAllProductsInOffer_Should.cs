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
    public class GetAllProductsInOffer_Should
    {
        [TestMethod]
        public async Task ShouldReturnCollectionWithProductInOffer_WhenValidOfferIdIsPassed()
        {
            //Arrange
            var databaseName = System.Reflection.MethodBase.GetCurrentMethod().Name;

            var options = DbSeed.GetOptions(databaseName);

            DbSeed.SeedDatabase(options);

            int validOfferId = 1;
            int productsInOfferCount = 2;
            ICollection<ProductOffer> products = new List<ProductOffer>
                        {
                            new ProductOffer{ProductID = 1, Quantity =1 },
                            new ProductOffer{ProductID = 2, Quantity =1 }
                        };

            //using (var getContext = new StoreSystemDbContext(options))
            //{
            //    var offer = getContext.Offers.Include(x => x.ProductsInOffer).First(x => x.ProductsInOffer.Count > 0);
            //    validOfferId = offer.OfferID;
            //    products = offer.ProductsInOffer;
            //    productsInOfferCount = products.Count;
            //}

            using (var context = new StoreSystemDbContext(options))
            {
                var dateTimeNowProvider = new Mock<IDateTimeNowProvider>();
                var sut = new OfferService(context, dateTimeNowProvider.Object);

                //Act
                var actualProductsInOffer = await sut.GetAllProductsInOfferAsync(validOfferId);

                //Assert
                Assert.AreEqual(productsInOfferCount, actualProductsInOffer.Count);
                CollectionAssert.AreEquivalent(products.Select(x => x.ProductID).ToList(), actualProductsInOffer.Select(x => x.ProductID).ToList());
            }
        }

        [TestMethod]
        public async Task ShouldThrowsArgumentException_WhenInvalidOfferIdIsPassed()
        {
            //Arrange
            var databaseName = System.Reflection.MethodBase.GetCurrentMethod().Name;

            var options = DbSeed.GetOptions(databaseName);

            DbSeed.SeedDatabase(options);

            int invalidOfferID = 100;

            using (var context = new StoreSystemDbContext(options))
            {
                var dateTimeNowProvider = new Mock<IDateTimeNowProvider>();
                var sut = new OfferService(context, dateTimeNowProvider.Object);

                //Act
                //Assert
                await Assert.ThrowsExceptionAsync<ArgumentException>(() =>sut.GetAllProductsInOfferAsync(invalidOfferID));
            }
        }
    }
}

