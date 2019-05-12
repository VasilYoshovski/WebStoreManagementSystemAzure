using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using StoreSystem.Data;
using StoreSystem.Data.Models;
using StoreSystem.Web.Models.SaleViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreSystem.Tests.Controllers.SaleControllerTests
{
    [TestClass]
    public class EditPost_Should : SaleConrollerMocks
    {
        [TestMethod]
        public async Task ReturnProper_WhenValidSaleIdIsPassed()
        {
            int validSaleId = 1;

            var sale = new SaleCUViewModel
            {
                SaleID = validSaleId,
                ClientID = 1,
                OrderDate = new DateTime(2019, 5, 5),
                AddressID = 1,
                CityID = 1,
                CountryID = 1,
                ProductDiscount = 10,
                DeadlineDate = new DateTime(2019, 5, 5).AddDays(1),
                DeliveryDate = new DateTime(2019, 5, 5)
            };

            var sut = CreateController();
            sut.ModelState.ClearValidationState("error");

            //Act
            var result = await sut.Edit(validSaleId,sale);

            //Assert
            Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));


            SaleService.Verify(mock => mock.UpdateSaleAsync(validSaleId,1, 10, new DateTime(2019, 5, 5),1, new DateTime(2019, 5, 5), 1, 1, 1, null), Times.Once());

            AddressService.Verify(mock => mock.GetAllAddressesAsync(), Times.Never());
            CityService.Verify(mock => mock.GetAllCitiesAsync(), Times.Never());
            CountryService.Verify(mock => mock.GetAllCountriesAsync(), Times.Never());
            ClientService.Verify(mock => mock.GetAllClientsAsync(It.IsAny<int>(), It.IsAny<int>(), "*"), Times.Never());
        }

        [TestMethod]
        public async Task ReturnResutViewWithProperModel_WhenValidSaleModelIsAndInvalidIdPassed()
        {
            int validSaleId = 1;

            var sale = new SaleCUViewModel
            {
                SaleID = validSaleId,
                ClientID = 1,
                DeadlineDate = new DateTime(2019, 5, 5),
                AddressID = 1,
                CityID = 1,
                CountryID = 1,
                ProductDiscount = 10,
            };

            var sut = CreateController();
            sut.ModelState.AddModelError("error", "error");

            AddressService.Setup(x => x.GetAllAddressesAsync()).ReturnsAsync(new List<Address>());
            CityService.Setup(x => x.GetAllCitiesAsync()).ReturnsAsync(new List<City>());
            CountryService.Setup(x => x.GetAllCountriesAsync()).ReturnsAsync(new List<Country>());
            ClientService.Setup(x => x.GetAllClientsAsync(It.IsAny<int>(), It.IsAny<int>(), "*")).ReturnsAsync(new List<Client>());

            //Act
            var result = await sut.Edit(validSaleId, sale);

            //Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            Assert.IsInstanceOfType((result as ViewResult).Model, typeof(SaleCUViewModel));
            AddressService.Verify(mock => mock.GetAllAddressesAsync(), Times.Once());
            CityService.Verify(mock => mock.GetAllCitiesAsync(), Times.Once());
            CountryService.Verify(mock => mock.GetAllCountriesAsync(), Times.Once());
            ClientService.Verify(mock => mock.GetAllClientsAsync(It.IsAny<int>(), It.IsAny<int>(), "*"), Times.Once());
        }

        [TestMethod]
        public async Task ReturnNotFoundResult_WhenInvalidSaleIdIsPassed()
        {
            int invalidSaleId = 1;

            var sale = new SaleCUViewModel
            {
                SaleID = 2,
                ClientID = 1,
                DeadlineDate = new DateTime(2019, 5, 5),
                AddressID = 1,
                CityID = 1,
                CountryID = 1,
                ProductDiscount = 10,
            };
            var sut = CreateController();
            
            //Act
            var result = await sut.Edit(invalidSaleId, sale);

            //Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));

        }
    }
}
