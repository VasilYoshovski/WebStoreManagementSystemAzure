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
    public class DeleteConfirmed_Should : OfferConrollerMocks
    {
        [TestMethod]
        public async Task ReturnProper_WhenUserIsInRoleAdminAndValidOfferIdIsPassed()
        {
            int validOfferId = 1;

            var callArgs = new List<int>();

            OfferService.Setup(x => x.DeleteOfferAsync(It.IsAny<int>()))
                .ReturnsAsync(true)
                .Callback((int s) => callArgs.Add(s));
            var sut = CreateController();

            //Act
            var result = await sut.DeleteConfirmed(validOfferId);

            //Assert
            Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));
            Assert.IsTrue(callArgs.All(x => x == validOfferId));
            Assert.IsTrue(callArgs.Count == 1);
        }

        [TestMethod]
        public async Task ReturnProper_WhenInvalidOfferIdIsPassed()
        {
            int errorOfferId = 1;
            InitMocks();
            var sut = CreateController();

            OfferService.Setup(x => x.DeleteOfferAsync(errorOfferId)).Throws(new ArgumentException());
            //Act
            var result = await sut.DeleteConfirmed(errorOfferId);

            //Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
            OfferService.Verify(mock => mock.DeleteOfferAsync(errorOfferId), Times.Once());
        }
    }
}
