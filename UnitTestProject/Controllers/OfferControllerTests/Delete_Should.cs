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
    public class Delete_Should : OfferConrollerMocks
    {
        [TestMethod]
        public async Task ReturnProper_WhenValidOfferIdIsPassed()
        {
            int validOfferId = 1;

            var callArgs = new List<int>();

            OfferService.Setup(x => x.GetOfferInfoAsync(It.IsAny<int>()))
                .ReturnsAsync(new Offer())
                .Callback((int s) => callArgs.Add(s));

            OfferInfoMapper.Setup(x => x.MapFrom(It.IsAny<Offer>())).Returns(new OfferInfoViewModel());

            var user = new StoreUser() { Id = "validId", UserName = "valid@valid.com", Email = "valid@valid.com" };
            var context = CreateControllerContext(user, ROLES.Client, true);
            var sut = CreateController();

            sut.ControllerContext = context;

            //Act
            var result = await sut.Delete(validOfferId);

            //Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            Assert.IsInstanceOfType((result as ViewResult).Model, typeof(OfferInfoViewModel));
            Assert.IsTrue(callArgs.All(x => x == validOfferId));
            Assert.IsTrue(callArgs.Count == 1);
            OfferInfoMapper.Verify(mock => mock.MapFrom(It.IsAny<Offer>()), Times.Once());
        }

        [TestMethod]
        public async Task ReturnProper_WhenInvalidOfferIdIsPassed()
        {
            int notFoundOfferId = 1;


            InitMocks();
            var sut = CreateController();

            //Act
            var result = await sut.Delete(notFoundOfferId);

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
            var result = await sut.Delete(nullOfferId);

            //Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
            OfferService.Verify(mock => mock.GetOfferInfoAsync(It.IsAny<int?>()), Times.Never());
        }
    }
}
