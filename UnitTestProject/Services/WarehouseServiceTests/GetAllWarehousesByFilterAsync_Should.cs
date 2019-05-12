using Microsoft.VisualStudio.TestTools.UnitTesting;
using StoreSystem.Data.DbContext;
using StoreSystem.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreSystem.Tests.Services.WarehouseServiceTests
{
    [TestClass]
    public class GetAllWarehousesByFilterAsync_Should
    {
        [TestMethod]
        [DataRow("")]
        [DataRow("      ")]
        [DataRow("              ")]
        [DataRow(null)]
        public async Task GetAllWarehousesWhenNoneAreRegistred(string filter)
        {
            //Arrange
            var GetAllWarehousesWhenOneIsRegistred = System.Reflection.MethodBase.GetCurrentMethod().Name;

            var options = Utils.GetOptions(GetAllWarehousesWhenOneIsRegistred);

            Utils.SeedDatabase(options);

            using (var arrangeContext = new StoreSystemDbContext(options))
            {
                var warehousesList = arrangeContext.Warehouses.ToList();
                foreach (var item in warehousesList)
                {
                    arrangeContext.Warehouses.Remove(item);
                }
                await arrangeContext.SaveChangesAsync();
            }

            using (var context = new StoreSystemDbContext(options))
            {
                var sut = new WarehouseService(context);

                //Act
                var warehousesList = await sut.GetAllWarehousesByFilterAsync(0, int.MaxValue, filter);

                //Assert
                Assert.AreEqual(0, warehousesList.Count);
            }
        }

        [TestMethod]
        [DataRow("")]
        [DataRow("      ")]
        [DataRow("              ")]
        [DataRow(null)]
        public async Task GetAllWarehousesWhenOneIsRegistred(string filter)
        {
            //Arrange
            var GetAllWarehousesWhenOneIsRegistred = System.Reflection.MethodBase.GetCurrentMethod().Name;

            var options = Utils.GetOptions(GetAllWarehousesWhenOneIsRegistred);

            Utils.SeedDatabase(options);

            using (var arrangeContext = new StoreSystemDbContext(options))
            {
                var warehousesList = arrangeContext.Warehouses.ToList();
                int cnt = 0;
                foreach (var item in warehousesList)
                {
                    if (0 != cnt)
                    {
                        arrangeContext.Warehouses.Remove(item);
                    }
                    else
                    {
                        cnt = 1;
                    }
                }
                await arrangeContext.SaveChangesAsync();
            }

            using (var context = new StoreSystemDbContext(options))
            {
                var sut = new WarehouseService(context);

                //Act
                var warehousesList = await sut.GetAllWarehousesByFilterAsync(0, int.MaxValue, filter);

                //Assert
                Assert.AreEqual(1, warehousesList.Count);
            }
        }

        [TestMethod]
        [DataRow("")]
        [DataRow("      ")]
        [DataRow("              ")]
        [DataRow(null)]
        public async Task GetAllWarehousesWhenTwoAreRegistred(string filter)
        {
            //Arrange
            var GetAllWarehousesWhenOneIsRegistred = System.Reflection.MethodBase.GetCurrentMethod().Name;

            var options = Utils.GetOptions(GetAllWarehousesWhenOneIsRegistred);

            Utils.SeedDatabase(options);

            using (var context = new StoreSystemDbContext(options))
            {
                var sut = new WarehouseService(context);

                //Act
                var warehousesList = await sut.GetAllWarehousesByFilterAsync(0, int.MaxValue, filter);

                //Assert
                Assert.AreEqual(2, warehousesList.Count);
            }
        }

        [TestMethod]
        [DataRow("unexisting filter")]
        public async Task GetAllWarehousesWhenNoneAreRegistredUnexistngFilter(string filter)
        {
            //Arrange
            var GetAllWarehousesWhenOneIsRegistred = System.Reflection.MethodBase.GetCurrentMethod().Name;

            var options = Utils.GetOptions(GetAllWarehousesWhenOneIsRegistred);

            Utils.SeedDatabase(options);

            using (var arrangeContext = new StoreSystemDbContext(options))
            {
                var warehousesList = arrangeContext.Warehouses.ToList();
                foreach (var item in warehousesList)
                {
                    arrangeContext.Warehouses.Remove(item);
                }
                await arrangeContext.SaveChangesAsync();
            }

            using (var context = new StoreSystemDbContext(options))
            {
                var sut = new WarehouseService(context);

                //Act
                var warehousesList = await sut.GetAllWarehousesByFilterAsync(0, int.MaxValue, filter);

                //Assert
                Assert.AreEqual(0, warehousesList.Count);
            }
        }

        [TestMethod]
        [DataRow("unexisting filter")]
        public async Task GetAllWarehousesWhenOneIsRegistredUnexistngFilter(string filter)
        {
            //Arrange
            var GetAllWarehousesWhenOneIsRegistred = System.Reflection.MethodBase.GetCurrentMethod().Name;

            var options = Utils.GetOptions(GetAllWarehousesWhenOneIsRegistred);

            Utils.SeedDatabase(options);

            using (var arrangeContext = new StoreSystemDbContext(options))
            {
                var warehousesList = arrangeContext.Warehouses.ToList();
                int cnt = 0;
                foreach (var item in warehousesList)
                {
                    if (0 != cnt)
                    {
                        arrangeContext.Warehouses.Remove(item);
                    }
                    else
                    {
                        cnt = 1;
                    }
                }
                await arrangeContext.SaveChangesAsync();
            }

            using (var context = new StoreSystemDbContext(options))
            {
                var sut = new WarehouseService(context);

                //Act
                var warehousesList = await sut.GetAllWarehousesByFilterAsync(0, int.MaxValue, filter);

                //Assert
                Assert.AreEqual(0, warehousesList.Count);
            }
        }

        [TestMethod]
        [DataRow("unexisting filter")]
        public async Task GetAllWarehousesWhenTwoAreRegistredUnexistngFilter(string filter)
        {
            //Arrange
            var GetAllWarehousesWhenOneIsRegistred = System.Reflection.MethodBase.GetCurrentMethod().Name;

            var options = Utils.GetOptions(GetAllWarehousesWhenOneIsRegistred);

            Utils.SeedDatabase(options);

            using (var context = new StoreSystemDbContext(options))
            {
                var sut = new WarehouseService(context);

                //Act
                var warehousesList = await sut.GetAllWarehousesByFilterAsync(0, int.MaxValue, filter);

                //Assert
                Assert.AreEqual(0, warehousesList.Count);
            }
        }

        [TestMethod]
        [DataRow("ware")]
        public async Task GetAllWarehousesWhenNoneAreRegistredExistngFilter(string filter)
        {
            //Arrange
            var GetAllWarehousesWhenOneIsRegistred = System.Reflection.MethodBase.GetCurrentMethod().Name;

            var options = Utils.GetOptions(GetAllWarehousesWhenOneIsRegistred);

            Utils.SeedDatabase(options);

            using (var arrangeContext = new StoreSystemDbContext(options))
            {
                var warehousesList = arrangeContext.Warehouses.ToList();
                foreach (var item in warehousesList)
                {
                    arrangeContext.Warehouses.Remove(item);
                }
                await arrangeContext.SaveChangesAsync();
            }

            using (var context = new StoreSystemDbContext(options))
            {
                var sut = new WarehouseService(context);

                //Act
                var warehousesList = await sut.GetAllWarehousesByFilterAsync(0, int.MaxValue, filter);

                //Assert
                Assert.AreEqual(0, warehousesList.Count);
            }
        }

        [TestMethod]
        [DataRow("ware")]
        public async Task GetAllWarehousesWhenOneIsRegistredExistngFilter(string filter)
        {
            //Arrange
            var GetAllWarehousesWhenOneIsRegistred = System.Reflection.MethodBase.GetCurrentMethod().Name;

            var options = Utils.GetOptions(GetAllWarehousesWhenOneIsRegistred);

            Utils.SeedDatabase(options);

            using (var arrangeContext = new StoreSystemDbContext(options))
            {
                var warehousesList = arrangeContext.Warehouses.ToList();
                int cnt = 0;
                foreach (var item in warehousesList)
                {
                    if (0 != cnt)
                    {
                        arrangeContext.Warehouses.Remove(item);
                    }
                    else
                    {
                        cnt = 1;
                    }
                }
                await arrangeContext.SaveChangesAsync();
            }

            using (var context = new StoreSystemDbContext(options))
            {
                var sut = new WarehouseService(context);

                //Act
                var warehousesList = await sut.GetAllWarehousesByFilterAsync(0, int.MaxValue, filter);

                //Assert
                Assert.AreEqual(1, warehousesList.Count);
            }
        }

        [TestMethod]
        [DataRow("ware")]
        public async Task GetAllWarehousesWhenTwoAreRegistredExistngFilter(string filter)
        {
            //Arrange
            var GetAllWarehousesWhenOneIsRegistred = System.Reflection.MethodBase.GetCurrentMethod().Name;

            var options = Utils.GetOptions(GetAllWarehousesWhenOneIsRegistred);

            Utils.SeedDatabase(options);

            using (var context = new StoreSystemDbContext(options))
            {
                var sut = new WarehouseService(context);

                //Act
                var warehousesList = await sut.GetAllWarehousesByFilterAsync(0, int.MaxValue, filter);

                //Assert
                Assert.AreEqual(2, warehousesList.Count);
            }
        }
    }
}
