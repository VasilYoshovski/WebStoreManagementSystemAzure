﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using StoreSystem.Data.DbContext;
using StoreSystem.Data.Models;
using StoreSystem.Services;
using System;

namespace StoreSystem.Tests.Services.WarehouseServiceTests
{
    [TestClass]
    public class GetWarehouseByName_Should
    {
        [TestMethod]
        [DataRow("Warehouse1")]
        [DataRow("Warehouse2")]
        public async void GetWarehouseWhenValidWarehouseNamePassed(string validWarehouseName)
        {
            //Arrange
            var GetWarehouseWhenValidWarehouseNamePassed = System.Reflection.MethodBase.GetCurrentMethod().Name;

            var options = Utils.GetOptions(GetWarehouseWhenValidWarehouseNamePassed);

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
                var actualWarehouse = await sut.GetWarehouseByNameAsync(validWarehouseName);

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

            //Act & Assert
            using (var context = new StoreSystemDbContext(options))
            {
                var sut = new WarehouseService(context);
                Assert.ThrowsException<ArgumentException>(() => sut.GetWarehouseByNameAsync(validWarehouseName));
            }
        }
    }
}
