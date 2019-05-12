using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StoreSystem.Data;

namespace StoreSystem.Web.Controllers
{
    [Area("Admin")]
    public class StoreConfigurationsController : Controller
    {
        // GET: StoreServices
        [Authorize(Roles = ROLES.Admin)]
        public ActionResult Index()
        {
            return View();
        }
    }
}
