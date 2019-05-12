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
    public class CreateSaleByOfferID_Should
    {
        [TestMethod]
        public async Task CreateSaleFromOffer_WhenValidOfferIdIsPassed()
        {
            //Arrange
            var databaseName = System.Reflection.MethodBase.GetCurrentMethod().Name;

            var options = DbSeed.GetOptions(databaseName);

            DbSeed.SeedDatabase(options);

            Offer validOffer;

            using (var getContext = new StoreSystemDbContext(options))
            {
                validOffer = getContext.Offers.Include(x => x.Client).First();
            }

            using (var context = new StoreSystemDbContext(options))
            {
                var dateTimeNowProvider = new Mock<IDateTimeNowProvider>();
                var validDate = new DateTime(2019, 4, 1);
                var sut = new SaleService(context, dateTimeNowProvider.Object);
                
                //Act
                var saleActual = await sut.CreateSaleByOfferIDAsync(validOffer.OfferID);
                
                //Assert
                Assert.AreEqual(validOffer.OfferID, saleActual.OfferID);
                Assert.AreEqual(validOffer.ClientID, context.Sales.FirstOrDefault(x => x.OfferID == validOffer.OfferID).ClientID);
                Assert.AreEqual(validOffer.AddressID, context.Sales.FirstOrDefault(x => x.OfferID == validOffer.OfferID).AddressID);
                Assert.AreEqual(validOffer.CityID, context.Sales.FirstOrDefault(x => x.OfferID == validOffer.OfferID).CityID);
                Assert.AreEqual(validOffer.CountryID, context.Sales.FirstOrDefault(x => x.OfferID == validOffer.OfferID).CountryID);
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
                var sut = new SaleService(context, dateTimeNowProvider.Object);

                //Act & Assert 
                await Assert.ThrowsExceptionAsync<ArgumentException>(() => sut.CreateSaleByOfferIDAsync(invalidOfferID));
            }
        }

    }
}