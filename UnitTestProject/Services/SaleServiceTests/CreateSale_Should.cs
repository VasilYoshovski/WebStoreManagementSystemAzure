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
    public class CreateSale_Should
    {
        [TestMethod]
        public async Task CreateAndAddNewSaleInDB_WhenValidParametersArePassed()
        {
            var databaseName = System.Reflection.MethodBase.GetCurrentMethod().Name;

            using (var context = new StoreSystemDbContext(DbSeed.GetOptions(databaseName)))
            {
                var dateTimeNowProvider = new Mock<IDateTimeNowProvider>();

                var validDate = new DateTime(2019,4,1);
                var validClientName = "Pesho";

                dateTimeNowProvider.Setup(x => x.Now).Returns(validDate);
                var sut = new SaleService(context, dateTimeNowProvider.Object);
                var client = new Client() { Name = validClientName };
                var address = new Address();
                var city = new City();
                var country = new Country();
                await sut.CreateSaleAsync(client, 0.15m, 3, address, city, country);
                Assert.AreEqual(1, context.Sales.Count());
                Assert.AreEqual(validClientName, context.Sales.Single(x => x.Client == client).Client.Name);
            }
        }

        [TestMethod]
        public async Task CreateAndAddNewSaleWithIDsInDB_WhenValidParametersArePassed()
        {
            var databaseName = System.Reflection.MethodBase.GetCurrentMethod().Name;

            using (var context = new StoreSystemDbContext(DbSeed.GetOptions(databaseName)))
            {
                //Arrange
                var dateTimeNowProvider = new Mock<IDateTimeNowProvider>();

                var validDate = new DateTime(2019, 1, 1);

                dateTimeNowProvider.Setup(x => x.Now).Returns(validDate);
                var sut = new SaleService(context, dateTimeNowProvider.Object);
                var client = 1;
                var address = 1;
                var city = 1;
                var country = 1;
                var deadline = new DateTime(2019, 5, 10);
                var discount = 0.10m;

                //Act
                await sut.CreateSaleAsync(client, discount,deadline, address, city, country);

                //Assert
                Assert.AreEqual(1, context.Sales.Count());
                Assert.AreEqual(client, context.Sales.Single(x => x.ClientID == client).ClientID);
            }
        }
    }
}
