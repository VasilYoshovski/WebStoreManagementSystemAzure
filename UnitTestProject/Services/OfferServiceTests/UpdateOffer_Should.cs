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

namespace StoreSystem.Tests.Services.OfferServiceTests
{
    [TestClass]
    public class UpdateOffer_Should
    {
        [TestMethod]
        public async Task UpdateOffer_WhenValidParanetersArePassed()
        {
            //Arrange
            var databaseName = System.Reflection.MethodBase.GetCurrentMethod().Name;

            var options = DbSeed.GetOptions(databaseName);

            DbSeed.SeedDatabase(options);

            int validOfferId;
            DateTime deliveryDate;
            var client = 1;
            var address = 1;
            var city = 1;
            var country = 1;
            var deadline = new DateTime(2019, 5, 10);
            var discount = 0.10m;
            var offerDate = new DateTime(2019, 5, 1);
            using (var getContext = new StoreSystemDbContext(options))
            {
                var offer = getContext.Offers.First();
                validOfferId = offer.OfferID;
                deliveryDate = offer.OfferDate.AddDays(1);
            }

            using (var context = new StoreSystemDbContext(options))
            {
                var dateTimeNowProvider = new Mock<IDateTimeNowProvider>();
                var sut = new OfferService(context, dateTimeNowProvider.Object);

                //Act
                var isExecuted = await sut.UpdateOfferAsync(validOfferId, client, discount, offerDate, (deadline-offerDate).TotalDays, deliveryDate,address, city, country);

                //Assert
                Assert.IsTrue(isExecuted);
                Assert.AreEqual(client, context.Offers.Find(validOfferId).ClientID);
                Assert.AreEqual(discount, context.Offers.Find(validOfferId).ProductDiscount);
                Assert.AreEqual(offerDate, context.Offers.Find(validOfferId).OfferDate);
                Assert.AreEqual(deliveryDate, context.Offers.Find(validOfferId).DeliveryDate);
                Assert.AreEqual(deadline, context.Offers.Find(validOfferId).ExpiredDate);
                Assert.AreEqual(address, context.Offers.Find(validOfferId).AddressID);
                Assert.AreEqual(city, context.Offers.Find(validOfferId).CityID);
                Assert.AreEqual(country, context.Offers.Find(validOfferId).CountryID);

            }
        }

        [TestMethod]
        public async Task ThrowsArgumentException_WhenInvalidOfferIdIsPassed()
        {
            //Arrange
            var databaseName = System.Reflection.MethodBase.GetCurrentMethod().Name;

            var options = DbSeed.GetOptions(databaseName);

            DbSeed.SeedDatabase(options);

            int invalidOfferId = 100;
            var client = 1;
            var address = 1;
            var city = 1;
            var country = 1;
            var deadline = new DateTime(2019, 5, 10);
            var deliveryDate = new DateTime(2019, 5, 8);
            var discount = 0.10m;
            var offerDate = new DateTime(2019, 5, 1);


            using (var context = new StoreSystemDbContext(options))
            {
                var dateTimeNowProvider = new Mock<IDateTimeNowProvider>();
                var sut = new OfferService(context, dateTimeNowProvider.Object);
                var errorText = string.Format(
                                       Consts.ObjectIDNotExist,
                                       nameof(Offer),
                                       invalidOfferId);
                //Act
                //Assert
                Assert.AreEqual(
                    errorText,
                    (await Assert.ThrowsExceptionAsync<ArgumentException>(() =>
                    sut.UpdateOfferAsync(invalidOfferId, client, discount, offerDate, (deadline - offerDate).TotalDays, deliveryDate, address, city, country))).Message
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

            int validOfferId;
            DateTime deliveryDate = new DateTime(2019,5,8);
            var client = 1;
            var address = 1;
            var city = 1;
            var country = 1;
            var discount = 0.10m;
            DateTime offerDate;
            DateTime invalidDeadlineDate;


            using (var getContext = new StoreSystemDbContext(options))
            {
                var offer = getContext.Offers.First();
                validOfferId = offer.OfferID;
                invalidDeadlineDate = offer.OfferDate.AddDays(-1);
                offerDate = offer.OfferDate;
            }

            using (var context = new StoreSystemDbContext(options))
            {
                var dateTimeNowProvider = new Mock<IDateTimeNowProvider>();
                var sut = new OfferService(context, dateTimeNowProvider.Object);
                var errorText = string.Format(
                       Consts.DateError,
                       invalidDeadlineDate,
                       offerDate);
                //Act
                //Assert
                Assert.AreEqual(
                    errorText,
                    (await Assert.ThrowsExceptionAsync<ArgumentException>(() =>
                    sut.UpdateOfferAsync(validOfferId, client, discount, offerDate, (invalidDeadlineDate - offerDate).TotalDays, deliveryDate, address, city, country))).Message
                    );
            }
        }
    }
}