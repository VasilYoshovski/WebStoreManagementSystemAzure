using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using StoreSystem.Data;
using StoreSystem.Data.Models;
using StoreSystem.Services;
using System.Threading.Tasks;

namespace StoreSystem.Web.Controllers
{
    public class WarehousesController : Controller
    {
        private readonly IWarehouseService warehouseService;
        private readonly IAddressService addressService;
        private readonly ICityService cityCervice;
        private readonly ICountryService countryCervice;

        public WarehousesController(
            IWarehouseService warehouseService,
            IAddressService addressService,
            ICityService cityCervice,
            ICountryService countryCervice)
        {
            this.warehouseService = warehouseService;
            this.addressService = addressService;
            this.cityCervice = cityCervice;
            this.countryCervice = countryCervice;
        }

        // GET: Warehouses
        [Authorize(Roles = ROLES.AdminOrOfficeStaffOrClientOrSupplier)]
        public async Task<IActionResult> Index(string searchString)
        {
            this.ViewData["Title"] = "List of warehouses";
            var result = searchString == null ?
                await this.warehouseService.GetAllWarehousesAsync() :
                await this.warehouseService.GetAllWarehousesByFilterAsync(0, int.MaxValue, searchString);
            return View(result);
        }

        // GET: Warehouses/Details/5
        [Authorize(Roles = ROLES.AdminOrOfficeStaffOrClientOrSupplier)]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var warehouse = await this.warehouseService.FindWarehouseByIDAsync(id ?? -1);
            if (warehouse == null)
            {
                return NotFound();
            }

            return View(warehouse);
        }

        // GET: Warehouses/Create
        [Authorize(Roles = ROLES.AdminOrOfficeStaff)]
        public async Task<IActionResult> Create()
        {
            ViewData["AddressID"] = new SelectList(await this.addressService.GetAllAddressesAsync(), "AddressID", "Name");
            ViewData["CityID"] = new SelectList(await this.cityCervice.GetAllCitiesAsync(), "CityID", "Name");
            ViewData["CountryID"] = new SelectList(await this.countryCervice.GetAllCountriesAsync(), "CountryID", "Name");
            return View();
        }

        // POST: Warehouses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = ROLES.AdminOrOfficeStaff)]
        public async Task<IActionResult> Create([Bind("WarehouseID,Name,AddressID,CityID,CountryID")] Warehouse warehouse)
        {
            if (ModelState.IsValid)
            {
                await this.warehouseService.CreateWarehouseAsync(
                    warehouse.Name,
                    warehouse.CountryID,
                    warehouse.CityID,
                    warehouse.AddressID);
                return RedirectToAction(nameof(Index));
            }
            ViewData["AddressID"] = new SelectList(await this.addressService.GetAllAddressesAsync(), "AddressID", "Name", warehouse.AddressID);
            ViewData["CityID"] = new SelectList(await this.cityCervice.GetAllCitiesAsync(), "CityID", "Name", warehouse.CityID);
            ViewData["CountryID"] = new SelectList(await this.countryCervice.GetAllCountriesAsync(), "CountryID", "Name", warehouse.CountryID);
            return View(warehouse);
        }

        // GET: Warehouses/Edit/5
        [Authorize(Roles = ROLES.AdminOrOfficeStaff)]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var warehouse = await this.warehouseService.GetWarehouseByIDAsync(id ?? -1);
            if (warehouse == null)
            {
                return NotFound();
            }
            ViewData["AddressID"] = new SelectList(await this.addressService.GetAllAddressesAsync(), "AddressID", "Name", warehouse.AddressID);
            ViewData["CityID"] = new SelectList(await this.cityCervice.GetAllCitiesAsync(), "CityID", "Name", warehouse.CityID);
            ViewData["CountryID"] = new SelectList(await this.countryCervice.GetAllCountriesAsync(), "CountryID", "Name", warehouse.CountryID);
            return View(warehouse);
        }

        // POST: Warehouses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = ROLES.AdminOrOfficeStaff)]
        public async Task<IActionResult> Edit(int id, [Bind("WarehouseID,Name,AddressID,CityID,CountryID")] Warehouse warehouse)
        {
            if (id != warehouse.WarehouseID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await this.warehouseService.UpdateWarehouseAsync(
                    id,
                    warehouse.Name,
                    warehouse.CountryID,
                    warehouse.CityID,
                    warehouse.AddressID);
                return RedirectToAction(nameof(Index));
            }
            ViewData["AddressID"] = new SelectList(await this.addressService.GetAllAddressesAsync(), "AddressID", "Name", warehouse.AddressID);
            ViewData["CityID"] = new SelectList(await this.cityCervice.GetAllCitiesAsync(), "CityID", "Name", warehouse.CityID);
            ViewData["CountryID"] = new SelectList(await this.countryCervice.GetAllCountriesAsync(), "CountryID", "Name", warehouse.CountryID);
            return View(warehouse);
        }

        // GET: Warehouses/Delete/5
        [Authorize(Roles = ROLES.Admin)]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var warehouse = await this.warehouseService.FindWarehouseByIDAsync(id ?? -1);
            if (warehouse == null)
            {
                return NotFound();
            }

            return View(warehouse);
        }

        // POST: Warehouses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = ROLES.Admin)]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await this.warehouseService.DeleteWarehouseAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
