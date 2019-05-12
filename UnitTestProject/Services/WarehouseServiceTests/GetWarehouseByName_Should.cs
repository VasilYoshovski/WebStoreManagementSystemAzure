using Microsoft.VisualStudio.TestTools.UnitTesting;
using StoreSystem.Data.DbContext;
using StoreSystem.Data.Models;
using StoreSystem.Services;
using System;
using System.Threading.Tasks;

namespace StoreSystem.Tests.Services.WarehouseServiceTests
{
    [TestClass]
    public class GetWarehouseByName_Should
    {
        [TestMethod]
        [DataRow("Warehouse1")]
        [DataRow("Warehouse2")]
        public async Task GetWarehouseWhenValidWarehouseNamePassed(string validWarehouseName)
        {
            //Arrange
            var GetWarehouseWhenValidWarehouseNamePassed = System.Reflection.MethodBase.GetCurrentMethod().Name;

            var options = Utils.GetOptions(GetWarehouseWhenValidWarehouseNamePassed);

            Utils.SeedDatabase(options);

            using (var arrangeContext = new StoreSystemDbContext(options))
            {
                var tmpWarehouse = new Warehouse()
                {
                    Name = validWarehouseName,
                    WarehouseID = 1000,
                    AddressID = 1,
                    CityID = 1,
                    CountryID = 1
                };
                arrangeContext.Warehouses.Add(tmpWarehouse);
                await arrangeContext.SaveChangesAsync();
            }

            using (var context = new StoreSystemDbContext(options))
            {
                var sut = new WarehouseService(context);

                //Act
                var actualWarehouse = await sut.GetWarehouseByNameAsync(validWarehouseName);

                //Assert
                Assert.AreEqual(validWarehouseName, actualWarehouse.Name);
            }
        }

        [TestMethod]
        [DataRow("Warehouse13")]
        [DataRow("Warehouse23")]
        public async Task ReturnNullWhenInvalidWarehouseNameIsPassed(string validWarehouseName)
        {
            //Arrange
            var ReturnNullWhenInvalidWarehouseNameIsPassed = System.Reflection.MethodBase.GetCurrentMethod().Name;

            var options = Utils.GetOptions(ReturnNullWhenInvalidWarehouseNameIsPassed);

            Utils.SeedDatabase(options);

            using (var arrangeContext = new StoreSystemDbContext(options))
            {
                var tmpWarehouse = new Warehouse()
                {
                    Name = "fakeName",
                    WarehouseID = 1011,
                    AddressID = 1,
                    CityID = 1,
                    CountryID = 1
                };
                arrangeContext.Warehouses.Add(tmpWarehouse);
                await arrangeContext.SaveChangesAsync();
            }

            //Act & Assert
            using (var context = new StoreSystemDbContext(options))
            {
                var sut = new WarehouseService(context);
                await Assert.ThrowsExceptionAsync<ArgumentException>(async () => await sut.GetWarehouseByNameAsync(validWarehouseName));
            }
        }
    }
}
