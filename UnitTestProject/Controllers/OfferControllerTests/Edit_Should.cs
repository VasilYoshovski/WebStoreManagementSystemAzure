using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using StoreSystem.Data;
using StoreSystem.Data.Models;
using StoreSystem.Web.Models.OfferViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreSystem.Tests.Controllers.OfferControllerTests
{
    [TestClass]
    public class Edit_Should : OfferConrollerMocks
    {
        [TestMethod]
        public async Task ReturnProper_WhenValidOfferIdIsPassed()
        {
            int validOfferId = 1;

            var callArgs = new List<int>();
            InitMocks();

            OfferService.Setup(x => x.GetOfferInfoAsync(It.IsAny<int>()))
                .ReturnsAsync(new Offer())
                .Callback((int s) => callArgs.Add(s));


            OfferService.Setup(x => x.GetAllProductsInOfferAsync(It.IsAny<int>()))
                .ReturnsAsync(new List<ProductOffer>());

            AddressService.Setup(x => x.GetAllAddressesAsync()).ReturnsAsync(new List<Address>());
            CityService.Setup(x => x.GetAllCitiesAsync()).ReturnsAsync(new List<City>());
            CountryService.Setup(x => x.GetAllCountriesAsync()).ReturnsAsync(new List<Country>());
            ClientService.Setup(x => x.GetAllClientsAsync(It.IsAny<int>(), It.IsAny<int>(), "*")).ReturnsAsync(new List<Client>());

            OfferCUMapper.Setup(x => x.MapFrom(It.IsAny<Offer>())).Returns(new OfferCUViewModel());

            var user = new StoreUser() { Id = "validId", UserName = "valid@valid.com", Email = "valid@valid.com" };
            var context = CreateControllerContext(user, ROLES.Client, true);


            var sut = CreateController();

            sut.ControllerContext = context;

            //Act
            var result = await sut.Edit(validOfferId);

            //Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            Assert.IsInstanceOfType((result as ViewResult).Model, typeof(OfferCUViewModel));
            Assert.IsTrue(callArgs.All(x => x == validOfferId));
            Assert.IsTrue(callArgs.Count == 1);
            AddressService.Verify(mock => mock.GetAllAddressesAsync(), Times.Once());
        }

        [TestMethod]
        public async Task ReturnProper_WhenInvalidOfferIdIsPassed()
        {
            int notFoundOfferId = 1;
            InitMocks();
            var sut = CreateController();

            //Act
            var result = await sut.Edit(notFoundOfferId);

            //Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
            OfferService.Verify(mock => mock.GetOfferInfoAsync(notFoundOfferId), Times.Once());
        }

        [TestMethod]
        public async Task ReturnProper_WhenNullOfferIdIsPassed()
        {
            int? nullOfferId = null;

            var callArgs = new List<int>();
            InitMocks();
            var sut = CreateController();

            //Act
            var result = await sut.Edit(nullOfferId);

            //Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
            OfferService.Verify(mock => mock.GetOfferInfoAsync(It.IsAny<int?>()), Times.Never());
        }
    }
}
