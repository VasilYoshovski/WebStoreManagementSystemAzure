using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using StoreSystem.Data;
using StoreSystem.Data.Models;
using StoreSystem.Tests.Controllers.OfferControllerTests;
using StoreSystem.Web.Models.OfferViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreSystem.Tests.Controllers.OfferControllerTests
{
    [TestClass]
    public class Details_Should:OfferConrollerMocks
    {

        [TestMethod]
        public async Task ReturnProper_WhenNullOfferIdIsPassed()
        {
            int? nullOfferId = null;

            var callArgs = new List<int>();

            InitMocks();
            var sut = CreateController();

            //Act
            var result = await sut.Details(nullOfferId);

            //Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
            OfferService.Verify(mock => mock.GetOfferInfoAsync(It.IsAny<int?>()), Times.Never());
        }

        [TestMethod]
        public async Task ReturnProper_WhenUserIsInRoleAdminAndValidOfferIdIsPassed()
        {
            int validOfferId = 1;

            var callArgs = new List<int>();

            OfferService.Setup(x => x.GetOfferInfoAsync(It.IsAny<int>()))
                .ReturnsAsync(new Offer())
                .Callback((int s) => callArgs.Add(s));


            OfferService.Setup(x => x.GetAllProductsInOfferAsync(It.IsAny<int>()))
                .ReturnsAsync(new List<ProductOffer>());

            var user = new StoreUser() { Id = "validId", UserName = "valid@valid.com", Email = "valid@valid.com"};
            var context = CreateControllerContext(user, ROLES.Admin, true);
            var sut = CreateController();

            sut.ControllerContext = context;

            //Act
            var result = await sut.Details(validOfferId);

            //Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            Assert.IsInstanceOfType((result as ViewResult).Model, typeof(OfferDetailsViewModel));
            Assert.IsTrue(callArgs.All(x => x == validOfferId));
            Assert.IsTrue(callArgs.Count == 1);
            Assert.IsTrue(((result as ViewResult).Model as OfferDetailsViewModel).CanEdit);
        }

        [TestMethod]
        public async Task ReturnProper_WhenUserIsInRoleClientAndValidOfferIdIsPassed()
        {
            int validOfferId = 1;

            var callArgs = new List<int>();

            OfferService.Setup(x => x.GetOfferInfoAsync(It.IsAny<int>()))
                .ReturnsAsync(new Offer())
                .Callback((int s) => callArgs.Add(s));


            OfferService.Setup(x => x.GetAllProductsInOfferAsync(It.IsAny<int>()))
                .ReturnsAsync(new List<ProductOffer>());

            var user = new StoreUser() { Id = "validId", UserName = "valid@valid.com", Email = "valid@valid.com" };
            var context = CreateControllerContext(user, ROLES.Client, true);
            var sut = CreateController();

            sut.ControllerContext = context;

            //Act
            var result = await sut.Details(validOfferId);

            //Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            Assert.IsInstanceOfType((result as ViewResult).Model, typeof(OfferDetailsViewModel));
            Assert.IsTrue(callArgs.All(x => x == validOfferId));
            Assert.IsTrue(callArgs.Count == 1);
            Assert.IsFalse(((result as ViewResult).Model as OfferDetailsViewModel).CanEdit);
        }

        [TestMethod]
        public async Task ReturnProper_WhenInvalidOfferIdIsPassed()
        {
            int notFoundOfferId = 1;
            InitMocks();
            var sut = CreateController();

            //Act
            var result = await sut.Details(notFoundOfferId);

            //Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
            OfferService.Verify(mock => mock.GetOfferInfoAsync(notFoundOfferId), Times.Once());
        }
    }
}
