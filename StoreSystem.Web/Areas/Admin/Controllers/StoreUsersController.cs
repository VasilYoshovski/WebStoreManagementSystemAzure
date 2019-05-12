using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StoreSystem.Data;
using StoreSystem.Data.DbContext;
using StoreSystem.Data.Models;
using StoreSystem.Services;
using StoreSystem.Services.Contracts;
using StoreSystem.Web.Constants;
using StoreSystem.Web.Mappers;
using StoreSystem.Web.Models.StoreUserViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreSystem.Web.Controllers
{
    [Area("Admin")]
    public class StoreUsersController : Controller
    {
        private readonly UserManager<StoreUser> userManager;
        private readonly IStoreUserService storeUserService;
        private readonly IDatabaseService databaseService;
        private readonly IManageService manageService;
        private readonly IViewModelMapper<StoreUser, StoreUserViewModel> storeUserMapper;
        private readonly IViewModelMapper<List<StoreUser>, StoreUsersCollectionViewModel> storeUsersCollectionMapper;

        public StoreUsersController(
            UserManager<StoreUser> userManager,
            IStoreUserService storeUserService,
            IDatabaseService databaseService,
            IManageService manageService,
            IViewModelMapper<StoreUser, StoreUserViewModel> storeUserMapper,
            IViewModelMapper<List<StoreUser>, StoreUsersCollectionViewModel> storeUsersCollectionMapper)
        {
            this.userManager = userManager;
            this.storeUserService = storeUserService;
            this.databaseService = databaseService;
            this.manageService = manageService;
            this.storeUserMapper = storeUserMapper;
            this.storeUsersCollectionMapper = storeUsersCollectionMapper;
        }

        // GET: StoreServices
        [Authorize(Roles = ROLES.Admin)]
        public async Task<ActionResult> UsersGroupedByRole()
        {
            var userRoles = await this.manageService.GetAllRolesAsync();
            return View(userRoles);
        }

        // GET: Users
        [HttpGet]
        [Authorize(Roles = ROLES.AdminOrOfficeStaff)]
        public async Task<IActionResult> Index()
        {
            var usersList = await this.manageService.GetAllUsersAsync();

            var clientsList = await this.databaseService.GetUsersNameAndIDByRoleAsync(ROLES.Client);
            var clientsNameList = clientsList.Select(x => x.userName).ToList();
            usersList = usersList.Where(u => clientsNameList.All(cn => cn != u.UserName)).ToList();

            var supplierList = await this.databaseService.GetUsersNameAndIDByRoleAsync(ROLES.Supplier);
            var suppliersNameList = supplierList.Select(x => x.userName).ToList();
            usersList = usersList.Where(u => suppliersNameList.All(cn => cn != u.UserName)).ToList();

            //var mappedList23 = usersList.Select(d => this.storeUserMapper.MapFrom(d)).ToList();
            var mappedList = this.storeUsersCollectionMapper.MapFrom(usersList).StoreUsers.ToList();
            for (int i = 0; i < mappedList.Count; i++)
            {
                var ffff = usersList.Where(u => u.Id == mappedList[i].UserID).FirstOrDefault();
                mappedList[i].Role = (await this.userManager.GetRolesAsync(ffff)).FirstOrDefault();
            }
            return View(mappedList);
        }

        // Post: Users
        [HttpPost]
        [Authorize(Roles = ROLES.AdminOrOfficeStaff)]
        public async Task<IActionResult> Index(string searchString)
        {
            var usersList = await this.manageService.GetAllUsersByFilterAsync(0, int.MaxValue, searchString);

            var clientsList = await this.databaseService.GetUsersNameAndIDByRoleAsync(ROLES.Client);
            var clientsNameList = clientsList.Select(x => x.userName).ToList();
            usersList = usersList.Where(u => clientsNameList.All(cn => cn != u.UserName)).ToList();

            var supplierList = await this.databaseService.GetUsersNameAndIDByRoleAsync(ROLES.Supplier);
            var suppliersNameList = supplierList.Select(x => x.userName).ToList();
            usersList = usersList.Where(u => suppliersNameList.All(cn => cn != u.UserName)).ToList();

            //var mappedList23 = usersList.Select(d => this.storeUserMapper.MapFrom(d)).ToList();
            var mappedList = this.storeUsersCollectionMapper.MapFrom(usersList).StoreUsers.ToList();
            for (int i = 0; i < mappedList.Count; i++)
            {
                var ffff = usersList.Where(u => u.Id == mappedList[i].UserID).FirstOrDefault();
                mappedList[i].Role = (await this.userManager.GetRolesAsync(ffff)).FirstOrDefault();
            }
            return View(mappedList);
        }

        //// GET: StoreUsers/SetDefaultPassword
        //[Authorize(Roles = ROLES.Admin)]
        //public async Task<ActionResult> SetDefaultPassword(string userID, string newPasswodString)
        //{
        //    return RedirectToAction("Index");
        //}

        // GET: StoreUsers/ChangeUserRoleToVisitor
        [Authorize(Roles = ROLES.Admin)]
        public async Task<ActionResult> ChangeUserRoleToVisitor(string id)
        {
            var user = await this.storeUserService.GetUserByName(id);
            if ((user.Email != StoreConstants.ADMIN_EMAIL) && (user.Email != StoreConstants.OFFICE_STAFF_EMAIL))
            {
                await this.databaseService.ChangeUserRoleAsync(user.Id, ROLES.Visitor);
            }
            return RedirectToAction("Index");
        }

        // GET: StoreUsers/ChangeUserRoleToOfficeStaff
        [Authorize(Roles = ROLES.Admin)]
        public async Task<ActionResult> ChangeUserRoleToOfficeStaff(string id)
        {
            var user = await this.storeUserService.GetUserByName(id);
            if ((user.Email != StoreConstants.ADMIN_EMAIL) && (user.Email != StoreConstants.OFFICE_STAFF_EMAIL))
            {
                await this.databaseService.ChangeUserRoleAsync(user.Id, ROLES.OfficeStaff);
            }
            return RedirectToAction("Index");
        }

        // GET: StoreUsers/ChangeUserRoleToAdmin
        [Authorize(Roles = ROLES.Admin)]
        public async Task<ActionResult> ChangeUserRoleToAdmin(string id)
        {
            var user = await this.storeUserService.GetUserByName(id);
            if ((user.Email != StoreConstants.ADMIN_EMAIL) && (user.Email != StoreConstants.OFFICE_STAFF_EMAIL))
            {
                await this.databaseService.ChangeUserRoleAsync(user.Id, ROLES.Admin);
            }
            return RedirectToAction("Index");
        }

        // GET: StoreUsers/Configure/5
        [Authorize(Roles = ROLES.Admin)]
        public async Task<ActionResult> Configure(string id)
        {
            var user = await this.storeUserService.GetUserByName(id);
            var userModel = this.storeUserMapper.MapFrom(user);
            userModel.Role = (await this.userManager.GetRolesAsync(user)).FirstOrDefault();
            if ((userModel.Email != StoreConstants.ADMIN_EMAIL) && (userModel.Email != StoreConstants.OFFICE_STAFF_EMAIL))
            {
                userModel.IsProtected = false;
            }
            else
            {
                userModel.IsProtected = true;
            }
            return View(userModel);
        }

        // GET: StoreUsers/Delete/5
        [HttpGet]
        [Authorize(Roles = ROLES.Admin)]
        public async Task<ActionResult> Delete(string id)
        {
            var user = await this.storeUserService.GetUserByName(id);
            var userModel = this.storeUserMapper.MapFrom(user);
            userModel.Role = (await this.userManager.GetRolesAsync(user)).FirstOrDefault();
            if ((userModel.Email != StoreConstants.ADMIN_EMAIL) && (userModel.Email != StoreConstants.OFFICE_STAFF_EMAIL))
            {
                userModel.IsProtected = false;
            }
            else
            {
                userModel.IsProtected = true;
            }
            return View(userModel);
        }

        // POST: StoreUsers/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = ROLES.Admin)]
        public async Task<ActionResult> Delete(string id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
                var user = await this.storeUserService.GetUserByName(id);
                if ((user.Email != StoreConstants.ADMIN_EMAIL) && (user.Email != StoreConstants.OFFICE_STAFF_EMAIL))
                {
                    var x = await this.userManager.DeleteAsync(user);
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Roles
        [HttpGet]
        [Authorize(Roles = ROLES.Admin)]
        public async Task<IActionResult> UserRoles()
        {
            var userRoles = await this.manageService.GetAllRolesAsync();
            return View(userRoles);
        }

        // Post: Roles
        [HttpPost]
        [Authorize(Roles = ROLES.Admin)]
        public async Task<IActionResult> UserRoles(string searchString)
        {
            var userRoles = await this.manageService.GetAllRolesByFilterAsync(0, int.MaxValue, searchString);
            return View(userRoles);
        }
    }
}
