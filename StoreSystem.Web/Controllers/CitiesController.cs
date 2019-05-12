using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StoreSystem.Data;
using StoreSystem.Data.DbContext;
using StoreSystem.Data.Models;
using StoreSystem.Services;
using System;
using System.Threading.Tasks;

namespace StoreSystem.Web.Controllers
{
    public class CitiesController : Controller
    {
        private readonly ICityService cityService;

        public CitiesController(ICityService cityService)
        {
            this.cityService = cityService ?? throw new ArgumentNullException(nameof(cityService));
        }

        // GET: Populate Cities From JSON
        [Authorize(Roles = ROLES.AdminAndOfficeStaff)]
        public async Task<IActionResult> PopulateCitiesFromJSON()
        {
            //var ddd = this.Configuration.GetSection("PathsForInitialDatabaseData").GetValue<bool>("IsEnabled");
            this.ViewData["Title"] = "List of cities";
            var createdCitiesCount = await this.cityService.PopulateCitiesFromJSONAsync(
                "..\\DatabaseInitializationArchiveJSON",
                "CitiesInitial.json");
            return RedirectToAction(nameof(Index));
        }

        // GET: Cities
        [Authorize(Roles = ROLES.AdminAndOfficeStaff)]
        public async Task<IActionResult> Index(string searchString)
        {
            this.ViewData["Title"] = "List of cities";
            var result = searchString == null ?
                await this.cityService.GetAllCitiesAsync() :
                await this.cityService.GetAllCitiesByFilterAsync(0, int.MaxValue, searchString);
            return View(result);
        }

        // GET: Cities/Details/5
        [HttpGet]
        [Authorize(Roles = ROLES.AdminAndOfficeStaff)]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var city = await this.cityService.FindCityByIDAsync(id ?? -1);
            if (city == null)
            {
                return NotFound();
            }

            return View(city);
        }

        // GET: Cities/Create
        [HttpGet]
        [Authorize(Roles = ROLES.AdminAndOfficeStaff)]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Cities/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = ROLES.AdminAndOfficeStaff)]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CityID,Name")] City city)
        {
            if (ModelState.IsValid)
            {
                await this.cityService.CreateCityAsync(city.Name);
                return RedirectToAction(nameof(Index));
            }
            return View(city);
        }

        // GET: Cities/Edit/5
        [HttpGet]
        [Authorize(Roles = ROLES.AdminAndOfficeStaff)]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var city = await this.cityService.FindCityByIDAsync(id ?? -1);
            if (city == null)
            {
                return NotFound();
            }
            return View(city);
        }

        // POST: Cities/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = ROLES.AdminAndOfficeStaff)]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CityID,Name")] City city)
        {
            if (id != city.CityID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await this.cityService.UpdateCityAsync(city.CityID, city.Name);
                return RedirectToAction(nameof(Index));
            }
            return View(city);
        }

        // GET: Cities/Delete/5
        [Authorize(Roles = ROLES.Admin)]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var city = await this.cityService.FindCityByIDAsync(id ?? -1);
            if (city == null)
            {
                return NotFound();
            }

            return View(city);
        }

        // POST: Cities/Delete/5
        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = ROLES.Admin)]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var city = await this.cityService.DeleteCityAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
