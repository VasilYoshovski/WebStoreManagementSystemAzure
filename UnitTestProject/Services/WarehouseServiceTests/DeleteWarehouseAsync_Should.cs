using Microsoft.VisualStudio.TestTools.UnitTesting;
using StoreSystem.Data.DbContext;
using StoreSystem.Data.Models;
using StoreSystem.Services;
using System;
using System.Threading.Tasks;

namespace StoreSystem.Tests.Services.WarehouseServiceTests
{
    [TestClass]
    public class DeleteWarehouseAsync_Should
    {
        [TestMethod]
        [DataRow("w", 52, 64, 13, true)]
        [DataRow("wa", 52, 64, 13, true)]
        [DataRow("war", 52, 64, 13, true)]
        [DataRow("warehouse15", 52, 64, 13, true)]
        [DataRow("warehouse900", 65, 41, 39, false)]
        public async Task DeleteWarehouseWhenValidID(string warehouseName, int cityID, int countryID, int addressID, bool toSave)
        {
            //Arrange
            var FindWarehouseWhenValidWarehouseIdIsPassed = System.Reflection.MethodBase.GetCurrentMethod().Name;

            var options = Utils.GetOptions(FindWarehouseWhenValidWarehouseIdIsPassed);

            Utils.SeedDatabase(options);

            using (var arrangeContext = new StoreSystemDbContext(options))
            {
                arrangeContext.Countries.Add(new Country() { CountryID = countryID });
                arrangeContext.Cities.Add(new City() { CityID = cityID });
                arrangeContext.Addresses.Add(new Address() { AddressID = addressID });
                await arrangeContext.SaveChangesAsync();
            }

            using (var context = new StoreSystemDbContext(options))
            {
                var sut = new WarehouseService(context);

                //Act
                var actualWarehouse = await sut.CreateWarehouseAsync(warehouseName, countryID, cityID, addressID, toSave);

                //Assert
                Assert.AreNotEqual(null, actualWarehouse);
                Assert.AreEqual(warehouseName, actualWarehouse.Name);
                Assert.AreEqual(countryID, actualWarehouse.CountryID);
                Assert.AreEqual(cityID, actualWarehouse.CityID);
                Assert.AreEqual(addressID, actualWarehouse.AddressID);

                var deleteResult = await sut.DeleteWarehouseAsync(actualWarehouse.WarehouseID);

                Assert.AreEqual(true, deleteResult);
            }
        }

        [TestMethod]
        [DataRow("w", 52, 64, 13, true)]
        [DataRow("wa", 52, 64, 13, true)]
        [DataRow("war", 52, 64, 13, true)]
        [DataRow("warehouse15", 52, 64, 13, true)]
        [DataRow("warehouse900", 65, 41, 39, false)]
        public async Task DeleteWarehouseWhenInvalidID(string warehouseName, int cityID, int countryID, int addressID, bool toSave)
        {
            //Arrange
            var FindWarehouseWhenValidWarehouseIdIsPassed = System.Reflection.MethodBase.GetCurrentMethod().Name;

            var options = Utils.GetOptions(FindWarehouseWhenValidWarehouseIdIsPassed);

            Utils.SeedDatabase(options);

            using (var arrangeContext = new StoreSystemDbContext(options))
            {
                arrangeContext.Countries.Add(new Country() { CountryID = countryID });
                arrangeContext.Cities.Add(new City() { CityID = cityID });
                arrangeContext.Addresses.Add(new Address() { AddressID = addressID });
                await arrangeContext.SaveChangesAsync();
            }

            using (var context = new StoreSystemDbContext(options))
            {
                var sut = new WarehouseService(context);

                //Act
                var actualWarehouse = await sut.CreateWarehouseAsync(warehouseName, countryID, cityID, addressID, toSave);

                //Assert
                Assert.AreNotEqual(null, actualWarehouse);
                Assert.AreEqual(warehouseName, actualWarehouse.Name);
                Assert.AreEqual(countryID, actualWarehouse.CountryID);
                Assert.AreEqual(cityID, actualWarehouse.CityID);
                Assert.AreEqual(addressID, actualWarehouse.AddressID);

                await Assert.ThrowsExceptionAsync<ArgumentException>(async () => await sut.DeleteWarehouseAsync(actualWarehouse.WarehouseID + 100));
            }
        }
    }
}
