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
    public class UpdateWarehouseAsync_Should
    {
        [TestMethod]
        [DataRow("wdfgdfggfd", 52, 64, 13, true)]
        [DataRow("wajkgkjkjkjkjk", 52, 64, 13, true)]
        [DataRow("warlgkljkljkljk", 52, 64, 13, true)]
        [DataRow("warehouse15", 52, 64, 13, true)]
        [DataRow("warehouse900", 65, 41, 39, false)]
        public async Task UpdateWarehouseNameWhenValidID(string warehouseName, int cityID, int countryID, int addressID, bool toSave)
        {
            //Arrange
            var UpdateWarehouseNameWhenValidID = System.Reflection.MethodBase.GetCurrentMethod().Name;

            var options = Utils.GetOptions(UpdateWarehouseNameWhenValidID);

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

                Assert.AreNotEqual(null, actualWarehouse);
                Assert.AreEqual(warehouseName, actualWarehouse.Name);
                Assert.AreEqual(countryID, actualWarehouse.CountryID);
                Assert.AreEqual(cityID, actualWarehouse.CityID);
                Assert.AreEqual(addressID, actualWarehouse.AddressID);

                var updatedWarehouse = await sut.UpdateWarehouseAsync(actualWarehouse.WarehouseID, warehouseName + "updated", countryID, cityID, addressID, toSave);

                //Assert
                Assert.AreEqual(addressID, updatedWarehouse.AddressID);
                Assert.AreEqual(countryID, updatedWarehouse.CountryID);
                Assert.AreEqual(cityID, updatedWarehouse.CityID);
                Assert.AreEqual(warehouseName + "updated", updatedWarehouse.Name);
            }
        }

        [TestMethod]
        [DataRow("wdfgdfggfd", 152, 164, 113, true)]
        [DataRow("wajkgkjkjkjkjk", 152, 164, 113, true)]
        [DataRow("warlgkljkljkljk", 152, 164, 113, true)]
        [DataRow("warehouse15", 152, 164, 113, true)]
        [DataRow("warehouse900", 165, 141, 139, false)]
        public async Task UpdateWarehouseCityWhenValidID(string warehouseName, int cityID, int countryID, int addressID, bool toSave)
        {
            //Arrange
            var UpdateWarehouseCityWhenValidID = System.Reflection.MethodBase.GetCurrentMethod().Name;

            var options = Utils.GetOptions(UpdateWarehouseCityWhenValidID);

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

                Assert.AreNotEqual(null, actualWarehouse);
                Assert.AreEqual(warehouseName, actualWarehouse.Name);
                Assert.AreEqual(countryID, actualWarehouse.CountryID);
                Assert.AreEqual(cityID, actualWarehouse.CityID);
                Assert.AreEqual(addressID, actualWarehouse.AddressID);

                var updatedWarehouse = await sut.UpdateWarehouseAsync(actualWarehouse.WarehouseID, warehouseName, countryID, 1, addressID, toSave);

                //Assert
                Assert.AreEqual(addressID, updatedWarehouse.AddressID);
                Assert.AreEqual(countryID, updatedWarehouse.CountryID);
                Assert.AreEqual(1, updatedWarehouse.CityID);
                Assert.AreEqual(warehouseName, updatedWarehouse.Name);
            }
        }

        [TestMethod]
        [DataRow("wdfgdfggfd", 152, 164, 113, true)]
        [DataRow("wajkgkjkjkjkjk", 152, 164, 113, true)]
        [DataRow("warlgkljkljkljk", 152, 164, 113, true)]
        [DataRow("warehouse15", 152, 164, 113, true)]
        [DataRow("warehouse900", 165, 141, 139, false)]
        public async Task UpdateWarehouseCountryWhenValidID(string warehouseName, int cityID, int countryID, int addressID, bool toSave)
        {
            //Arrange
            var UpdateWarehouseCountryWhenValidID = System.Reflection.MethodBase.GetCurrentMethod().Name;

            var options = Utils.GetOptions(UpdateWarehouseCountryWhenValidID);

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

                Assert.AreNotEqual(null, actualWarehouse);
                Assert.AreEqual(warehouseName, actualWarehouse.Name);
                Assert.AreEqual(countryID, actualWarehouse.CountryID);
                Assert.AreEqual(cityID, actualWarehouse.CityID);
                Assert.AreEqual(addressID, actualWarehouse.AddressID);

                var updatedWarehouse = await sut.UpdateWarehouseAsync(actualWarehouse.WarehouseID, warehouseName, 1, cityID, addressID, toSave);

                //Assert
                Assert.AreEqual(addressID, updatedWarehouse.AddressID);
                Assert.AreEqual(1, updatedWarehouse.CountryID);
                Assert.AreEqual(cityID, updatedWarehouse.CityID);
                Assert.AreEqual(warehouseName, updatedWarehouse.Name);
            }
        }

        [TestMethod]
        [DataRow("wdfgdfggfd", 152, 164, 113, true)]
        [DataRow("wajkgkjkjkjkjk", 152, 164, 113, true)]
        [DataRow("warlgkljkljkljk", 152, 164, 113, true)]
        [DataRow("warehouse15", 152, 164, 113, true)]
        [DataRow("warehouse900", 165, 141, 139, false)]
        public async Task UpdateWarehouseAddressWhenValidID(string warehouseName, int cityID, int countryID, int addressID, bool toSave)
        {
            //Arrange
            var UpdateWarehouseAddressWhenValidID = System.Reflection.MethodBase.GetCurrentMethod().Name;

            var options = Utils.GetOptions(UpdateWarehouseAddressWhenValidID);

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

                Assert.AreNotEqual(null, actualWarehouse);
                Assert.AreEqual(warehouseName, actualWarehouse.Name);
                Assert.AreEqual(countryID, actualWarehouse.CountryID);
                Assert.AreEqual(cityID, actualWarehouse.CityID);
                Assert.AreEqual(addressID, actualWarehouse.AddressID);

                var updatedWarehouse = await sut.UpdateWarehouseAsync(actualWarehouse.WarehouseID, warehouseName, countryID, cityID, 1, toSave);

                //Assert
                Assert.AreEqual(1, updatedWarehouse.AddressID);
                Assert.AreEqual(countryID, updatedWarehouse.CountryID);
                Assert.AreEqual(cityID, updatedWarehouse.CityID);
                Assert.AreEqual(warehouseName, updatedWarehouse.Name);
            }
        }

        [TestMethod]
        [DataRow("wdfgdfggfd", 152, 164, 113, true)]
        [DataRow("wajkgkjkjkjkjk", 152, 164, 113, true)]
        [DataRow("warlgkljkljkljk", 152, 164, 113, true)]
        [DataRow("warehouse15", 152, 164, 113, true)]
        [DataRow("warehouse900", 165, 141, 139, false)]
        public async Task UpdateWarehouseFullyWhenValidID(string warehouseName, int cityID, int countryID, int addressID, bool toSave)
        {
            //Arrange
            var UpdateWarehouseFullyWhenValidID = System.Reflection.MethodBase.GetCurrentMethod().Name;

            var options = Utils.GetOptions(UpdateWarehouseFullyWhenValidID);

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

                Assert.AreNotEqual(null, actualWarehouse);
                Assert.AreEqual(warehouseName, actualWarehouse.Name);
                Assert.AreEqual(countryID, actualWarehouse.CountryID);
                Assert.AreEqual(cityID, actualWarehouse.CityID);
                Assert.AreEqual(addressID, actualWarehouse.AddressID);

                var updatedWarehouse = await sut.UpdateWarehouseAsync(actualWarehouse.WarehouseID, warehouseName + "sdd", 1, 1, 1, toSave);

                //Assert
                Assert.AreEqual(1, updatedWarehouse.AddressID);
                Assert.AreEqual(1, updatedWarehouse.CountryID);
                Assert.AreEqual(1, updatedWarehouse.CityID);
                Assert.AreEqual(warehouseName + "sdd", updatedWarehouse.Name);
            }
        }

        [TestMethod]
        [DataRow("wdfgdfggfd", 52, 64, 13, true)]
        [DataRow("wajkgkjkjkjkjk", 52, 64, 13, true)]
        [DataRow("warlgkljkljkljk", 52, 64, 13, true)]
        [DataRow("warehouse15", 52, 64, 13, true)]
        [DataRow("warehouse900", 65, 41, 39, false)]
        public async Task UpdateWarehouseWithNullNameWhenValidID(string warehouseName, int cityID, int countryID, int addressID, bool toSave)
        {
            //Arrange
            var UpdateWarehouseWithInvalidNameWhenValidID = System.Reflection.MethodBase.GetCurrentMethod().Name;

            var options = Utils.GetOptions(UpdateWarehouseWithInvalidNameWhenValidID);

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

                Assert.AreNotEqual(null, actualWarehouse);
                Assert.AreEqual(warehouseName, actualWarehouse.Name);
                Assert.AreEqual(countryID, actualWarehouse.CountryID);
                Assert.AreEqual(cityID, actualWarehouse.CityID);
                Assert.AreEqual(addressID, actualWarehouse.AddressID);

                //Assert
                await Assert.ThrowsExceptionAsync<ArgumentException>(async () => await sut.UpdateWarehouseAsync(actualWarehouse.WarehouseID, null, countryID, cityID, addressID, toSave));
            }
        }

        [TestMethod]
        [DataRow("wdfgdfggfd", 52, 64, 13, true)]
        [DataRow("wajkgkjkjkjkjk", 52, 64, 13, true)]
        [DataRow("warlgkljkljkljk", 52, 64, 13, true)]
        [DataRow("warehouse15", 52, 64, 13, true)]
        [DataRow("warehouse900", 65, 41, 39, false)]
        public async Task UpdateWarehouseWithWhiteSpaceNameWhenValidID(string warehouseName, int cityID, int countryID, int addressID, bool toSave)
        {
            //Arrange
            var UpdateWarehouseWithWhiteSpaceNameWhenValidID = System.Reflection.MethodBase.GetCurrentMethod().Name;

            var options = Utils.GetOptions(UpdateWarehouseWithWhiteSpaceNameWhenValidID);

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
                await Assert.ThrowsExceptionAsync<ArgumentException>(async () => await sut.UpdateWarehouseAsync(actualWarehouse.WarehouseID, "                  ", countryID, cityID, addressID, toSave));
            }
        }

        [TestMethod]
        [DataRow("wdfgdfggfd", 52, 64, 13, true)]
        [DataRow("wajkgkjkjkjkjk", 52, 64, 13, true)]
        [DataRow("warlgkljkljkljk", 52, 64, 13, true)]
        [DataRow("warehouse15", 52, 64, 13, true)]
        [DataRow("warehouse900", 65, 41, 39, false)]
        public async Task UpdateWarehouseWithEmptyNameWhenValidID(string warehouseName, int cityID, int countryID, int addressID, bool toSave)
        {
            //Arrange
            var UpdateWarehouseWithEmptyNameWhenValidID = System.Reflection.MethodBase.GetCurrentMethod().Name;

            var options = Utils.GetOptions(UpdateWarehouseWithEmptyNameWhenValidID);

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
                await Assert.ThrowsExceptionAsync<ArgumentException>(async () => await sut.UpdateWarehouseAsync(actualWarehouse.WarehouseID, "", countryID, cityID, addressID, toSave));
            }
        }

        [TestMethod]
        [DataRow("wdfgdfggfd", 152, 164, 113, true)]
        [DataRow("wajkgkjkjkjkjk", 152, 164, 113, true)]
        [DataRow("warlgkljkljkljk", 152, 164, 113, true)]
        [DataRow("warehouse15", 152, 164, 113, true)]
        [DataRow("warehouse900", 165, 141, 139, false)]
        public async Task UpdateWarehouseWithInvalidCityWhenValidID(string warehouseName, int cityID, int countryID, int addressID, bool toSave)
        {
            //Arrange
            var UpdateWarehouseWithInvalidCityWhenValidID = System.Reflection.MethodBase.GetCurrentMethod().Name;

            var options = Utils.GetOptions(UpdateWarehouseWithInvalidCityWhenValidID);

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

                Assert.AreNotEqual(null, actualWarehouse);
                Assert.AreEqual(warehouseName, actualWarehouse.Name);
                Assert.AreEqual(countryID, actualWarehouse.CountryID);
                Assert.AreEqual(cityID, actualWarehouse.CityID);
                Assert.AreEqual(addressID, actualWarehouse.AddressID);

                //Assert
                await Assert.ThrowsExceptionAsync<ArgumentException>(async () => await sut.UpdateWarehouseAsync(actualWarehouse.WarehouseID, warehouseName, countryID, -31000, addressID, toSave));
            }
        }

        [TestMethod]
        [DataRow("wdfgdfggfd", 152, 164, 113, true)]
        [DataRow("wajkgkjkjkjkjk", 152, 164, 113, true)]
        [DataRow("warlgkljkljkljk", 152, 164, 113, true)]
        [DataRow("warehouse15", 152, 164, 113, true)]
        [DataRow("warehouse900", 165, 141, 139, false)]
        public async Task UpdateWarehouseWithInvalidCountryWhenValidID(string warehouseName, int cityID, int countryID, int addressID, bool toSave)
        {
            //Arrange
            var UpdateWarehouseWithInvalidCountryWhenValidID = System.Reflection.MethodBase.GetCurrentMethod().Name;

            var options = Utils.GetOptions(UpdateWarehouseWithInvalidCountryWhenValidID);

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

                Assert.AreNotEqual(null, actualWarehouse);
                Assert.AreEqual(warehouseName, actualWarehouse.Name);
                Assert.AreEqual(countryID, actualWarehouse.CountryID);
                Assert.AreEqual(cityID, actualWarehouse.CityID);
                Assert.AreEqual(addressID, actualWarehouse.AddressID);

                //Assert
                await Assert.ThrowsExceptionAsync<ArgumentException>(async () => await sut.UpdateWarehouseAsync(actualWarehouse.WarehouseID, warehouseName, -28000, cityID, addressID, toSave));
            }
        }

        [TestMethod]
        [DataRow("wdfgdfggfd", 152, 164, 113, true)]
        [DataRow("wajkgkjkjkjkjk", 152, 164, 113, true)]
        [DataRow("warlgkljkljkljk", 152, 164, 113, true)]
        [DataRow("warehouse15", 152, 164, 113, true)]
        [DataRow("warehouse900", 165, 141, 139, false)]
        public async Task UpdateWarehouseWithInvalidAddressWhenValidID(string warehouseName, int cityID, int countryID, int addressID, bool toSave)
        {
            //Arrange
            var UpdateWarehouseWithInvalidAddressWhenValidID = System.Reflection.MethodBase.GetCurrentMethod().Name;

            var options = Utils.GetOptions(UpdateWarehouseWithInvalidAddressWhenValidID);

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

                Assert.AreNotEqual(null, actualWarehouse);
                Assert.AreEqual(warehouseName, actualWarehouse.Name);
                Assert.AreEqual(countryID, actualWarehouse.CountryID);
                Assert.AreEqual(cityID, actualWarehouse.CityID);
                Assert.AreEqual(addressID, actualWarehouse.AddressID);

                //Assert
                await Assert.ThrowsExceptionAsync<ArgumentException>(async () => await sut.UpdateWarehouseAsync(actualWarehouse.WarehouseID, warehouseName, countryID, cityID, -25000, toSave));
            }
        }

        [TestMethod]
        [DataRow("wdfgdfggfd", 152, 164, 113, true)]
        [DataRow("wajkgkjkjkjkjk", 152, 164, 113, true)]
        [DataRow("warlgkljkljkljk", 152, 164, 113, true)]
        [DataRow("warehouse15", 152, 164, 113, true)]
        [DataRow("warehouse900", 165, 141, 139, false)]
        public async Task UpdateWarehouseFullyWithInvalidInputDataWhenValidID(string warehouseName, int cityID, int countryID, int addressID, bool toSave)
        {
            //Arrange
            var UpdateWarehouseFullyWithInvalidInputDataWhenValidID = System.Reflection.MethodBase.GetCurrentMethod().Name;

            var options = Utils.GetOptions(UpdateWarehouseFullyWithInvalidInputDataWhenValidID);

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

                Assert.AreNotEqual(null, actualWarehouse);
                Assert.AreEqual(warehouseName, actualWarehouse.Name);
                Assert.AreEqual(countryID, actualWarehouse.CountryID);
                Assert.AreEqual(cityID, actualWarehouse.CityID);
                Assert.AreEqual(addressID, actualWarehouse.AddressID);

                //Assert
                await Assert.ThrowsExceptionAsync<ArgumentException>(async () => await sut.UpdateWarehouseAsync(actualWarehouse.WarehouseID, null, -30001, -30002, -30003, toSave));
            }
        }

        [TestMethod]
        [DataRow("wdfgdfggfd", 52, 64, 13, true)]
        [DataRow("wajkgkjkjkjkjk", 52, 64, 13, true)]
        [DataRow("warlgkljkljkljk", 52, 64, 13, true)]
        [DataRow("warehouse15", 52, 64, 13, true)]
        [DataRow("warehouse900", 65, 41, 39, false)]
        public async Task UpdateWarehouseWhenInvalidID(string warehouseName, int cityID, int countryID, int addressID, bool toSave)
        {
            //Arrange
            var UpdateWarehouseWhenInvalidID = System.Reflection.MethodBase.GetCurrentMethod().Name;

            var options = Utils.GetOptions(UpdateWarehouseWhenInvalidID);

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

                Assert.AreNotEqual(null, actualWarehouse);
                Assert.AreEqual(warehouseName, actualWarehouse.Name);
                Assert.AreEqual(countryID, actualWarehouse.CountryID);
                Assert.AreEqual(cityID, actualWarehouse.CityID);
                Assert.AreEqual(addressID, actualWarehouse.AddressID);

                //Assert
                await Assert.ThrowsExceptionAsync<ArgumentException>(async () => await sut.UpdateWarehouseAsync(-actualWarehouse.WarehouseID, actualWarehouse.Name, actualWarehouse.CountryID, actualWarehouse.CityID, actualWarehouse.AddressID, toSave));
            }
        }

        [TestMethod]
        [DataRow("wdfgdfggfd", 52, 64, 13, true)]
        [DataRow("wajkgkjkjkjkjk", 52, 64, 13, true)]
        [DataRow("warlgkljkljkljk", 52, 64, 13, true)]
        [DataRow("warehouse15", 52, 64, 13, true)]
        [DataRow("warehouse900", 65, 41, 39, false)]
        public async Task UpdateWarehouseWithInvalidNameWhenInvalidID(string warehouseName, int cityID, int countryID, int addressID, bool toSave)
        {
            //Arrange
            var UpdateWarehouseWithInvalidNameWhenValidID = System.Reflection.MethodBase.GetCurrentMethod().Name;

            var options = Utils.GetOptions(UpdateWarehouseWithInvalidNameWhenValidID);

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
                await Assert.ThrowsExceptionAsync<ArgumentException>(async () => await sut.UpdateWarehouseAsync(-actualWarehouse.WarehouseID, null, countryID, cityID, addressID, toSave));
            }
        }

        [TestMethod]
        [DataRow("wdfgdfggfd", 52, 64, 13, true)]
        [DataRow("wajkgkjkjkjkjk", 52, 64, 13, true)]
        [DataRow("warlgkljkljkljk", 52, 64, 13, true)]
        [DataRow("warehouse15", 52, 64, 13, true)]
        [DataRow("warehouse900", 65, 41, 39, false)]
        public async Task UpdateWarehouseWithEmptyNameWhenInvalidID(string warehouseName, int cityID, int countryID, int addressID, bool toSave)
        {
            //Arrange
            var UpdateWarehouseWithEmptyNameWhenInvalidID = System.Reflection.MethodBase.GetCurrentMethod().Name;

            var options = Utils.GetOptions(UpdateWarehouseWithEmptyNameWhenInvalidID);

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
                await Assert.ThrowsExceptionAsync<ArgumentException>(async () => await sut.UpdateWarehouseAsync(-actualWarehouse.WarehouseID, "", countryID, cityID, addressID, toSave));
            }
        }

        [TestMethod]
        [DataRow("wdfgdfggfd", 52, 64, 13, true)]
        [DataRow("wajkgkjkjkjkjk", 52, 64, 13, true)]
        [DataRow("warlgkljkljkljk", 52, 64, 13, true)]
        [DataRow("warehouse15", 52, 64, 13, true)]
        [DataRow("warehouse900", 65, 41, 39, false)]
        public async Task UpdateWarehouseWithWhiteSpaceNameWhenInvalidID(string warehouseName, int cityID, int countryID, int addressID, bool toSave)
        {
            //Arrange
            var UpdateWarehouseWithWhiteSpaceNameWhenInvalidID = System.Reflection.MethodBase.GetCurrentMethod().Name;

            var options = Utils.GetOptions(UpdateWarehouseWithWhiteSpaceNameWhenInvalidID);

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
                await Assert.ThrowsExceptionAsync<ArgumentException>(async () => await sut.UpdateWarehouseAsync(-actualWarehouse.WarehouseID, "                ", countryID, cityID, addressID, toSave));
            }
        }

        [TestMethod]
        [DataRow("wdfgdfggfd", 152, 164, 113, true)]
        [DataRow("wajkgkjkjkjkjk", 152, 164, 113, true)]
        [DataRow("warlgkljkljkljk", 152, 164, 113, true)]
        [DataRow("warehouse15", 152, 164, 113, true)]
        [DataRow("warehouse900", 165, 141, 139, false)]
        public async Task UpdateWarehouseWithInvalidCityWhenInvalidID(string warehouseName, int cityID, int countryID, int addressID, bool toSave)
        {
            //Arrange
            var UpdateWarehouseWithInvalidCityWhenValidID = System.Reflection.MethodBase.GetCurrentMethod().Name;

            var options = Utils.GetOptions(UpdateWarehouseWithInvalidCityWhenValidID);

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

                Assert.AreNotEqual(null, actualWarehouse);
                Assert.AreEqual(warehouseName, actualWarehouse.Name);
                Assert.AreEqual(countryID, actualWarehouse.CountryID);
                Assert.AreEqual(cityID, actualWarehouse.CityID);
                Assert.AreEqual(addressID, actualWarehouse.AddressID);

                //Assert
                await Assert.ThrowsExceptionAsync<ArgumentException>(async () => await sut.UpdateWarehouseAsync(-actualWarehouse.WarehouseID, warehouseName, countryID, -31000, addressID, toSave));
            }
        }

        [TestMethod]
        [DataRow("wdfgdfggfd", 152, 164, 113, true)]
        [DataRow("wajkgkjkjkjkjk", 152, 164, 113, true)]
        [DataRow("warlgkljkljkljk", 152, 164, 113, true)]
        [DataRow("warehouse15", 152, 164, 113, true)]
        [DataRow("warehouse900", 165, 141, 139, false)]
        public async Task UpdateWarehouseWithInvalidCountryWhenInvalidID(string warehouseName, int cityID, int countryID, int addressID, bool toSave)
        {
            //Arrange
            var UpdateWarehouseWithInvalidCountryWhenValidID = System.Reflection.MethodBase.GetCurrentMethod().Name;

            var options = Utils.GetOptions(UpdateWarehouseWithInvalidCountryWhenValidID);

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

                Assert.AreNotEqual(null, actualWarehouse);
                Assert.AreEqual(warehouseName, actualWarehouse.Name);
                Assert.AreEqual(countryID, actualWarehouse.CountryID);
                Assert.AreEqual(cityID, actualWarehouse.CityID);
                Assert.AreEqual(addressID, actualWarehouse.AddressID);

                //Assert
                await Assert.ThrowsExceptionAsync<ArgumentException>(async () => await sut.UpdateWarehouseAsync(-actualWarehouse.WarehouseID, warehouseName, -28000, cityID, addressID, toSave));
            }
        }

        [TestMethod]
        [DataRow("wdfgdfggfd", 152, 164, 113, true)]
        [DataRow("wajkgkjkjkjkjk", 152, 164, 113, true)]
        [DataRow("warlgkljkljkljk", 152, 164, 113, true)]
        [DataRow("warehouse15", 152, 164, 113, true)]
        [DataRow("warehouse900", 165, 141, 139, false)]
        public async Task UpdateWarehouseWithInvalidAddressWhenInvalidID(string warehouseName, int cityID, int countryID, int addressID, bool toSave)
        {
            //Arrange
            var UpdateWarehouseWithInvalidAddressWhenValidID = System.Reflection.MethodBase.GetCurrentMethod().Name;

            var options = Utils.GetOptions(UpdateWarehouseWithInvalidAddressWhenValidID);

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

                Assert.AreNotEqual(null, actualWarehouse);
                Assert.AreEqual(warehouseName, actualWarehouse.Name);
                Assert.AreEqual(countryID, actualWarehouse.CountryID);
                Assert.AreEqual(cityID, actualWarehouse.CityID);
                Assert.AreEqual(addressID, actualWarehouse.AddressID);

                //Assert
                await Assert.ThrowsExceptionAsync<ArgumentException>(async () => await sut.UpdateWarehouseAsync(-actualWarehouse.WarehouseID, warehouseName, countryID, cityID, -25000, toSave));
            }
        }

        [TestMethod]
        [DataRow("wdfgdfggfd", 152, 164, 113, true)]
        [DataRow("wajkgkjkjkjkjk", 152, 164, 113, true)]
        [DataRow("warlgkljkljkljk", 152, 164, 113, true)]
        [DataRow("warehouse15", 152, 164, 113, true)]
        [DataRow("warehouse900", 165, 141, 139, false)]
        public async Task UpdateWarehouseFullyWithInvalidInputDataWhenInvalidID(string warehouseName, int cityID, int countryID, int addressID, bool toSave)
        {
            //Arrange
            var UpdateWarehouseFullyWithInvalidInputDataWhenValidID = System.Reflection.MethodBase.GetCurrentMethod().Name;

            var options = Utils.GetOptions(UpdateWarehouseFullyWithInvalidInputDataWhenValidID);

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

                Assert.AreNotEqual(null, actualWarehouse);
                Assert.AreEqual(warehouseName, actualWarehouse.Name);
                Assert.AreEqual(countryID, actualWarehouse.CountryID);
                Assert.AreEqual(cityID, actualWarehouse.CityID);
                Assert.AreEqual(addressID, actualWarehouse.AddressID);

                //Assert
                await Assert.ThrowsExceptionAsync<ArgumentException>(async () => await sut.UpdateWarehouseAsync(-actualWarehouse.WarehouseID, null, -30001, -30002, -30003, toSave));
            }
        }
    }
}
