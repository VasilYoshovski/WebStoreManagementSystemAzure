using Microsoft.VisualStudio.TestTools.UnitTesting;
using StoreSystem.Data.DbContext;
using StoreSystem.Data.Models;
using StoreSystem.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace StoreSystem.Tests.Services.WarehouseServiceTests
{
    [TestClass]
    public class CreateWarehouse_Should
    {
        [TestMethod]
        //[DataRow(null, 52, 64, 13, true)]
        //[DataRow("", 52, 64, 13, true)]
        [DataRow("w", 52, 64, 13, true)]
        [DataRow("wa", 52, 64, 13, true)]
        [DataRow("war", 52, 64, 13, true)]
        [DataRow("warehouse15", 52, 64, 13, true)]
        [DataRow("warehouse900", 65, 41, 39, false)]
        public async void CreateWarehouseWhenValidDataIsProvided(string warehouseName, int cityID, int countryID, int addressID, bool toSave)
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
                arrangeContext.SaveChanges();
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
            }
        }

        [TestMethod]
        [DataRow("warehouse1", 5, 4, 3, true)]
        [DataRow("warehouse1", 5, 4, 3, false)]
        public async void CreateWarehouseAndIgnoreValueOfSaveParameter(string warehouseName, int cityID, int countryID, int addressID, bool toSave)
        {
            //Arrange
            var CreateWarehouseAndIgnoreValueOfSaveParameter = System.Reflection.MethodBase.GetCurrentMethod().Name;

            var options = Utils.GetOptions(CreateWarehouseAndIgnoreValueOfSaveParameter);

            Utils.SeedDatabase(options);

            using (var arrangeContext = new StoreSystemDbContext(options))
            {
                arrangeContext.Countries.Add(new Country() { CountryID = countryID });
                arrangeContext.Cities.Add(new City() { CityID = cityID });
                arrangeContext.Addresses.Add(new Address() { AddressID = addressID });
                arrangeContext.SaveChanges();
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
            }
        }

        [TestMethod]
        [DataRow("warehouse81", 5, 4, 3, true)]
        [DataRow("warehouse81", 5, 4, 3, false)]
        public void ThrowsArgumentExceptionWhenWarehouseNameAlreadyExists(string warehouseName, int cityID, int countryID, int addressID, bool toSave)
        {
            //Arrange
            var ThrowsArgumentExceptionWhenWarehouseNameExists = System.Reflection.MethodBase.GetCurrentMethod().Name;

            var options = Utils.GetOptions(ThrowsArgumentExceptionWhenWarehouseNameExists);

            Utils.SeedDatabase(options);

            using (var arrangeContext = new StoreSystemDbContext(options))
            {
                arrangeContext.Warehouses.Add(new Warehouse() { Name = warehouseName });
                arrangeContext.Countries.Add(new Country() { CountryID = countryID });
                arrangeContext.Cities.Add(new City() { CityID = cityID });
                arrangeContext.Addresses.Add(new Address() { AddressID = addressID });
                arrangeContext.SaveChanges();
            }

            using (var context = new StoreSystemDbContext(options))
            {
                var sut = new WarehouseService(context);

                //Act & Assert
                Assert.ThrowsException<ArgumentException>(() => sut.CreateWarehouseAsync(warehouseName, countryID, cityID, addressID, toSave));
            }
        }

        [TestMethod]
        [DataRow("warehouse81", 5, 4, 3, true)]
        [DataRow("warehouse81", 5, 4, 3, false)]
        public void ThrowsArgumentExceptionWhenCountryIDDoesNotExist(string warehouseName, int cityID, int countryID, int addressID, bool toSave)
        {
            //Arrange
            var ThrowsArgumentExceptionWhenCountryIDDoesNotExist = System.Reflection.MethodBase.GetCurrentMethod().Name;

            var options = Utils.GetOptions(ThrowsArgumentExceptionWhenCountryIDDoesNotExist);

            Utils.SeedDatabase(options);

            using (var arrangeContext = new StoreSystemDbContext(options))
            {
                arrangeContext.Cities.Add(new City() { CityID = cityID });
                arrangeContext.Addresses.Add(new Address() { AddressID = addressID });
                arrangeContext.SaveChanges();
            }

            using (var context = new StoreSystemDbContext(options))
            {
                var sut = new WarehouseService(context);

                //Act & Assert
                Assert.ThrowsException<ArgumentException>(() => sut.CreateWarehouseAsync(warehouseName, countryID, cityID, addressID, toSave));
            }
        }

        [TestMethod]
        [DataRow("warehouse81", 5, 4, 3, true)]
        [DataRow("warehouse81", 5, 4, 3, false)]
        public void ThrowsArgumentExceptionWhenCityIDDoesNotExist(string warehouseName, int cityID, int countryID, int addressID, bool toSave)
        {
            //Arrange
            var ThrowsArgumentExceptionWhenCityIDDoesNotExist = System.Reflection.MethodBase.GetCurrentMethod().Name;

            var options = Utils.GetOptions(ThrowsArgumentExceptionWhenCityIDDoesNotExist);

            Utils.SeedDatabase(options);

            using (var arrangeContext = new StoreSystemDbContext(options))
            {
                arrangeContext.Countries.Add(new Country() { CountryID = countryID });
                arrangeContext.Addresses.Add(new Address() { AddressID = addressID });
                arrangeContext.SaveChanges();
            }

            using (var context = new StoreSystemDbContext(options))
            {
                var sut = new WarehouseService(context);

                //Act & Assert
                Assert.ThrowsException<ArgumentException>(() => sut.CreateWarehouseAsync(warehouseName, countryID, cityID, addressID, toSave));
            }
        }

        [TestMethod]
        [DataRow("warehouse81", 5, 4, 3, true)]
        [DataRow("warehouse81", 5, 4, 3, false)]
        public void ThrowsArgumentExceptionWhenAddressIDDoesNotExist(string warehouseName, int cityID, int countryID, int addressID, bool toSave)
        {
            //Arrange
            var ThrowsArgumentExceptionWhenAddressIDDoesNotExist = System.Reflection.MethodBase.GetCurrentMethod().Name;

            var options = Utils.GetOptions(ThrowsArgumentExceptionWhenAddressIDDoesNotExist);

            Utils.SeedDatabase(options);

            using (var arrangeContext = new StoreSystemDbContext(options))
            {
                arrangeContext.Countries.Add(new Country() { CountryID = countryID });
                arrangeContext.Cities.Add(new City() { CityID = cityID });
                arrangeContext.SaveChanges();
            }

            using (var context = new StoreSystemDbContext(options))
            {
                var sut = new WarehouseService(context);

                //Act & Assert
                Assert.ThrowsException<ArgumentException>(() => sut.CreateWarehouseAsync(warehouseName, countryID, cityID, addressID, toSave));
            }
        }

        [TestMethod]
        [DataRow(null, 5, 4, 3, true)]
        [DataRow(null, 5, 4, 3, false)]
        [DataRow("", 5, 4, 3, true)]
        [DataRow("", 5, 4, 3, false)]
        public void ThrowsArgumentExceptionWhenWarehouseNameIsNullOrEmpty(string warehouseName, int cityID, int countryID, int addressID, bool toSave)
        {
            //Arrange
            var ThrowsArgumentExceptionWhenWarehouseNameIsNullOrEmpty = System.Reflection.MethodBase.GetCurrentMethod().Name;

            var options = Utils.GetOptions(ThrowsArgumentExceptionWhenWarehouseNameIsNullOrEmpty);

            Utils.SeedDatabase(options);

            using (var arrangeContext = new StoreSystemDbContext(options))
            {
                //arrangeContext.Warehouses.Add(new Warehouse() { Name = warehouseName });
                arrangeContext.Countries.Add(new Country() { CountryID = countryID });
                arrangeContext.Cities.Add(new City() { CityID = cityID });
                arrangeContext.Addresses.Add(new Address() { AddressID = addressID });
                arrangeContext.SaveChanges();
            }

            using (var context = new StoreSystemDbContext(options))
            {
                var sut = new WarehouseService(context);

                //Act & Assert
                Assert.ThrowsException<ArgumentNullException>(() => sut.CreateWarehouseAsync(warehouseName, countryID, cityID, addressID, toSave));
            }
        }

        [TestMethod]
        [DataRow(null, 5, 4, 3, true)]
        [DataRow(null, 5, 4, 3, false)]
        [DataRow("", 5, 4, 3, true)]
        [DataRow("", 5, 4, 3, false)]
        public void ThrowsArgumentExceptionWhenWarehouseNameIsNullOrEmptyAndCountryIDIsInvalid(string warehouseName, int cityID, int countryID, int addressID, bool toSave)
        {
            //Arrange
            var ThrowsArgumentExceptionWhenWarehouseNameIsNullOrEmptyAndCountryIDIsInvalid = System.Reflection.MethodBase.GetCurrentMethod().Name;

            var options = Utils.GetOptions(ThrowsArgumentExceptionWhenWarehouseNameIsNullOrEmptyAndCountryIDIsInvalid);

            Utils.SeedDatabase(options);

            using (var arrangeContext = new StoreSystemDbContext(options))
            {
                //arrangeContext.Warehouses.Add(new Warehouse() { Name = warehouseName });
                //arrangeContext.Countries.Add(new Country() { CountryID = countryID });
                arrangeContext.Cities.Add(new City() { CityID = cityID });
                arrangeContext.Addresses.Add(new Address() { AddressID = addressID });
                arrangeContext.SaveChanges();
            }

            using (var context = new StoreSystemDbContext(options))
            {
                var sut = new WarehouseService(context);

                //Act & Assert
                Assert.ThrowsException<ArgumentNullException>(() => sut.CreateWarehouseAsync(warehouseName, countryID, cityID, addressID, toSave));
            }
        }

        [TestMethod]
        [DataRow(null, 5, 4, 3, true)]
        [DataRow(null, 5, 4, 3, false)]
        [DataRow("", 5, 4, 3, true)]
        [DataRow("", 5, 4, 3, false)]
        public void ThrowsArgumentExceptionWhenWarehouseNameIsNullOrEmptyAndCityIDIsInvalid(string warehouseName, int cityID, int countryID, int addressID, bool toSave)
        {
            //Arrange
            var ThrowsArgumentExceptionWhenWarehouseNameIsNullOrEmptyAndCityIDIsInvalid = System.Reflection.MethodBase.GetCurrentMethod().Name;

            var options = Utils.GetOptions(ThrowsArgumentExceptionWhenWarehouseNameIsNullOrEmptyAndCityIDIsInvalid);

            Utils.SeedDatabase(options);

            using (var arrangeContext = new StoreSystemDbContext(options))
            {
                //arrangeContext.Warehouses.Add(new Warehouse() { Name = warehouseName });
                arrangeContext.Countries.Add(new Country() { CountryID = countryID });
                //arrangeContext.Cities.Add(new City() { CityID = cityID });
                arrangeContext.Addresses.Add(new Address() { AddressID = addressID });
                arrangeContext.SaveChanges();
            }

            using (var context = new StoreSystemDbContext(options))
            {
                var sut = new WarehouseService(context);

                //Act & Assert
                Assert.ThrowsException<ArgumentNullException>(() => sut.CreateWarehouseAsync(warehouseName, countryID, cityID, addressID, toSave));
            }
        }

        [TestMethod]
        [DataRow(null, 5, 4, 3, true)]
        [DataRow(null, 5, 4, 3, false)]
        [DataRow("", 5, 4, 3, true)]
        [DataRow("", 5, 4, 3, false)]
        public void ThrowsArgumentExceptionWhenWarehouseNameIsNullOrEmptyAndAddressIDIsInvalid(string warehouseName, int cityID, int countryID, int addressID, bool toSave)
        {
            //Arrange
            var ThrowsArgumentExceptionWhenWarehouseNameIsNullOrEmptyAndAddressIDIsInvalid = System.Reflection.MethodBase.GetCurrentMethod().Name;

            var options = Utils.GetOptions(ThrowsArgumentExceptionWhenWarehouseNameIsNullOrEmptyAndAddressIDIsInvalid);

            Utils.SeedDatabase(options);

            using (var arrangeContext = new StoreSystemDbContext(options))
            {
                //arrangeContext.Warehouses.Add(new Warehouse() { Name = warehouseName });
                arrangeContext.Countries.Add(new Country() { CountryID = countryID });
                arrangeContext.Cities.Add(new City() { CityID = cityID });
                //arrangeContext.Addresses.Add(new Address() { AddressID = addressID });
                arrangeContext.SaveChanges();
            }

            using (var context = new StoreSystemDbContext(options))
            {
                var sut = new WarehouseService(context);

                //Act & Assert
                Assert.ThrowsException<ArgumentNullException>(() => sut.CreateWarehouseAsync(warehouseName, countryID, cityID, addressID, toSave));
            }
        }

        [TestMethod]
        [DataRow(" ", 5, 4, 3, true)]
        [DataRow("  ", 5, 4, 3, false)]
        [DataRow("   ", 5, 4, 3, false)]
        [DataRow("    ", 5, 4, 3, false)]
        [DataRow("                                     ", 5, 4, 3, false)]
        public void ThrowsArgumentExceptionWhenWarehouseNameIsWhiteSpase(string warehouseName, int cityID, int countryID, int addressID, bool toSave)
        {
            //Arrange
            var ThrowsArgumentExceptionWhenWarehouseNameIsWhiteSpase = System.Reflection.MethodBase.GetCurrentMethod().Name;

            var options = Utils.GetOptions(ThrowsArgumentExceptionWhenWarehouseNameIsWhiteSpase);

            Utils.SeedDatabase(options);

            using (var arrangeContext = new StoreSystemDbContext(options))
            {
                //arrangeContext.Warehouses.Add(new Warehouse() { Name = warehouseName });
                arrangeContext.Countries.Add(new Country() { CountryID = countryID });
                arrangeContext.Cities.Add(new City() { CityID = cityID });
                arrangeContext.Addresses.Add(new Address() { AddressID = addressID });
                arrangeContext.SaveChanges();
            }

            using (var context = new StoreSystemDbContext(options))
            {
                var sut = new WarehouseService(context);

                //Act & Assert
                Assert.ThrowsException<ArgumentException>(() => sut.CreateWarehouseAsync(warehouseName, countryID, cityID, addressID, toSave));
            }
        }

        [TestMethod]
        [DataRow(" ", 5, 4, 3, true)]
        [DataRow("  ", 5, 4, 3, false)]
        [DataRow("   ", 5, 4, 3, false)]
        [DataRow("    ", 5, 4, 3, false)]
        [DataRow("                                     ", 5, 4, 3, false)]
        public void ThrowsArgumentExceptionWhenWarehouseNameIsWhiteSpaseAndCountryIDIsInvalid(string warehouseName, int cityID, int countryID, int addressID, bool toSave)
        {
            //Arrange
            var ThrowsArgumentExceptionWhenWarehouseNameIsWhiteSpaseAndCountryIDIsInvalid = System.Reflection.MethodBase.GetCurrentMethod().Name;

            var options = Utils.GetOptions(ThrowsArgumentExceptionWhenWarehouseNameIsWhiteSpaseAndCountryIDIsInvalid);

            Utils.SeedDatabase(options);

            using (var arrangeContext = new StoreSystemDbContext(options))
            {
                //arrangeContext.Warehouses.Add(new Warehouse() { Name = warehouseName });
                //arrangeContext.Countries.Add(new Country() { CountryID = countryID });
                arrangeContext.Cities.Add(new City() { CityID = cityID });
                arrangeContext.Addresses.Add(new Address() { AddressID = addressID });
                arrangeContext.SaveChanges();
            }

            using (var context = new StoreSystemDbContext(options))
            {
                var sut = new WarehouseService(context);

                //Act & Assert
                Assert.ThrowsException<ArgumentException>(() => sut.CreateWarehouseAsync(warehouseName, countryID, cityID, addressID, toSave));
            }
        }

        [TestMethod]
        [DataRow(" ", 5, 4, 3, true)]
        [DataRow("  ", 5, 4, 3, false)]
        [DataRow("   ", 5, 4, 3, false)]
        [DataRow("    ", 5, 4, 3, false)]
        [DataRow("                                     ", 5, 4, 3, false)]
        public void ThrowsArgumentExceptionWhenWarehouseNameIsWhiteSpaseAndCityIDIsInvalid(string warehouseName, int cityID, int countryID, int addressID, bool toSave)
        {
            //Arrange
            var ThrowsArgumentExceptionWhenWarehouseNameIsWhiteSpaseAndCityIDIsInvalid = System.Reflection.MethodBase.GetCurrentMethod().Name;

            var options = Utils.GetOptions(ThrowsArgumentExceptionWhenWarehouseNameIsWhiteSpaseAndCityIDIsInvalid);

            Utils.SeedDatabase(options);

            using (var arrangeContext = new StoreSystemDbContext(options))
            {
                //arrangeContext.Warehouses.Add(new Warehouse() { Name = warehouseName });
                arrangeContext.Countries.Add(new Country() { CountryID = countryID });
                //arrangeContext.Cities.Add(new City() { CityID = cityID });
                arrangeContext.Addresses.Add(new Address() { AddressID = addressID });
                arrangeContext.SaveChanges();
            }

            using (var context = new StoreSystemDbContext(options))
            {
                var sut = new WarehouseService(context);

                //Act & Assert
                Assert.ThrowsException<ArgumentException>(() => sut.CreateWarehouseAsync(warehouseName, countryID, cityID, addressID, toSave));
            }
        }

        [TestMethod]
        [DataRow(" ", 5, 4, 3, true)]
        [DataRow("  ", 5, 4, 3, false)]
        [DataRow("   ", 5, 4, 3, false)]
        [DataRow("    ", 5, 4, 3, false)]
        [DataRow("                                     ", 5, 4, 3, false)]
        public void ThrowsArgumentExceptionWhenWarehouseNameIsWhiteSpaseAndAddressIDIsInvalid(string warehouseName, int cityID, int countryID, int addressID, bool toSave)
        {
            //Arrange
            var ThrowsArgumentExceptionWhenWarehouseNameIsWhiteSpaseAndAddressIDIsInvalid = System.Reflection.MethodBase.GetCurrentMethod().Name;

            var options = Utils.GetOptions(ThrowsArgumentExceptionWhenWarehouseNameIsWhiteSpaseAndAddressIDIsInvalid);

            Utils.SeedDatabase(options);

            using (var arrangeContext = new StoreSystemDbContext(options))
            {
                //arrangeContext.Warehouses.Add(new Warehouse() { Name = warehouseName });
                arrangeContext.Countries.Add(new Country() { CountryID = countryID });
                arrangeContext.Cities.Add(new City() { CityID = cityID });
                //arrangeContext.Addresses.Add(new Address() { AddressID = addressID });
                arrangeContext.SaveChanges();
            }

            using (var context = new StoreSystemDbContext(options))
            {
                var sut = new WarehouseService(context);

                //Act & Assert
                Assert.ThrowsException<ArgumentException>(() => sut.CreateWarehouseAsync(warehouseName, countryID, cityID, addressID, toSave));
            }
        }
    }
}
