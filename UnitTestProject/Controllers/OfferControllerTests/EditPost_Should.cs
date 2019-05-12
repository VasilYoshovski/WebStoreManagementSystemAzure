using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using StoreSystem.Data;
using StoreSystem.Data.Models;
using StoreSystem.Web.Models.OfferViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreSystem.Tests.Controllers.OfferControllerTests
{
    [TestClass]
    public class EditPost_Should : OfferConrollerMocks
    {
        [TestMethod]
        public async Task ReturnProper_WhenValidOfferIdIsPassed()
        {
            int validOfferId = 1;

            var sale = new OfferCUViewModel
            {
                OfferID = validOfferId,
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
            var result = await sut.Edit(validOfferId,sale);

            //Assert
            Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));


            OfferService.Verify(mock => mock.UpdateOfferAsync(validOfferId,1, 10, new DateTime(2019, 5, 5),1, new DateTime(2019, 5, 5), 1, 1, 1), Times.Once());

            AddressService.Verify(mock => mock.GetAllAddressesAsync(), Times.Never());
            CityService.Verify(mock => mock.GetAllCitiesAsync(), Times.Never());
            CountryService.Verify(mock => mock.GetAllCountriesAsync(), Times.Never());
            ClientService.Verify(mock => mock.GetAllClientsAsync(It.IsAny<int>(), It.IsAny<int>(), "*"), Times.Never());
        }

        [TestMethod]
        public async Task ReturnResutViewWithProperModel_WhenValidOfferModelIsAndInvalidIdPassed()
        {
            int validOfferId = 1;

            var sale = new OfferCUViewModel
            {
                OfferID = validOfferId,
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
            var result = await sut.Edit(validOfferId, sale);

            //Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            Assert.IsInstanceOfType((result as ViewResult).Model, typeof(OfferCUViewModel));
            AddressService.Verify(mock => mock.GetAllAddressesAsync(), Times.Once());
            CityService.Verify(mock => mock.GetAllCitiesAsync(), Times.Once());
            CountryService.Verify(mock => mock.GetAllCountriesAsync(), Times.Once());
            ClientService.Verify(mock => mock.GetAllClientsAsync(It.IsAny<int>(), It.IsAny<int>(), "*"), Times.Once());
        }

        [TestMethod]
        public async Task ReturnNotFoundResult_WhenInvalidOfferIdIsPassed()
        {
            int invalidOfferId = 1;

            var sale = new OfferCUViewModel
            {
                OfferID = 2,
                ClientID = 1,
                DeadlineDate = new DateTime(2019, 5, 5),
                AddressID = 1,
                CityID = 1,
                CountryID = 1,
                ProductDiscount = 10,
            };
            var sut = CreateController();
            
            //Act
            var result = await sut.Edit(invalidOfferId, sale);

            //Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));

        }
    }
}
