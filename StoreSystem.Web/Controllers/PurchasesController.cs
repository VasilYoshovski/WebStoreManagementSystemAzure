using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StoreSystem.Data;
using StoreSystem.Data.Models;
using StoreSystem.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreSystem.Web.Controllers
{
    public class PurchasesController : Controller
    {
        private readonly IPurchaseService purchaseService;
        private readonly ISupplierService supplierService;
        private readonly IWarehouseService warehouseService;

        public PurchasesController(
            IPurchaseService purchaseService,
            ISupplierService supplierService,
            IWarehouseService warehouseService)
        {
            this.purchaseService = purchaseService;
            this.supplierService = supplierService;
            this.warehouseService = warehouseService;
        }

        // GET: Purchases
        [Authorize(Roles = ROLES.AdminAndOfficeStaff)]
        public async Task<IActionResult> Index()
        {
            this.ViewData["Title"] = "List of purchases";
            var purchases = await purchaseService.GetAllPurchasesAsync();
            return View(purchases);
        }

        // GET: Purchases/Details/5
        [Authorize(Roles = ROLES.AdminAndOfficeStaff)]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var purchase = await this.purchaseService.FindPurchaseByIDAsync(id ?? -1);
            if (purchase == null)
            {
                return NotFound();
            }

            return View(purchase);
        }

        // GET: Purchases/Create
        [Authorize(Roles = ROLES.AdminAndOfficeStaff)]
        public async Task<IActionResult> Create()
        {
            ViewData["SupplierID"] = new SelectList(await this.supplierService.GetAllSuppliersAsync(), "SupplierID", "Name");
            ViewData["WarehouseID"] = new SelectList(await this.warehouseService.GetAllWarehousesAsync(), "WarehouseID", "Name");
            return View();
        }

        // POST: Purchases/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = ROLES.AdminAndOfficeStaff)]
        public async Task<IActionResult> Create([Bind("PurchaseID,PurchaseDate,DeadlineDate,DeliveryDate,SupplierID,WarehouseID")] Purchase purchase)
        {
            if (ModelState.IsValid)
            {
                await this.purchaseService.CreatePurchaseAsync(
                    purchase.SupplierID,
                    purchase.WarehouseID,
                    purchase.PurchaseDate,
                    purchase.DeliveryDate,
                    purchase.DeadlineDate);
                //_context.Add(purchase);
                //await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["SupplierID"] = new SelectList(await this.supplierService.GetAllSuppliersAsync(), "SupplierID", "Name", purchase.SupplierID);
            ViewData["WarehouseID"] = new SelectList(await this.warehouseService.GetAllWarehousesAsync(), "WarehouseID", "Name", purchase.WarehouseID);
            return View(purchase);
        }

        // GET: Purchases/Edit/5
        [Authorize(Roles = ROLES.AdminAndOfficeStaff)]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var purchase = await this.purchaseService.FindPurchaseByIDAsync(id ?? -1);
            if (purchase == null)
            {
                return NotFound();
            }
            ViewData["SupplierID"] = new SelectList(await this.supplierService.GetAllSuppliersAsync(), "SupplierID", "Name", purchase.SupplierID);
            ViewData["WarehouseID"] = new SelectList(await this.warehouseService.GetAllWarehousesAsync(), "WarehouseID", "Name", purchase.WarehouseID);
            return View(purchase);
        }

        // POST: Purchases/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = ROLES.AdminAndOfficeStaff)]
        public async Task<IActionResult> Edit(int id, [Bind("PurchaseID,PurchaseDate,DeadlineDate,DeliveryDate,SupplierID,WarehouseID")] Purchase purchase)
        {
            if (id != purchase.PurchaseID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await this.purchaseService.UpdatePurchaseAsync(
                    purchase.PurchaseID,
                    purchase.SupplierID,
                    purchase.WarehouseID,
                    purchase.PurchaseDate,
                    purchase.DeliveryDate,
                    purchase.DeadlineDate);
                return RedirectToAction(nameof(Index));
            }
            ViewData["SupplierID"] = new SelectList(await this.supplierService.GetAllSuppliersAsync(), "SupplierID", "Name", purchase.SupplierID);
            ViewData["WarehouseID"] = new SelectList(await this.warehouseService.GetAllWarehousesAsync(), "WarehouseID", "Name", purchase.WarehouseID);
            return View(purchase);
        }

        // GET: Purchases/Delete/5
        [Authorize(Roles = ROLES.Admin)]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var purchase = await this.purchaseService.GetPurchaseByIDAsync(id ?? -1);
            if (purchase == null)
            {
                return NotFound();
            }

            return View(purchase);
        }

        // POST: Purchases/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = ROLES.Admin)]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await this.purchaseService.DeletePurchaseAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
