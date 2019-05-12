using Microsoft.VisualStudio.TestTools.UnitTesting;
using StoreSystem.Data.DbContext;
using StoreSystem.Services;
using System;

namespace StoreSystem.Tests.Services.WarehouseServiceTests
{
    [TestClass]
    public class WarehouseServiceConstructor_Should
    {
        [TestMethod]
        public void CreateInstanceWhenContextIsValid()
        {
            //Arrange
            var CreateInstanceWhenContextIsValid = System.Reflection.MethodBase.GetCurrentMethod().Name;

            var options = Utils.GetOptions(CreateInstanceWhenContextIsValid);

            Utils.SeedDatabase(options);

            //Act & Assert
            using (var context = new StoreSystemDbContext(options))
            {
                Assert.AreNotEqual(null, new WarehouseService(context));
            }
        }

        [TestMethod]
        public void ThrowsArgumentNullException()
        {
            //Arrange
            var ThrowsArgumentNullException = System.Reflection.MethodBase.GetCurrentMethod().Name;

            var options = Utils.GetOptions(ThrowsArgumentNullException);

            Utils.SeedDatabase(options);

            //Act & Assert
            using (var context = new StoreSystemDbContext(options))
            {
                Assert.ThrowsException<ArgumentNullException>(() => new WarehouseService(null));
            }
        }
    }
}
