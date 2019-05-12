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
    public class CountriesController : Controller
    {
        private readonly ICountryService countryService;

        public CountriesController(ICountryService countryService)
        {
            this.countryService = countryService ?? throw new ArgumentNullException(nameof(countryService));
        }

        // GET: Populate Countries From JSON
        [Authorize(Roles = ROLES.AdminAndOfficeStaff)]
        public async Task<IActionResult> PopulateCountriesFromJSON()
        {
            //var ddd = this.Configuration.GetSection("PathsForInitialDatabaseData").GetValue<bool>("IsEnabled");
            this.ViewData["Title"] = "List of countries";
            var createdCitiesCount = await this.countryService.PopulateCountriesFromJSONAsync(
                "..\\DatabaseInitializationArchiveJSON",
                "CountriesInitial.json");
            return RedirectToAction(nameof(Index));
        }

        // GET: Countries
        [Authorize(Roles = ROLES.AdminAndOfficeStaff)]
        public async Task<IActionResult> Index(string searchString)
        {
            this.ViewData["Title"] = "List of countries";
            var result = searchString == null ?
                await this.countryService.GetAllCountriesAsync() :
                await this.countryService.GetAllCountriesByFilterAsync(0, int.MaxValue, searchString);
            return View(result);
        }

        // GET: Countries/Details/5
        [HttpGet]
        [Authorize(Roles = ROLES.AdminAndOfficeStaff)]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var country = await this.countryService.FindCountryByIDAsync(id ?? -1);
            if (country == null)
            {
                return NotFound();
            }

            return View(country);
        }

        // GET: Countries/Create
        [HttpGet]
        [Authorize(Roles = ROLES.AdminAndOfficeStaff)]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Countries/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = ROLES.AdminAndOfficeStaff)]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CountryID,Name")] Country country)
        {
            if (ModelState.IsValid)
            {
                await this.countryService.CreateCountryAsync(country.Name);
                return RedirectToAction(nameof(Index));
            }
            return View(country);
        }

        // GET: Countries/Edit/5
        [HttpGet]
        [Authorize(Roles = ROLES.AdminAndOfficeStaff)]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var country = await this.countryService.FindCountryByIDAsync(id ?? -1);
            if (country == null)
            {
                return NotFound();
            }
            return View(country);
        }

        // POST: Countries/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = ROLES.AdminAndOfficeStaff)]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CountryID,Name")] Country country)
        {
            if (id != country.CountryID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await this.countryService.UpdateCountryAsync(country.CountryID, country.Name);
                return RedirectToAction(nameof(Index));
            }
            return View(country);
        }

        // GET: Countries/Delete/5
        [HttpGet]
        [Authorize(Roles = ROLES.Admin)]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var country = await this.countryService.FindCountryByIDAsync(id ?? -1);
            if (country == null)
            {
                return NotFound();
            }

            return View(country);
        }

        // POST: Countries/Delete/5
        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = ROLES.Admin)]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var country = await this.countryService.DeleteCountryAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
