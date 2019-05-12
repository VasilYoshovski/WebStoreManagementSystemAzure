using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StoreSystem.Data;
using StoreSystem.Data.Models;
using StoreSystem.Services;
using System;
using System.Threading.Tasks;

namespace StoreSystem.Web.Controllers
{
    public class AddressesController : Controller
    {
        private readonly IAddressService addressService;

        public AddressesController(IAddressService addressService)
        {
            this.addressService = addressService ?? throw new ArgumentNullException(nameof(addressService));
        }

        // GET: Addresses
        [Authorize(Roles = ROLES.AdminAndOfficeStaff)]
        public async Task<IActionResult> Index(string searchString)
        {
            this.ViewData["Title"] = "List of addresses";
            var result = searchString == null ?
                await this.addressService.GetAllAddressesAsync() :
                await this.addressService.GetAllAddressesByFilterAsync(0, int.MaxValue, searchString);
            return View(result);
        }

        // GET: Addresses/Details/5
        [HttpGet]
        [Authorize(Roles = ROLES.AdminAndOfficeStaff)]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var address = await this.addressService.FindAddressByIDAsync(id ?? -1);
            if (address == null)
            {
                return NotFound();
            }

            return View(address);
        }

        // GET: Addresses/Create
        [HttpGet]
        [Authorize(Roles = ROLES.AdminAndOfficeStaff)]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Addresses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = ROLES.AdminAndOfficeStaff)]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AddressID,Name")] Address address)
        {
            if (ModelState.IsValid)
            {
                await this.addressService.CreateAddressAsync(address.Name);
                return RedirectToAction(nameof(Index));
            }
            return View(address);
        }

        // GET: Addresses/Edit/5
        [Authorize(Roles = ROLES.AdminAndOfficeStaff)]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var address = await this.addressService.FindAddressByIDAsync(id ?? -1);
            if (address == null)
            {
                return NotFound();
            }
            return View(address);
        }

        // POST: Addresses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = ROLES.AdminAndOfficeStaff)]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AddressID,Name")] Address address)
        {
            if (id != address.AddressID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await this.addressService.UpdateAddressAsync(address.AddressID, address.Name);
                return RedirectToAction(nameof(Index));
            }
            return View(address);
        }

        // GET: Addresses/Delete/5
        [Authorize(Roles = ROLES.Admin)]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var address = await this.addressService.FindAddressByIDAsync(id ?? -1);
            if (address == null)
            {
                return NotFound();
            }

            return View(address);
        }

        // POST: Addresses/Delete/5
        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = ROLES.Admin)]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var address = await this.addressService.DeleteAddressAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
