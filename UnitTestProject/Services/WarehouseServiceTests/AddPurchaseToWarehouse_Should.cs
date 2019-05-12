﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using StoreSystem.Data.DbContext;
using StoreSystem.Data.Models;
using StoreSystem.Services;
using System;
using System.Linq;

namespace StoreSystem.Tests.Services.WarehouseServiceTests
{
    [TestClass]
    public class AddPurchaseToWarehouse_Should
    {
        [TestMethod]
        [DataRow(10, 7)]
        [DataRow(61, 298)]
        public void AddPurchaseWhenPurchaseIDAndWarehouseIDExist(int validWarehouseID, int validPurchaseID)
        {
            //Arrange
            var AddPurchaseWhenPurchaseIDAndWarehouseIDExist = System.Reflection.MethodBase.GetCurrentMethod().Name;

            var options = Utils.GetOptions(AddPurchaseWhenPurchaseIDAndWarehouseIDExist);

            Utils.SeedDatabase(options);

            using (var arrangeContext = new StoreSystemDbContext(options))
            {
                arrangeContext.Warehouses.Add(new Warehouse() { WarehouseID = validWarehouseID });
                arrangeContext.Purchases.Add(new Purchase() { PurchaseID = validPurchaseID });
                arrangeContext.SaveChanges();
            }

            using (var context = new StoreSystemDbContext(options))
            {
                var sut = new WarehouseService(context);

                //Act
                var isAdded = sut.AddPurchaseToWarehouse(validWarehouseID, validPurchaseID);
                var addedPurchase = context.Warehouses.Find(validWarehouseID).Purchases
                    .Where(x => x.PurchaseID == validPurchaseID)
                    .FirstOrDefault();

                //Assert
                Assert.AreEqual(true, isAdded);
                Assert.AreEqual(validPurchaseID, addedPurchase.PurchaseID);
            }
        }

        [TestMethod]
        [DataRow(120, -7)]
        [DataRow(134, 0)]
        [DataRow(10, 7)]
        [DataRow(61, 298)]
        public void ThrowsArgumentExceptionWhenPurchaseIDDoesNotExist(int validWarehouseID, int validPurchaseID)
        {
               //Arrange
               var ThrowsArgumentExceptionWhenPurchaseIDDoesNotExist = System.Reflection.MethodBase.GetCurrentMethod().Name;

            var options = Utils.GetOptions(ThrowsArgumentExceptionWhenPurchaseIDDoesNotExist);

            Utils.SeedDatabase(options);

            using (var arrangeContext = new StoreSystemDbContext(options))
            {
                arrangeContext.Warehouses.Add(new Warehouse() { WarehouseID = validWarehouseID });
                arrangeContext.SaveChanges();
            }

            using (var context = new StoreSystemDbContext(options))
            {
                var sut = new WarehouseService(context);

                //Act & Assert
                Assert.ThrowsException<ArgumentException>(() => sut.AddPurchaseToWarehouse(validWarehouseID, validPurchaseID));
            }
        }

        [TestMethod]
        [DataRow(-214, 7)]
        [DataRow(0, 7)]
        [DataRow(10, 7)]
        [DataRow(61, 298)]
        public void ThrowsArgumentExceptionWhenWarehouseIDDoesNotExist(int validWarehouseID, int validPurchaseID)
        {
            //Arrange
            var ThrowsArgumentExceptionWhenWarehouseIDDoesNotExist = System.Reflection.MethodBase.GetCurrentMethod().Name;

            var options = Utils.GetOptions(ThrowsArgumentExceptionWhenWarehouseIDDoesNotExist);

            Utils.SeedDatabase(options);

            using (var arrangeContext = new StoreSystemDbContext(options))
            {
                arrangeContext.Purchases.Add(new Purchase() { PurchaseID = validPurchaseID });
                arrangeContext.SaveChanges();
            }

            using (var context = new StoreSystemDbContext(options))
            {
                var sut = new WarehouseService(context);

                //Act & Assert
                Assert.ThrowsException<ArgumentException>(() => sut.AddPurchaseToWarehouse(validWarehouseID, validPurchaseID));
            }
        }

        [TestMethod]
        [DataRow(-214, 7)]
        [DataRow(0, 0)]
        [DataRow(0, 7)]
        [DataRow(10, -47)]
        [DataRow(61, 0)]
        public void ThrowsArgumentExceptionWhenNeitherPurchaseIDNorWarehouseIDExist(int validWarehouseID, int validPurchaseID)
        {
            //Arrange
            var ThrowsArgumentExceptionWhenNeitherPurchaseIDNorWarehouseIDExist = System.Reflection.MethodBase.GetCurrentMethod().Name;

            var options = Utils.GetOptions(ThrowsArgumentExceptionWhenNeitherPurchaseIDNorWarehouseIDExist);

            Utils.SeedDatabase(options);

            using (var context = new StoreSystemDbContext(options))
            {
                var sut = new WarehouseService(context);

                //Act & Assert
                Assert.ThrowsException<ArgumentException>(() => sut.AddPurchaseToWarehouse(validWarehouseID, validPurchaseID));
            }
        }
    }
}
