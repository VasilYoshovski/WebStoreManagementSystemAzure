using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using StoreSystem.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StoreSystem.Tests.Controllers.SaleControllerTests
{
    [TestClass]
    public class Create_Should:SaleConrollerMocks
    {
        [TestMethod]
        public async Task ReturnProperViewAsync()
        {
            var sut = CreateController();
            AddressService.Setup(x => x.GetAllAddressesAsync()).ReturnsAsync(new List<Address>());
            CityService.Setup(x => x.GetAllCitiesAsync()).ReturnsAsync(new List<City>());
            CountryService.Setup(x => x.GetAllCountriesAsync()).ReturnsAsync(new List<Country>());
            ClientService.Setup(x => x.GetAllClientsAsync(It.IsAny<int>(), It.IsAny<int>(), "*")).ReturnsAsync(new List<Client>());


            //Act
            var result = await sut.Create();

            //Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }
    }
}
