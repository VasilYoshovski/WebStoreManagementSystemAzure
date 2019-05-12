using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using StoreSystem.Data;
using StoreSystem.Data.Models;
using StoreSystem.Services;
using StoreSystem.Web.Controllers;
using StoreSystem.Web.Mappers;
using StoreSystem.Web.Models.ClientViewModels;
using StoreSystem.Web.Models.ProductViewModels;
using StoreSystem.Web.Models.SaleViewModels;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace StoreSystem.Tests.Controllers.SaleControllerTests
{
    public class SaleConrollerMocks
    {
        static SaleConrollerMocks()
        {
            SaleService = new Mock<ISaleService>();
            ClientService = new Mock<IClientService>();
            AddressService = new Mock<IAddressService>();
            CityService = new Mock<ICityService>();
            CountryService = new Mock<ICountryService>();
            SaleInfoMapper = new Mock<IViewModelMapper<Sale, SaleInfoViewModel>>();
            SaleCUMapper = new Mock<IViewModelMapper<Sale, SaleCUViewModel>>();
            ProductSaleInfoMapper = new Mock<IViewModelMapper<ProductSale, ProductLineInfoViewModel>>();
            ClientInfoMapper = new Mock<IViewModelMapper<Client, ClientInfoViewModel>>();
        }
        public static Mock<IUserStore<StoreUser>> mockUserStore { get; set; } = new Mock<IUserStore<StoreUser>>();
        public static Mock<ISaleService> SaleService { get; set; }
        public static Mock<IClientService> ClientService { get; set; }
        public static Mock<IAddressService> AddressService { get; set; }
        public static Mock<ICityService> CityService { get; set; }
        public static Mock<ICountryService> CountryService { get; set; }
        public static Mock<UserManager<StoreUser>> UserManager { get; set; }
        public static Mock<IViewModelMapper<Sale, SaleInfoViewModel>> SaleInfoMapper { get; set; }
        public static Mock<IViewModelMapper<Sale, SaleCUViewModel>> SaleCUMapper { get; set; }
        public static Mock<IViewModelMapper<ProductSale, ProductLineInfoViewModel>> ProductSaleInfoMapper { get; set; }
        public static Mock<IViewModelMapper<Client, ClientInfoViewModel>> ClientInfoMapper { get; set; }

        public static void InitMocks()
        {
            SaleService = new Mock<ISaleService>();
            ClientService = new Mock<IClientService>();
            AddressService = new Mock<IAddressService>();
            CityService = new Mock<ICityService>();
            CountryService = new Mock<ICountryService>();
            SaleInfoMapper = new Mock<IViewModelMapper<Sale, SaleInfoViewModel>>();
            SaleCUMapper = new Mock<IViewModelMapper<Sale, SaleCUViewModel>>();
            ProductSaleInfoMapper = new Mock<IViewModelMapper<ProductSale, ProductLineInfoViewModel>>();
            ClientInfoMapper = new Mock<IViewModelMapper<Client, ClientInfoViewModel>>();
        }

        public static SalesController CreateController()
        {
            SalesController controller;

            if (UserManager == null)
            {
                controller = new SalesController(
                     SaleService.Object,
                     ClientService.Object,
                     AddressService.Object,
                     CityService.Object,
                     CountryService.Object,
                     new UserManager<StoreUser>(mockUserStore.Object, null, null, null, null, null, null, null, null),
                     SaleInfoMapper.Object,
                     SaleCUMapper.Object,
                     ProductSaleInfoMapper.Object,
                     ClientInfoMapper.Object
                     );
            } else
            {
                controller = new SalesController(
                     SaleService.Object,
                     ClientService.Object,
                     AddressService.Object,
                     CityService.Object,
                     CountryService.Object,
                     UserManager.Object,
                     SaleInfoMapper.Object,
                     SaleCUMapper.Object,
                     ProductSaleInfoMapper.Object,
                     ClientInfoMapper.Object
                     );
            }
            return controller;
        }

        public static ControllerContext CreateControllerContext(StoreUser storeUser, string roles=null, bool isInRoleReturnBool=true, string identityName = null)
        {
            var userStoreMock = Mock.Of<IUserStore<StoreUser>>();
            var userMgr = new Mock<UserManager<StoreUser>>(userStoreMock, null, null, null, null, null, null, null, null);
            var user = storeUser;   

            var tcs = new TaskCompletionSource<StoreUser>();
            tcs.SetResult(user);

            userMgr.Setup(x => x.FindByIdAsync(user.Id)).Returns(tcs.Task);

            var claimsPrincipalMock = new Mock<ClaimsPrincipal>();
            if (roles == null) claimsPrincipalMock.Setup(x => x.IsInRole(It.IsAny<string>())).Returns(isInRoleReturnBool);
            else claimsPrincipalMock.Setup(x => x.IsInRole(roles)).Returns(isInRoleReturnBool);
            claimsPrincipalMock.Name = identityName;
            claimsPrincipalMock.SetupGet(x => x.Identity.Name).Returns(claimsPrincipalMock.Name);

            userMgr.Setup(x => x.GetUserAsync(claimsPrincipalMock.Object)).Returns(tcs.Task);

            UserManager = userMgr;
            var context = new ControllerContext();
            context.HttpContext = new DefaultHttpContext();
            context.HttpContext.User = claimsPrincipalMock.Object;

            return context;
        }
    }
}
