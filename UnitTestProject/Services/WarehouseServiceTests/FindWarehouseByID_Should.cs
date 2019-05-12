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

namespace StoreSystem.Tests.Services.WarehouseServiceTests
{
    [TestClass]
    public class FindWarehouseByID_Should
    {
        [TestMethod]
        [DataRow(1000)]
        [DataRow(20000)]
        public async void FindWarehouseWhenValidWarehouseIdIsPassed(int validWarehouseID)
        {
            //Arrange
            var FindWarehouseWhenValidWarehouseIdIsPassed = System.Reflection.MethodBase.GetCurrentMethod().Name;

            var options = Utils.GetOptions(FindWarehouseWhenValidWarehouseIdIsPassed);

            Utils.SeedDatabase(options);

            using (var arrangeContext = new StoreSystemDbContext(options))
            {
                arrangeContext.Warehouses.Add(new Warehouse() { WarehouseID = validWarehouseID });
                arrangeContext.SaveChanges();
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
        public void ReturnNullWhenInvalidWarehouseIdIsPassed(int validWarehouseID)
        {
            //Arrange
            var ReturnNullWhenInvalidWarehouseIdIsPassed = System.Reflection.MethodBase.GetCurrentMethod().Name;

            var options = Utils.GetOptions(ReturnNullWhenInvalidWarehouseIdIsPassed);

            Utils.SeedDatabase(options);

            using (var context = new StoreSystemDbContext(options))
            {
                var sut = new WarehouseService(context);

                //Act
                var actualWarehouse = sut.FindWarehouseByIDAsync(validWarehouseID);

                //Assert
                Assert.AreEqual(null, actualWarehouse);
            }
        }
    }
}
