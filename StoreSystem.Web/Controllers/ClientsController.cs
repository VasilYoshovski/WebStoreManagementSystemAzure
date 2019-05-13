using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StoreSystem.Data;
using StoreSystem.Data.DbContext;
using StoreSystem.Data.Models;
using StoreSystem.Services;
using StoreSystem.Web.Mappers;
using StoreSystem.Web.Models.ClientViewModels;
using StoreSystem.Web.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreSystem.Web.Controllers
{
    public class ClientsController : Controller
    {
        private readonly IClientService clientService;
        private readonly IAddressService addressService;
        private readonly ICityService cityService;
        private readonly ICountryService countryService;
        private readonly IStoreUserService storeUserService;
        private readonly IDatabaseService databaseService;
        private readonly IViewModelMapper<Client, ClientInfoViewModel> clientInfoMapper;
        private readonly IViewModelMapper<Client, ClientCUViewModel> clientCUMapper;

        public ClientsController(
            IClientService clientService,
            IAddressService addressService,
            ICityService cityService,
            ICountryService countryService,
            IStoreUserService storeUserService,
            IViewModelMapper<Client, ClientInfoViewModel> clientInfoMapper,
            IViewModelMapper<Client, ClientCUViewModel> clientCUMapper,
            IDatabaseService databaseService
            )
        {
            this.clientService = clientService ?? throw new ArgumentNullException(nameof(clientService));
            this.addressService = addressService ?? throw new ArgumentNullException(nameof(addressService));
            this.cityService = cityService ?? throw new ArgumentNullException(nameof(cityService));
            this.countryService = countryService ?? throw new ArgumentNullException(nameof(countryService));
            this.storeUserService = storeUserService ?? throw new ArgumentNullException(nameof(storeUserService));
            this.databaseService = databaseService;
            this.clientInfoMapper = clientInfoMapper ?? throw new ArgumentNullException(nameof(clientInfoMapper));
            this.clientCUMapper = clientCUMapper ?? throw new ArgumentNullException(nameof(clientCUMapper));
        }

        // GET: Clients
        [AuthorizeRolesAttribute(ROLES.OfficeStaff, ROLES.Admin)]
        public async Task<IActionResult> Index()
        {
            var clientsList = await this.clientService.GetAllClientsWithAddressAsync();

            var res = clientsList.Select(this.clientInfoMapper.MapFrom).ToList();
            foreach (var item in res)
            {
                item.StoreUsername = await this.storeUserService.GetUserNameByUserId(item.StoreUserId);
            }
            return this.View(res);
        }

        // GET: Clients/Details/5
        [AuthorizeRolesAttribute(ROLES.OfficeStaff, ROLES.Admin)]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var client = await this.clientService.FindClientWithAddressAsync((int)id);

            if (client == null)
            {
                return this.NotFound();
            }

            var res = this.clientInfoMapper.MapFrom(client);

            res.StoreUsername = await this.storeUserService.GetUserNameByUserId(res.StoreUserId);

            return this.View(res);
        }

        // GET: Clients/Create
        [AuthorizeRolesAttribute(ROLES.OfficeStaff, ROLES.Admin)]
        public async Task<IActionResult> Create()
        {
            this.ViewData["Addresses"] = new SelectList(await this.addressService.GetAllAddressesAsync(), "AddressID", "Name");
            this.ViewData["Cities"] = new SelectList(await this.cityService.GetAllCitiesAsync(), "CityID", "Name");
            this.ViewData["Countries"] = new SelectList(await this.countryService.GetAllCountriesAsync(), "CountryID", "Name");
            this.ViewData["Users"] = new SelectList(await this.storeUserService.GetVisitorUsers(), "Id", "UserName");
            return this.View();
        }

        // POST: Clients/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeRolesAttribute(ROLES.OfficeStaff, ROLES.Admin)]
        public async Task<IActionResult> Create(ClientCUViewModel client)
        {
            if (this.ModelState.IsValid)
            {
                await this.clientService.CreateClientAsync(client.Name, client.UIN, client.AddressId, client.CityId, client.CountryId, client.StoreUserId);
                return this.RedirectToAction(nameof(Index));
            }
            this.ViewData["Addresses"] = new SelectList(await this.addressService.GetAllAddressesAsync(), "AddressID", "Name", client.AddressId);
            this.ViewData["Cities"] = new SelectList(await this.cityService.GetAllCitiesAsync(), "CityID", "Name", client.CityId);
            this.ViewData["Countries"] = new SelectList(await this.countryService.GetAllCountriesAsync(), "CountryID", "Name", client.CountryId);
            return this.View(client);
        }

        // GET: Clients/Edit/5
        [AuthorizeRolesAttribute(ROLES.OfficeStaff, ROLES.Admin)]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var client = await this.clientService.FindClientByIDAsync((int)id);
            if (client == null)
            {
                return this.NotFound();
            }
            this.ViewData["Addresses"] = new SelectList(await this.addressService.GetAllAddressesAsync(), "AddressID", "Name", client.AddressID);
            this.ViewData["Cities"] = new SelectList(await this.cityService.GetAllCitiesAsync(), "CityID", "Name", client.CityID);
            this.ViewData["Countries"] = new SelectList(await this.countryService.GetAllCountriesAsync(), "CountryID", "Name", client.CountryID);

            return this.View(this.clientCUMapper.MapFrom(client));
        }

        // POST: Clients/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeRolesAttribute(ROLES.OfficeStaff, ROLES.Admin)]
        public async Task<IActionResult> Edit(int id, ClientCUViewModel client)
        {
            if (id != client.ClientID)
            {
                return this.NotFound();
            }

            if (this.ModelState.IsValid)
            {
                try
                {
                    await this.clientService.UpdateClientAsync(client.ClientID, client.Name, client.UIN, client.AddressId, client.CityId, client.CountryId);
                }
                catch (Exception ex)
                {
                    this.ModelState.AddModelError("Error", ex.Message);
                }
                return this.View(client);
            }
            this.ViewData["Addresses"] = new SelectList(await this.addressService.GetAllAddressesAsync(), "AddressID", "Name", client.AddressId);
            this.ViewData["Cities"] = new SelectList(await this.cityService.GetAllCitiesAsync(), "CityID", "Name", client.CityId);
            this.ViewData["Countries"] = new SelectList(await this.countryService.GetAllCountriesAsync(), "CountryID", "Name", client.CountryId);

            return this.View(client);
        }

        // GET: Clients/Delete/5
        [AuthorizeRolesAttribute(ROLES.Admin)]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var client = await this.clientService.FindClientWithAddressAsync((int)id);
            if (client == null)
            {
                return this.NotFound();
            }

            return this.View(this.clientInfoMapper.MapFrom(client));
            //return this.View(client);
        }

        // POST: Clients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [AuthorizeRolesAttribute(ROLES.Admin)]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var userId = await this.clientService.DeleteClient(id);
            await this.databaseService.ChangeUserRoleAsync(userId, ROLES.Visitor);
            return this.RedirectToAction(nameof(Index));
        }


    }
}
