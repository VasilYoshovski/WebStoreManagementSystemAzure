using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StoreSystem.Data.DbContext;
using StoreSystem.Data.Models;
using StoreSystem.Services;
using StoreSystem.Tests.Services.AddressServiceTests.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StoreSystem.Tests.Services.AddressServiceTests
{
    [TestClass]
    public class AddAddressByName_Should
    {
        [TestMethod]
        public void AddRecord_WhenValidAddressTextAndSaveTrueIsPassed()
        {
            var databaseName = System.Reflection.MethodBase.GetCurrentMethod().Name;

            using (var assertContext = new StoreSystemDbContext(DbSeedAddress.GetOptions(databaseName)))
            {
                const string validAddressText = "valid address";
                var sut = new AddressService(assertContext);
                var actual = sut.CreateAddressAsync(validAddressText, true);
                Assert.IsTrue(assertContext.Addresses.All(x => x.Name == validAddressText));
                Assert.IsTrue(assertContext.Addresses.Count() == 1);
                Assert.AreEqual<string>(validAddressText, assertContext.Addresses.Single(x => x.Name == validAddressText).Name);
            }
        }

        [TestMethod]
        public void ReturnAddressFromDB_WhenExistingAddressTextIsPassed()
        {
            var databaseName = System.Reflection.MethodBase.GetCurrentMethod().Name;
            var options = DbSeed.GetOptions(databaseName);
            DbSeed.SeedDatabase(options);
            using (var assertContext = new StoreSystemDbContext(options))
            {
                const string existingAddressText = "valid address 1";
                var sut = new AddressService(assertContext);
                var a = sut.CreateAddressAsync(existingAddressText);
                Assert.IsTrue(assertContext.Addresses.SingleOrDefault(x => x.Name == existingAddressText) != null);
                var t = assertContext.Addresses.Single(x => x.Name == existingAddressText);
                var v = assertContext.Addresses.Count();
                Assert.IsTrue(assertContext.Addresses.Count() == 2);
            }
        }

        [TestMethod]
        public void MarkRecordForAdding_WhenValidAddressTextAndSaveFalseIsPassed()
        {
            var databaseName = System.Reflection.MethodBase.GetCurrentMethod().Name;
            var options = DbSeed.GetOptions(databaseName);

            using (var assertContext = new StoreSystemDbContext(options))
            {
                const string validAddressText = "valid address";
                var sut = new AddressService(assertContext);
                var actual = sut.CreateAddressAsync(validAddressText, false).Result;
                
                Assert.IsTrue(assertContext.Entry<Address>(actual).State == EntityState.Added);
                Assert.IsTrue(assertContext.Addresses.Count() == 0);
            }
        }

        public void AddRecord_WhenValidAddressTextIsPassed3()
        {
            //var databaseName = System.Reflection.MethodBase.GetCurrentMethod().Name;
            //DbSeed.SeedDatabase(nameof(databaseName));
            //using (var assertContext = DbSeed.GetOptions(databaseName))
            //{

            //}
        }
    }
}
