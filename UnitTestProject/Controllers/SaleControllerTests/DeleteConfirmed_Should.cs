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
    public class DeleteConfirmed_Should : SaleConrollerMocks
    {
        [TestMethod]
        public async Task ReturnProper_WhenUserIsInRoleAdminAndValidSaleIdIsPassed()
        {
            int validSaleId = 1;

            var callArgs = new List<int>();

            SaleService.Setup(x => x.DeleteSaleAsync(It.IsAny<int>()))
                .ReturnsAsync(true)
                .Callback((int s) => callArgs.Add(s));
            var sut = CreateController();

            //Act
            var result = await sut.DeleteConfirmed(validSaleId);

            //Assert
            Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));
            Assert.IsTrue(callArgs.All(x => x == validSaleId));
            Assert.IsTrue(callArgs.Count == 1);
        }

        [TestMethod]
        public async Task ReturnProper_WhenInvalidSaleIdIsPassed()
        {
            int errorSaleId = 1;
            InitMocks();
            var sut = CreateController();

            SaleService.Setup(x => x.DeleteSaleAsync(errorSaleId)).Throws(new ArgumentException());
            //Act
            var result = await sut.DeleteConfirmed(errorSaleId);

            //Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
            SaleService.Verify(mock => mock.DeleteSaleAsync(errorSaleId), Times.Once());
        }
    }
}
