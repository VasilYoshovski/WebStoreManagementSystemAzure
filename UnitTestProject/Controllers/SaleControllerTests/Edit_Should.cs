using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using StoreSystem.Data;
using StoreSystem.Data.Models;
using StoreSystem.Web.Models.SaleViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreSystem.Tests.Controllers.SaleControllerTests
{
    [TestClass]
    public class Edit_Should : SaleConrollerMocks
    {
        [TestMethod]
        public async Task ReturnProper_WhenValidSaleIdIsPassed()
        {
            int validSaleId = 1;

            var callArgs = new List<int>();
            InitMocks();

            SaleService.Setup(x => x.GetSaleInfoAsync(It.IsAny<int>()))
                .ReturnsAsync(new Sale())
                .Callback((int s) => callArgs.Add(s));


            SaleService.Setup(x => x.GetAllProductsInSaleAsync(It.IsAny<int>()))
                .ReturnsAsync(new List<ProductSale>());

            AddressService.Setup(x => x.GetAllAddressesAsync()).ReturnsAsync(new List<Address>());
            CityService.Setup(x => x.GetAllCitiesAsync()).ReturnsAsync(new List<City>());
            CountryService.Setup(x => x.GetAllCountriesAsync()).ReturnsAsync(new List<Country>());
            ClientService.Setup(x => x.GetAllClientsAsync(It.IsAny<int>(), It.IsAny<int>(), "*")).ReturnsAsync(new List<Client>());

            SaleCUMapper.Setup(x => x.MapFrom(It.IsAny<Sale>())).Returns(new SaleCUViewModel());

            var user = new StoreUser() { Id = "validId", UserName = "valid@valid.com", Email = "valid@valid.com" };
            var context = CreateControllerContext(user, ROLES.Client, true);


            var sut = CreateController();

            sut.ControllerContext = context;

            //Act
            var result = await sut.Edit(validSaleId);

            //Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            Assert.IsInstanceOfType((result as ViewResult).Model, typeof(SaleCUViewModel));
            Assert.IsTrue(callArgs.All(x => x == validSaleId));
            Assert.IsTrue(callArgs.Count == 1);
            AddressService.Verify(mock => mock.GetAllAddressesAsync(), Times.Once());
        }

        [TestMethod]
        public async Task ReturnProper_WhenInvalidSaleIdIsPassed()
        {
            int notFoundSaleId = 1;
            InitMocks();
            var sut = CreateController();

            //Act
            var result = await sut.Edit(notFoundSaleId);

            //Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
            SaleService.Verify(mock => mock.GetSaleInfoAsync(notFoundSaleId), Times.Once());
        }

        [TestMethod]
        public async Task ReturnProper_WhenNullSaleIdIsPassed()
        {
            int? nullSaleId = null;

            var callArgs = new List<int>();
            InitMocks();
            var sut = CreateController();

            //Act
            var result = await sut.Edit(nullSaleId);

            //Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
            SaleService.Verify(mock => mock.GetSaleInfoAsync(It.IsAny<int?>()), Times.Never());
        }
    }
}
