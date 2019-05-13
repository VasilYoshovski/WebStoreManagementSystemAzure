
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using StoreSystem.Data.Models;
using StoreSystem.Services;
using StoreSystem.Web.Mappers;
using StoreSystem.Web.Models.ProductViewModels;
using StoreSystem.Web.Models.OfferViewModels;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using StoreSystem.Data;
using StoreSystem.Web.Utils;

namespace StoreSystem.Web.Controllers
{
    public class OffersController : Controller
    {
        private readonly IOfferService offerService;
        private readonly ISaleService saleService;
        private readonly IClientService clientService;
        private readonly IAddressService addressService;
        private readonly ICityService cityService;
        private readonly ICountryService countryService;
        private readonly UserManager<StoreUser> userManager;
        private readonly IViewModelMapper<Offer, OfferInfoViewModel> offerInfoMapper;
        private readonly IViewModelMapper<Offer, OfferCUViewModel> offerCUMapper;
        private readonly IViewModelMapper<ProductOffer, ProductLineInfoViewModel> productOfferInfoMapper;

        public OffersController(
            IOfferService offerService,
            ISaleService saleService,
            IClientService clientService,
            IAddressService addressService,
            ICityService cityService,
            ICountryService countryService,
            UserManager<StoreUser> userManager,
            IViewModelMapper<Offer, OfferInfoViewModel> offerInfoMapper,
            IViewModelMapper<Offer, OfferCUViewModel> offerCUMapper,
            IViewModelMapper<ProductOffer, ProductLineInfoViewModel> productOfferInfoMapper)
        {
            this.offerService = offerService ?? throw new ArgumentNullException(nameof(offerService));
            this.saleService = saleService ?? throw new ArgumentNullException(nameof(saleService));
            this.clientService = clientService ?? throw new ArgumentNullException(nameof(clientService));
            this.addressService = addressService ?? throw new ArgumentNullException(nameof(addressService));
            this.cityService = cityService ?? throw new ArgumentNullException(nameof(cityService));
            this.countryService = countryService ?? throw new ArgumentNullException(nameof(countryService));
            this.userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            this.offerInfoMapper = offerInfoMapper ?? throw new ArgumentNullException(nameof(offerInfoMapper));
            this.offerCUMapper = offerCUMapper ?? throw new ArgumentNullException(nameof(offerCUMapper));
            this.productOfferInfoMapper = productOfferInfoMapper ?? throw new ArgumentNullException(nameof(productOfferInfoMapper));
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

        [AuthorizeRolesAttribute(ROLES.OfficeStaff, ROLES.Admin)]
        public async Task<IActionResult> CreateSale(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var offer = await this.offerService.GetOfferInfoAsync(id);
            if (offer == null)
            {
                return this.NotFound();
            }

            var sale =await saleService.CreateSaleByOfferIDAsync((int)id);



            return this.RedirectToAction("Details", "Sales", new { id = sale.SaleID });
        }

        // GET: Offers
        [AuthorizeRolesAttribute(ROLES.OfficeStaff, ROLES.Admin, ROLES.Client)]
        public async Task<IActionResult> Index()
        {
            int? clientId = null;
            if (User.IsInRole(ROLES.Client))
            {
                clientId = (await userManager.GetUserAsync(User))?.ClientId;
                if (clientId == null)
                {
                    return this.NotFound();
                }
            }
            var offers = await this.offerService.GetOffersWithTotalAsync(clientID: clientId);
            return this.View(new OfferIndexViewModel() { OffersList = offers, CanEdit = this.CanEdit() });
        }

        [AuthorizeRolesAttribute(ROLES.OfficeStaff, ROLES.Admin, ROLES.Client)]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var offer = await this.offerService.GetOfferInfoAsync(id);
            if (offer == null)
            {
                return this.NotFound();
            }

            var productsInOffer = await this.offerService.GetAllProductsInOfferAsync((int)id);
            var psViewModel = productsInOffer.Select(this.productOfferInfoMapper.MapFrom);

            var detailViewModel = new OfferDetailsViewModel()
            {
                ProductsInOffer = psViewModel.ToList(),
                OfferInfo = this.offerInfoMapper.MapFrom(offer),
                CanEdit = this.CanEdit()
            };

            return this.View(detailViewModel);
        }

