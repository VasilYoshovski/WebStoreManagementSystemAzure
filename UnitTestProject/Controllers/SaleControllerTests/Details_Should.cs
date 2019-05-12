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
    public class Details_Should:SaleConrollerMocks
    {
        [TestMethod]
        public async Task ReturnProper_WhenUserIsInRoleAdminAndValidSaleIdIsPassed()
        {
            int validSaleId = 1;

            var callArgs = new List<int>();

            SaleService.Setup(x => x.GetSaleInfoAsync(It.IsAny<int>()))
                .ReturnsAsync(new Sale())
                .Callback((int s) => callArgs.Add(s));


            SaleService.Setup(x => x.GetAllProductsInSaleAsync(It.IsAny<int>()))
                .ReturnsAsync(new List<ProductSale>());

            var user = new StoreUser() { Id = "validId", UserName = "valid@valid.com", Email = "valid@valid.com"};
            var context = CreateControllerContext(user, ROLES.Admin, true);
            var sut = CreateController();

            sut.ControllerContext = context;

            //Act
            var result = await sut.Details(validSaleId);

            //Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            Assert.IsInstanceOfType((result as ViewResult).Model, typeof(SaleIDetailsViewModel));
            Assert.IsTrue(callArgs.All(x => x == validSaleId));
            Assert.IsTrue(callArgs.Count == 1);
            Assert.IsTrue(((result as ViewResult).Model as SaleIDetailsViewModel).CanEdit);
        }

        [TestMethod]
        public async Task ReturnProper_WhenUserIsInRoleClientAndValidSaleIdIsPassed()
        {
            int validSaleId = 1;

            var callArgs = new List<int>();

            SaleService.Setup(x => x.GetSaleInfoAsync(It.IsAny<int>()))
                .ReturnsAsync(new Sale())
                .Callback((int s) => callArgs.Add(s));


            SaleService.Setup(x => x.GetAllProductsInSaleAsync(It.IsAny<int>()))
                .ReturnsAsync(new List<ProductSale>());

            var user = new StoreUser() { Id = "validId", UserName = "valid@valid.com", Email = "valid@valid.com" };
            var context = CreateControllerContext(user, ROLES.Client, true);
            var sut = CreateController();

            sut.ControllerContext = context;

            //Act
            var result = await sut.Details(validSaleId);

            //Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            Assert.IsInstanceOfType((result as ViewResult).Model, typeof(SaleIDetailsViewModel));
            Assert.IsTrue(callArgs.All(x => x == validSaleId));
            Assert.IsTrue(callArgs.Count == 1);
            Assert.IsFalse(((result as ViewResult).Model as SaleIDetailsViewModel).CanEdit);
        }

        [TestMethod]
        public async Task ReturnProper_WhenInvalidSaleIdIsPassed()
        {
            int notFoundSaleId = 1;
            InitMocks();
            var sut = CreateController();

            //Act
            var result = await sut.Details(notFoundSaleId);

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
            var result = await sut.Details(nullSaleId);

            //Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
            SaleService.Verify(mock => mock.GetSaleInfoAsync(It.IsAny<int?>()), Times.Never());
        }
    }
}
