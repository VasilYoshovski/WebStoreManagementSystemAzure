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
    public class CreateOffer_Should
    {


        [TestMethod]
        public async Task CreateAndAddNewOfferWithIDsInDB_WhenValidParametersArePassed()
        {
            var databaseName = System.Reflection.MethodBase.GetCurrentMethod().Name;

            using (var context = new StoreSystemDbContext(DbSeed.GetOptions(databaseName)))
            {
                //Arrange
                var dateTimeNowProvider = new Mock<IDateTimeNowProvider>();

                var validDate = new DateTime(2019, 1, 1);

                dateTimeNowProvider.Setup(x => x.Now).Returns(validDate);
                var sut = new OfferService(context, dateTimeNowProvider.Object);
                var client = 1;
                var address = 1;
                var city = 1;
                var country = 1;
                var deadline = new DateTime(2019, 5, 10);
                var discount = 0.10m;

                //Act
                await sut.CreateOfferAsync(client, discount,deadline, address, city, country);

                //Assert
                Assert.AreEqual(1, context.Offers.Count());
                Assert.AreEqual(client, context.Offers.Single(x => x.ClientID == client).ClientID);
            }
        }
    }
}
