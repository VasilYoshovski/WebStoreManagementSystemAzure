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
    public class DeleteOffer_Should
    {
        [TestMethod]
        public async Task DeleteProperOffer_WhenValidOfferIdIsPassed()
        {
            //Arrange
            var databaseName = System.Reflection.MethodBase.GetCurrentMethod().Name;

            var options = DbSeed.GetOptions(databaseName);

            DbSeed.SeedDatabase(options);

            int validOfferId;
            int totalCountOfOffers;

            using (var getContext = new StoreSystemDbContext(options))
            {
                validOfferId = getContext.Offers.First().OfferID;
                totalCountOfOffers = getContext.Offers.Count();
            }

            using (var context = new StoreSystemDbContext(options))
            {
                var dateTimeNowProvider = new Mock<IDateTimeNowProvider>();
                var sut = new OfferService(context, dateTimeNowProvider.Object);

                //Act
                var isExecxuted = await sut.DeleteOfferAsync(validOfferId);

                //Assert
                Assert.IsTrue(isExecxuted);
                Assert.IsTrue(context.Offers.Find(validOfferId) == null);
                Assert.AreEqual(totalCountOfOffers - 1, context.Offers.Count());
            }
        }

        [TestMethod]
        public async Task DeleteProperProductOfferRecords_WhenValidOfferIdIsPassed()
        {
            //Arrange
            var databaseName = System.Reflection.MethodBase.GetCurrentMethod().Name;

            var options = DbSeed.GetOptions(databaseName);

            DbSeed.SeedDatabase(options);
            Offer sale;
            int totalRecordsInProductOffer;

            using (var getContext = new StoreSystemDbContext(options))
            {
                sale = getContext.Offers.Include(x => x.ProductsInOffer).First();
                totalRecordsInProductOffer = getContext.ProductOffers.Count();    
            }

            int validOfferId = sale.OfferID;
            var countOfProductsInOffer = sale.ProductsInOffer.Count();

            using (var context = new StoreSystemDbContext(options))
            {
                var dateTimeNowProvider = new Mock<IDateTimeNowProvider>();
                var sut = new OfferService(context, dateTimeNowProvider.Object);

                //Act
                var isExecxuted = await sut.DeleteOfferAsync(validOfferId);

                //Assert
                Assert.IsTrue(isExecxuted);
                Assert.AreEqual(totalRecordsInProductOffer - countOfProductsInOffer, context.ProductOffers.Count());
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
                await Assert.ThrowsExceptionAsync<ArgumentException>(() => sut.DeleteOfferAsync(invalidOfferID));
            }
        }
    }
}


