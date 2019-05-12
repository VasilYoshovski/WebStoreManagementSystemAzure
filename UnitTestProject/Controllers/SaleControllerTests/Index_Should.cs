using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using StoreSystem.Data;
using StoreSystem.Data.Models;
using StoreSystem.Services.Dto;
using StoreSystem.Web.Models.SaleViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreSystem.Tests.Controllers.SaleControllerTests
{
    [TestClass]
    public class Index_Should : SaleConrollerMocks
    {

        [TestMethod]
        public async Task ReturnProper_WhenUserIsNotInRoleClient()
        {
            var user = new StoreUser() { Id = "validId", UserName = "valid@valid.com", Email = "valid@valid.com", ClientId = null };

            var context = CreateControllerContext(user, ROLES.Admin, true);
            var sut = CreateController();

            sut.ControllerContext = context;
            
            //Act
            var result = await sut.Index();

            //Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            Assert.IsInstanceOfType((result as ViewResult).Model, typeof(SaleIndexViewModel));
            SaleService.Verify(mock => mock.GetSalesWithTotalAsync(null,null,null,null,null), Times.Once());
        }

        [TestMethod]
        public async Task ReturnProper_WhenUserIsInRoleClientAndHasInvalidId()
        {
            var user = new StoreUser() { Id = "validId", UserName = "valid@valid.com", Email = "valid@valid.com", ClientId = null };
            var context = CreateControllerContext(user, ROLES.Client, true);
            var sut = CreateController();

            sut.ControllerContext = context;

            //Act
            var result = await sut.Index();

            //Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task ReturnProper_WhenUserIsInRoleClientAndHasValidId()
        {
            int validClientId = 1;

            var callArgs = new List<int>();

            SaleService.Setup(x => x.GetSalesWithTotalAsync(null, null, It.IsAny<int>(), null, null))
                .ReturnsAsync(new List<SaleWithTotalDto>())
                .Callback((int a, string b, int s, DateTime c, DateTime d) => callArgs.Add(s));

            var user = new StoreUser() { Id = "validId", UserName = "valid@valid.com", Email = "valid@valid.com", ClientId = validClientId };
            var context = CreateControllerContext(user, ROLES.Client, true);
            var sut = CreateController();

            sut.ControllerContext = context;

            //Act
            var result = await sut.Index();

            //Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            Assert.IsInstanceOfType((result as ViewResult).Model, typeof(SaleIndexViewModel));
            Assert.IsTrue(callArgs.All(x => x == validClientId));
            Assert.IsTrue(callArgs.Count == 1);
        }
    }
}
