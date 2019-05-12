using Microsoft.VisualStudio.TestTools.UnitTesting;
using StoreSystem.Data.DbContext;
using StoreSystem.Data.Models;
using StoreSystem.Services;
using System.Threading.Tasks;

namespace StoreSystem.Tests.Services.WarehouseServiceTests
{
    [TestClass]
    public class FindWarehouseByID_Should
    {
        [TestMethod]
        [DataRow(1000)]
        [DataRow(20000)]
        public async Task FindWarehouseWhenValidWarehouseIdIsPassed(int validWarehouseID)
        {
            //Arrange
            var FindWarehouseWhenValidWarehouseIdIsPassed = System.Reflection.MethodBase.GetCurrentMethod().Name;

            var options = Utils.GetOptions(FindWarehouseWhenValidWarehouseIdIsPassed);

            Utils.SeedDatabase(options);

            using (var arrangeContext = new StoreSystemDbContext(options))
            {
                var tmpWarehouse = new Warehouse()
                {
                    Name = "WH123456",
                    WarehouseID = validWarehouseID,
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
                var actualWarehouse = await sut.FindWarehouseByIDAsync(validWarehouseID);

                //Assert
                Assert.AreEqual(validWarehouseID, actualWarehouse.WarehouseID);
            }
        }

        [TestMethod]
        [DataRow(-100)]
        [DataRow(-2)]
        [DataRow(-1)]
        [DataRow(0)]
        [DataRow(3)]
        [DataRow(32)]
        [DataRow(356)]
        public async Task ReturnNullWhenInvalidWarehouseIdIsPassed(int validWarehouseID)
        {
            //Arrange
            var ReturnNullWhenInvalidWarehouseIdIsPassed = System.Reflection.MethodBase.GetCurrentMethod().Name;

            var options = Utils.GetOptions(ReturnNullWhenInvalidWarehouseIdIsPassed);

            Utils.SeedDatabase(options);

            using (var context = new StoreSystemDbContext(options))
            {
                var sut = new WarehouseService(context);

                //Act
                var actualWarehouse = await sut.FindWarehouseByIDAsync(validWarehouseID);

                //Assert
                Assert.AreEqual(null, actualWarehouse);
            }
        }
    }
}
