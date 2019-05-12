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

        [Route("ToPlain")]
        [HttpPost]
        public IActionResult Plain([FromBody] HtmlSource htmlSource) //string htmlContent
        {
           // var myObject = JsonConvert.DeserializeObject<string>(htmlSource.Source);
           
            return this.View("Plain.cshtml");
        }

        [Route("Pdf")]
        [HttpPost]
        public void PdfGo([FromBody] HtmlSource htmlSource) //string htmlContent
        {
            //var converter = new BasicConverter(new PdfTools());
            CustomAssemblyLoadContext context = new CustomAssemblyLoadContext();
            context.LoadUnmanagedLibrary(@"C:\Users\stani\Desktop\storemanagementsystemweb\StoreSystem.Web\libwkhtmltox.dll");

            var converter = new SynchronizedConverter(new PdfTools());


            var doc = new HtmlToPdfDocument()
            {
                GlobalSettings = {
                        ColorMode = ColorMode.Color,
                        Orientation = Orientation.Portrait,
                        PaperSize = PaperKind.A4,
                        Margins = new MarginSettings() { Top = 10 },
                        Out = @"D:\test.pdf",
                    },
                Objects = {
                    new ObjectSettings() {
                        PagesCount = true,
                        //HtmlContent = htmlSource.Source,
                        Page = htmlSource.Source,
                        WebSettings = { DefaultEncoding = "utf-8" },
                        HeaderSettings = { FontSize = 9, Right = "Page [page] of [toPage]", Line = true, Spacing = 2.812 }
                    }
                }
            };

            //var doc = new HtmlToPdfDocument()
            //{
            //    GlobalSettings = {
            //        ColorMode = ColorMode.Color,
            //        Orientation = Orientation.Portrait,
            //        PaperSize = PaperKind.A4,
            //        Margins = new MarginSettings() { Top = 10 },
            //        Out = @"D:\test.pdf",
            //    },
            //    Objects = {
            //        new ObjectSettings()
            //        {
            //            Page = urlParam,
            //        },
            //    }
            //};

            converter.Convert(doc);
        }
    }

    public class HtmlSource
    {
        public string Source { get; set; }
    }
}
