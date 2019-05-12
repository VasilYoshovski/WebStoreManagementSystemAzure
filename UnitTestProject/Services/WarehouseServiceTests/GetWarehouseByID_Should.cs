using Microsoft.VisualStudio.TestTools.UnitTesting;
using StoreSystem.Data.DbContext;
using StoreSystem.Data.Models;
using StoreSystem.Services;
using System;
using System.Threading.Tasks;

namespace StoreSystem.Tests.Services.WarehouseServiceTests
{
    [TestClass]
    public class GetWarehouseByID_Should
    {
        [TestMethod]
        [DataRow(9991)]
        [DataRow(9992)]
        public async Task GetWarehouseWhenValidWarehouseIDPassed(int id)
        {
            //Arrange
            var GetWarehouseWhenValidWarehouseIDPassed = System.Reflection.MethodBase.GetCurrentMethod().Name;

            var options = Utils.GetOptions(GetWarehouseWhenValidWarehouseIDPassed);

            Utils.SeedDatabase(options);

            using (var arrangeContext = new StoreSystemDbContext(options))
            {
                var tmpWarehouse = new Warehouse()
                {
                    Name = "valid name" + id.ToString(),
                    WarehouseID = id,
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
                var actualWarehouse = await sut.GetWarehouseByIDAsync(id);

                //Assert
                Assert.AreEqual(id, actualWarehouse.WarehouseID);
            }
        }

        [TestMethod]
        [DataRow(-1)]
        [DataRow(0)]
        public async Task ReturnNullWhenInvalidWarehouseIDIsPassed(int id)
        {
            //Arrange
            var ReturnNullWhenInvalidWarehouseIDIsPassed = System.Reflection.MethodBase.GetCurrentMethod().Name;

            var options = Utils.GetOptions(ReturnNullWhenInvalidWarehouseIDIsPassed);

            Utils.SeedDatabase(options);

            using (var arrangeContext = new StoreSystemDbContext(options))
            {
                var tmpWarehouse = new Warehouse()
                {
                    Name = "valid name",
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
                await Assert.ThrowsExceptionAsync<ArgumentException>(async () => await sut.GetWarehouseByIDAsync(id));
            }
        }
    }
}
