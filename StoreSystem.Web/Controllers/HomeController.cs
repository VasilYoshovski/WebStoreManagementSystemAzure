using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using StoreSystem.Data.Models;
using StoreSystem.Web.Models;

namespace StoreSystem.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserManager<StoreUser> UserManager;
        private readonly IMemoryCache memoryCache;

        public HomeController(UserManager<StoreUser> userManager, IMemoryCache memoryCache)
        {
            UserManager = userManager ?? throw new System.ArgumentNullException(nameof(userManager));
            this.memoryCache = memoryCache ?? throw new System.ArgumentNullException(nameof(memoryCache));
        }

        public IActionResult Index()
        {
            var currentUser = this.UserManager.GetUserAsync(User).Result;
            if (currentUser != null)
            {
                ViewData["CurrentUserRoles"] = new List<string>(this.UserManager.GetRolesAsync(currentUser).Result);
            }
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
