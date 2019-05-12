using Microsoft.VisualStudio.TestTools.UnitTesting;
using StoreSystem.Data.DbContext;
using StoreSystem.Data.Models;
using StoreSystem.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace StoreSystem.Tests.Services.WarehouseServiceTests
{
    [TestClass]
    public class FindWarehouseByName_Should
    {
        [TestMethod]
        [DataRow("Warehouse1")]
        [DataRow("Warehouse2")]
        public async void FindWarehouseWhenValidWarehouseNamePassed(string validWarehouseName)
        {
            //Arrange
            var FindWarehouseWhenValidWarehouseNamePassed = System.Reflection.MethodBase.GetCurrentMethod().Name;

            var options = Utils.GetOptions(FindWarehouseWhenValidWarehouseNamePassed);

            Utils.SeedDatabase(options);

            using (var arrangeContext = new StoreSystemDbContext(options))
            {
                arrangeContext.Warehouses.Add(new Warehouse() { Name = validWarehouseName });
                arrangeContext.SaveChanges();
            }

            using (var context = new StoreSystemDbContext(options))
            {
                var sut = new WarehouseService(context);

                //Act
                var actualWarehouse = await sut.FindWarehouseByNameAsync(validWarehouseName);

                //Assert
                Assert.AreEqual(validWarehouseName, actualWarehouse.Name);
            }
        }

        [TestMethod]
        [DataRow("Warehouse1")]
        [DataRow("Warehouse2")]
        public void ReturnNullWhenInvalidWarehouseNameIsPassed(string validWarehouseName)
        {
            //Arrange
            var ReturnNullWhenInvalidWarehouseNameIsPassed = System.Reflection.MethodBase.GetCurrentMethod().Name;

            var options = Utils.GetOptions(ReturnNullWhenInvalidWarehouseNameIsPassed);

            Utils.SeedDatabase(options);

            using (var arrangeContext = new StoreSystemDbContext(options))
            {
                arrangeContext.Warehouses.Add(new Warehouse() { Name = "fakeName" });
                arrangeContext.SaveChanges();
            }

            using (var context = new StoreSystemDbContext(options))
            {
                var sut = new WarehouseService(context);

                //Act
                var actualWarehouse = sut.FindWarehouseByNameAsync(validWarehouseName);

                //Assert
                Assert.AreEqual(null, actualWarehouse);
            }
        }
    }
}
