using DinkToPdf;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using StoreSystem.Data;
using StoreSystem.Data.Models;
using StoreSystem.Services;
using StoreSystem.Web.Controllers.Utils;
using StoreSystem.Web.Mappers;
using StoreSystem.Web.Models.ClientViewModels;
using StoreSystem.Web.Models.ProductViewModels;
using StoreSystem.Web.Models.SaleViewModels;
using StoreSystem.Web.Utils;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace StoreSystem.Web.Controllers
{
    public class SalesController : Controller
    {
        private readonly ISaleService saleService;
        private readonly IClientService clientService;
        private readonly IAddressService addressService;
        private readonly ICityService cityService;
        private readonly ICountryService countryService;
        private readonly UserManager<StoreUser> userManager;
        private readonly IViewModelMapper<Sale, SaleInfoViewModel> saleInfoMapper;
        private readonly IViewModelMapper<Sale, SaleCUViewModel> saleCUMapper;
        private readonly IViewModelMapper<ProductSale, ProductLineInfoViewModel> productSaleInfoMapper;
        private readonly IViewModelMapper<Client, ClientInfoViewModel> clientInfoMapper;

        public SalesController(
            ISaleService saleService,
            IClientService clientService,
            IAddressService addressService,
            ICityService cityService,
            ICountryService countryService,
            UserManager<StoreUser> userManager,
            IViewModelMapper<Sale, SaleInfoViewModel> saleInfoMapper,
            IViewModelMapper<Sale, SaleCUViewModel> saleCUMapper,
            IViewModelMapper<ProductSale, ProductLineInfoViewModel> productSaleInfoMapper,
            IViewModelMapper<Client, ClientInfoViewModel> clientInfoMapper)
        {
            this.saleService = saleService ?? throw new ArgumentNullException(nameof(saleService));
            this.clientService = clientService ?? throw new ArgumentNullException(nameof(clientService));
            this.addressService = addressService ?? throw new ArgumentNullException(nameof(addressService));
            this.cityService = cityService ?? throw new ArgumentNullException(nameof(cityService));
            this.countryService = countryService ?? throw new ArgumentNullException(nameof(countryService));
            this.userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            this.saleInfoMapper = saleInfoMapper ?? throw new ArgumentNullException(nameof(saleInfoMapper));
            this.saleCUMapper = saleCUMapper ?? throw new ArgumentNullException(nameof(saleCUMapper));
            this.productSaleInfoMapper = productSaleInfoMapper ?? throw new ArgumentNullException(nameof(productSaleInfoMapper));
            this.clientInfoMapper = clientInfoMapper ?? throw new ArgumentNullException(nameof(clientInfoMapper));
        }

        private bool CanEdit()
        {
            var canEdit = false;

            if (this.User.IsInRole(ROLES.Admin) || this.User.IsInRole(ROLES.OfficeStaff))
            {
                canEdit = true;
            }
            else
            {
                canEdit = false;
            }
            return canEdit;
        }

        // GET: Sales
        [AuthorizeRolesAttribute(ROLES.OfficeStaff, ROLES.Admin, ROLES.Client)]
        public async Task<IActionResult> Index()
        {
            int? clientId = null;
            if (this.User.IsInRole(ROLES.Client))
            {
                clientId = (await this.userManager.GetUserAsync(this.User))?.ClientId;
                if (clientId == null)
                {
                    return this.NotFound();
                }
            }
            var sales = await this.saleService.GetSalesWithTotalAsync(clientID: clientId);
            var notClosed = await this.saleService.GetNotClosedSalesAsync();
            return this.View(new SaleIndexViewModel() { SalesList = sales, NotClosedSales = notClosed, CanEdit = this.CanEdit() });
        }

        // GET: Sales/Details/5
        [AuthorizeRolesAttribute(ROLES.OfficeStaff, ROLES.Admin,ROLES.Client)]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var sale = await this.saleService.GetSaleInfoAsync(id);
            if (sale == null)
            {
                return this.NotFound();
            }

            var productsInSale = await this.saleService.GetAllProductsInSaleAsync((int)id);
            var psViewModel = productsInSale.Select(this.productSaleInfoMapper.MapFrom);

            var detailViewModel = new SaleIDetailsViewModel()
            {
                ProductsInSale = psViewModel.ToList(),
                SaleInfo = this.saleInfoMapper.MapFrom(sale),
                CanEdit = this.CanEdit()
            };

            return this.View(detailViewModel);
        }

        //[AuthorizeRolesAttribute(ROLES.OfficeStaff, ROLES.Admin, ROLES.Client)]
        public async Task<IActionResult> Invoice(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var sale = await this.saleService.GetSaleInfoAsync(id);
            if (sale == null)
            {
                return this.NotFound();
            }

            var productsInSale = await this.saleService.GetAllProductsInSaleAsync((int)id);

            var client = await this.clientService.FindClientWithAddressAsync(sale.ClientID);

            var invoiceViewModel = new InvoiceViewModel()
            {
                ProductsInSale = productsInSale.Select(this.productSaleInfoMapper.MapFrom).ToList(),
                SaleInfo = this.saleInfoMapper.MapFrom(sale),
                ClientInfo = this.clientInfoMapper.MapFrom(client)
            };

            return this.View(invoiceViewModel);
        }

        // GET: Sales/Create
        [AuthorizeRolesAttribute(ROLES.OfficeStaff, ROLES.Admin)]
        public async Task<IActionResult> Create()
        {
            this.ViewData["Addresses"] = new SelectList(await this.addressService.GetAllAddressesAsync(), "AddressID", "Name");
            this.ViewData["Cities"] = new SelectList(await this.cityService.GetAllCitiesAsync(), "CityID", "Name");
            this.ViewData["Countries"] = new SelectList(await this.countryService.GetAllCountriesAsync(), "CountryID", "Name");
            this.ViewData["Clients"] = new SelectList(await this.clientService.GetAllClientsAsync(), "ClientID", "Name");
            return this.View();
        }

        // POST: Sales/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SaleCUViewModel sale)
        {
            if (this.ModelState.IsValid)
            {
                await this.saleService.CreateSaleAsync(sale.ClientID, sale.ProductDiscount, sale.DeadlineDate, sale.AddressID, sale.CityID, sale.CountryID);
                return this.RedirectToAction(nameof(Index));
            }

            this.ViewData["Clients"] = new SelectList(await this.clientService.GetAllClientsAsync(), "ClientID", "Name", sale.ClientID);
            this.ViewData["Addresses"] = new SelectList(await this.addressService.GetAllAddressesAsync(), "AddressID", "Name", sale.AddressID);
            this.ViewData["Cities"] = new SelectList(await this.cityService.GetAllCitiesAsync(), "CityID", "Name", sale.CityID);
            this.ViewData["Countries"] = new SelectList(await this.countryService.GetAllCountriesAsync(), "CountryID", "Name", sale.CountryID);

            return this.View(sale);
        }

        // GET: Sales/Edit/5
        [AuthorizeRolesAttribute(ROLES.OfficeStaff, ROLES.Admin)]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var sale = await this.saleService.GetSaleInfoAsync(id);
            if (sale == null)
            {
                return this.NotFound();
            }

            this.ViewData["Clients"] = new SelectList(await this.clientService.GetAllClientsAsync(), "ClientID", "Name", sale.ClientID);
            this.ViewData["Addresses"] = new SelectList(await this.addressService.GetAllAddressesAsync(), "AddressID", "Name", sale.AddressID);
            this.ViewData["Cities"] = new SelectList(await this.cityService.GetAllCitiesAsync(), "CityID", "Name", sale.CityID);
            this.ViewData["Countries"] = new SelectList(await this.countryService.GetAllCountriesAsync(), "CountryID", "Name", sale.CountryID);

            return this.View(this.saleCUMapper.MapFrom(sale));
        }

        // POST: Sales/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeRolesAttribute(ROLES.OfficeStaff, ROLES.Admin)]
        public async Task<IActionResult> Edit(int id, SaleCUViewModel sale)
        {
            if (id != sale.SaleID)
            {
                return this.NotFound();
            }

            if (this.ModelState.IsValid)
            {
                try
                {
                    await this.saleService.UpdateSaleAsync(
                        sale.SaleID, sale.ClientID, sale.ProductDiscount,
                        sale.OrderDate, (sale.DeadlineDate - sale.OrderDate).Days,
                        sale.DeliveryDate, sale.AddressID, sale.CityID, sale.CountryID);
                }
                catch (Exception ex)
                {
                    this.ModelState.AddModelError("Error", ex.Message);
                }
                return this.RedirectToAction(nameof(Index));
                //return this.View(sale);
            }

            this.ViewData["Clients"] = new SelectList(await this.clientService.GetAllClientsAsync(), "ClientID", "Name", sale.ClientID);
            this.ViewData["Addresses"] = new SelectList(await this.addressService.GetAllAddressesAsync(), "AddressID", "Name", sale.AddressID);
            this.ViewData["Cities"] = new SelectList(await this.cityService.GetAllCitiesAsync(), "CityID", "Name", sale.CityID);
            this.ViewData["Countries"] = new SelectList(await this.countryService.GetAllCountriesAsync(), "CountryID", "Name", sale.CountryID);

            return this.View(sale);
        }

        [AuthorizeRolesAttribute(ROLES.Admin)]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var sale = await this.saleService.GetSaleInfoAsync(id);
            if (sale == null)
            {
                return this.NotFound();
            }

            return this.View(this.saleInfoMapper.MapFrom(sale));
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [AuthorizeRolesAttribute(ROLES.Admin)]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                await this.saleService.DeleteSaleAsync(id);
            }
            catch (Exception ex)
            {
                this.ModelState.AddModelError("Error", ex.Message);
                return this.NotFound();
            }
            return this.RedirectToAction(nameof(Index));
        }
    }
}
