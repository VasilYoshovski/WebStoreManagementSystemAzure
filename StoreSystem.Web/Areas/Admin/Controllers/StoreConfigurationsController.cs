using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StoreSystem.Data;

namespace StoreSystem.Web.Controllers
{
    [Area("Admin")]
    public class StoreConfigurationsController : Controller
    {
        // GET: StoreServices
        [Authorize(Roles = ROLES.AdminOrOfficeStaff)]
        public ActionResult Index()
        {
            return View();
        }
    }
}
