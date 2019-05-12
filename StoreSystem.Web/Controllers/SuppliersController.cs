using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StoreSystem.Data;
using StoreSystem.Data.DbContext;
using StoreSystem.Data.Models;
using StoreSystem.Services;
using StoreSystem.Web.Models.SupplierViewModels;

namespace StoreSystem.Web.Controllers
{
    public class SuppliersController : Controller
    {
        private readonly UserManager<StoreUser> UserManager;
        private readonly ISupplierService supplierService;
        private readonly IDatabaseService databaseService;
        private readonly IAddressService addressService;
        private readonly ICityService cityCervice;
        private readonly ICountryService countryCervice;

        public SuppliersController(
            UserManager<StoreUser> userManager,
            ISupplierService supplierService,
            IDatabaseService databaseService,
            IAddressService addressService,
            ICityService cityCervice,
            ICountryService countryCervice)
        {
            this.UserManager = userManager;
            this.databaseService = databaseService;
            this.addressService = addressService;
            this.cityCervice = cityCervice;
            this.countryCervice = countryCervice;
            this.supplierService = supplierService;
        }

        // GET: Suppliers
        [HttpGet]
        [Authorize(Roles = ROLES.AdminAndOfficeStaff)]
        public async Task<IActionResult> Index()
        {
            var usersList = await this.supplierService.GetAllSuppliersAsync();
            return View(usersList);
        }

        // Post: Suppliers
        [HttpPost]
        [Authorize(Roles = ROLES.AdminAndOfficeStaff)]
        public async Task<IActionResult> Index(string searchString)
        {
            var usersList = await this.supplierService.GetAllSuppliersByFilterAsync(0, int.MaxValue, searchString);
            return View(usersList);
        }

        // GET: Suppliers/Details/5
        [Authorize(Roles = ROLES.AdminAndOfficeStaff)]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var supplier = await this.supplierService.GetSupplierByIDAsync(id ?? -1);
            if (supplier == null)
            {
                return NotFound();
            }

            return View(supplier);
        }

        // GET: Suppliers/Create
        [Authorize(Roles = ROLES.AdminAndOfficeStaff)]
        public async Task<IActionResult> Create()
        {
            var visitorUsersSelectList = (await databaseService.GetUsersNameAndIDByRoleAsync(ROLES.Visitor))
                .Select(m => new SelectListItem()
                {
                    Text = m.userName,
                    Value = m.userID,
                });
            ViewData["MUserID"] = visitorUsersSelectList.OrderBy(x => x.Text);
            ViewData["AddressID"] = new SelectList(await this.addressService.GetAllAddressesAsync(), "AddressID", "Name");
            ViewData["CityID"] = new SelectList(await this.cityCervice.GetAllCitiesAsync(), "CityID", "Name");
            ViewData["CountryID"] = new SelectList(await this.countryCervice.GetAllCountriesAsync(), "CountryID", "Name");
            return View();
        }

        // POST: Suppliers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = ROLES.AdminAndOfficeStaff)]
        public async Task<IActionResult> Create(SupplierViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                var visitorUsersSelectList = (await databaseService.GetUsersNameAndIDByRoleAsync(ROLES.Visitor))
                    .Select(m => new SelectListItem()
                    {
                        Text = m.userName,
                        Value = m.userID,
                    });
                ViewData["MUserID"] = visitorUsersSelectList.OrderBy(x => x.Text);
                ViewData["AddressID"] = new SelectList(await this.addressService.GetAllAddressesAsync(), "AddressID", "Name", model.AddressID);
                ViewData["CityID"] = new SelectList(await this.cityCervice.GetAllCitiesAsync(), "CityID", "Name", model.CityID);
                ViewData["CountryID"] = new SelectList(await this.countryCervice.GetAllCountriesAsync(), "CountryID", "Name", model.CountryID);
                return View(model);
            }

            try
            {
                var supplier = this.supplierService.CreateSupplierAsync(
                    model.Name, model.UIN, model.CountryID, model.CityID, model.AddressID, model.MicrosoftUserID, true).Result;

                return RedirectToAction(nameof(Details), new { id = supplier.SupplierID });
            }
            catch (ArgumentException ex)
            {
                this.ModelState.AddModelError("Error", ex.Message);
                return View(model);
            }
        }

        // GET: Suppliers/Edit/5
        [Authorize(Roles = ROLES.AdminAndOfficeStaff)]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var supplier = await this.supplierService.GetSupplierByIDAsync(id ?? -1);
            if (supplier == null)
            {
                return NotFound();
            }
            ViewData["AddressID"] = new SelectList(await this.addressService.GetAllAddressesAsync(), "AddressID", "Name", supplier.AddressID);
            ViewData["CityID"] = new SelectList(await this.cityCervice.GetAllCitiesAsync(), "CityID", "Name", supplier.CityID);
            ViewData["CountryID"] = new SelectList(await this.countryCervice.GetAllCountriesAsync(), "CountryID", "Name", supplier.CountryID);
            return View(supplier);
        }

        // POST: Suppliers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = ROLES.AdminAndOfficeStaff)]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SupplierID,Name,UIN,AddressID,CityID,CountryID,StoreUserId")] Supplier supplier)
        {
            if (id != supplier.SupplierID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await this.supplierService.UpdateSupplierAsync(
                    id,
                    supplier.Name,
                    supplier.UIN,
                    supplier.CountryID,
                    supplier.CityID,
                    supplier.AddressID,
                    supplier.StoreUserId,
                    false);
                return RedirectToAction(nameof(Index));
            }
            ViewData["AddressID"] = new SelectList(await this.addressService.GetAllAddressesAsync(), "AddressID", "Name", supplier.AddressID);
            ViewData["CityID"] = new SelectList(await this.cityCervice.GetAllCitiesAsync(), "CityID", "Name", supplier.CityID);
            ViewData["CountryID"] = new SelectList(await this.countryCervice.GetAllCountriesAsync(), "CountryID", "Name", supplier.CountryID);
            return View(supplier);
        }

        // GET: Suppliers/Delete/5
        [Authorize(Roles = ROLES.Admin)]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var supplier = await this.supplierService.GetSupplierByIDAsync(id ?? -1);
            if (supplier == null)
            {
                return NotFound();
            }

            return View(supplier);
        }

        // POST: Suppliers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = ROLES.Admin)]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await this.supplierService.DeleteSupplierAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