        // GET: Offers/Create
        [AuthorizeRolesAttribute(ROLES.OfficeStaff, ROLES.Admin)]
        public async Task<IActionResult> Create()
        {
            this.ViewData["Clients"] = new SelectList(await this.clientService.GetAllClientsAsync(), "ClientID", "Name");
            this.ViewData["Addresses"] = new SelectList(await this.addressService.GetAllAddressesAsync(), "AddressID", "Name");
            this.ViewData["Cities"] = new SelectList(await this.cityService.GetAllCitiesAsync(), "CityID", "Name");
            this.ViewData["Countries"] = new SelectList(await this.countryService.GetAllCountriesAsync(), "CountryID", "Name");
            return this.View();
        }

        // POST: Offers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeRolesAttribute(ROLES.OfficeStaff, ROLES.Admin)]
        public async Task<IActionResult> Create(OfferCUViewModel offer)
        {
            if (this.ModelState.IsValid)
            {
                await this.offerService.CreateOfferAsync(offer.ClientID, offer.ProductDiscount, offer.DeadlineDate, offer.AddressID, offer.CityID, offer.CountryID);
                return this.RedirectToAction(nameof(Index));
            }

            this.ViewData["Clients"] = new SelectList(await this.clientService.GetAllClientsAsync(), "ClientID", "Name", offer.ClientID);
            this.ViewData["Addresses"] = new SelectList(await this.addressService.GetAllAddressesAsync(), "AddressID", "Name", offer.AddressID);
            this.ViewData["Cities"] = new SelectList(await this.cityService.GetAllCitiesAsync(), "CityID", "Name", offer.CityID);
            this.ViewData["Countries"] = new SelectList(await this.countryService.GetAllCountriesAsync(), "CountryID", "Name", offer.CountryID);

            return this.View(offer);
        }

        // GET: Offers/Edit/5
        [AuthorizeRolesAttribute(ROLES.OfficeStaff, ROLES.Admin)]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var offer = await this.offerService.GetOfferInfoAsync(id);
            if (offer == null)
            {
                return this.NotFound();
            }

            this.ViewData["Clients"] = new SelectList(await this.clientService.GetAllClientsAsync(), "ClientID", "Name", offer.ClientID);
            this.ViewData["Addresses"] = new SelectList(await this.addressService.GetAllAddressesAsync(), "AddressID", "Name", offer.AddressID);
            this.ViewData["Cities"] = new SelectList(await this.cityService.GetAllCitiesAsync(), "CityID", "Name", offer.CityID);
            this.ViewData["Countries"] = new SelectList(await this.countryService.GetAllCountriesAsync(), "CountryID", "Name", offer.CountryID);
            var a = this.offerCUMapper.MapFrom(offer);
            return this.View(this.offerCUMapper.MapFrom(offer));
        }

        // POST: Offers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeRolesAttribute(ROLES.OfficeStaff, ROLES.Admin)]
        public async Task<IActionResult> Edit(int id, OfferCUViewModel offer)
        {
            if (id != offer.OfferID)
            {
                return this.NotFound();
            }

            if (this.ModelState.IsValid)
            {
                try
                {
                    await this.offerService.UpdateOfferAsync(
                        offer.OfferID, offer.ClientID, offer.ProductDiscount,
                        offer.OrderDate, (offer.DeadlineDate - offer.OrderDate).Days,
                        offer.DeliveryDate, offer.AddressID, offer.CityID, offer.CountryID);
                }
                catch (Exception ex)
                {
                    this.ModelState.AddModelError("Error", ex.Message);
                }
                return this.RedirectToAction(nameof(Index));
            }

            this.ViewData["Clients"] = new SelectList(await this.clientService.GetAllClientsAsync(), "ClientID", "Name", offer.ClientID);
            this.ViewData["Addresses"] = new SelectList(await this.addressService.GetAllAddressesAsync(), "AddressID", "Name", offer.AddressID);
            this.ViewData["Cities"] = new SelectList(await this.cityService.GetAllCitiesAsync(), "CityID", "Name", offer.CityID);
            this.ViewData["Countries"] = new SelectList(await this.countryService.GetAllCountriesAsync(), "CountryID", "Name", offer.CountryID);

            return this.View(offer);
        }

        [AuthorizeRolesAttribute(ROLES.Admin)]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var offer = await this.offerService.GetOfferInfoAsync(id);
            if (offer == null)
            {
                return this.NotFound();
            }

            return this.View(offerInfoMapper.MapFrom(offer));
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [AuthorizeRolesAttribute(ROLES.Admin)]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                await this.offerService.DeleteOfferAsync(id);
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
