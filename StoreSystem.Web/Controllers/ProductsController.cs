using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StoreSystem.Data;
using StoreSystem.Data.DbContext;
using StoreSystem.Data.Models;
using StoreSystem.Services;
using StoreSystem.Web.Mappers;
using StoreSystem.Web.Models.ProductViewModels;
using StoreSystem.Web.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreSystem.Web.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductService productService;
        private readonly IViewModelMapper<Product, ProductViewModel> productMapper;
        private readonly IViewModelMapper<IReadOnlyCollection<Product>, ProductsCollectionViewModel> productCollectionMapper;

        public ProductsController(IProductService productService,
            IViewModelMapper<Product, ProductViewModel> productMapper,
            IViewModelMapper<IReadOnlyCollection<Product>, ProductsCollectionViewModel> productCollectionMapper)
        {
            this.productService = productService ?? throw new ArgumentNullException(nameof(productService));
            this.productMapper = productMapper ?? throw new ArgumentNullException(nameof(productMapper));
            this.productCollectionMapper = productCollectionMapper ?? throw new ArgumentNullException(nameof(productCollectionMapper));
        }


        private bool CanEdit()
        {
            var canEdit = false;

            if (this.User.IsInRole(ROLES.Admin) || this.User.IsInRole(ROLES.OfficeStaff))
            {
                canEdit = true;
            }
            else
            {
                canEdit = false;
            }
            return canEdit;
        }

        // GET: Products
        public async Task<IActionResult> Index(
            string sortOrder,
            string currentFilter,
            string searchString,
            int? pageNumber)
        {
            this.ViewData["Title"] = "List of products";
            this.ViewData["NameSortParm"] = string.IsNullOrEmpty(sortOrder) ? "Name_desc" : "Name";
            this.ViewData["QuantitySortParm"] = sortOrder == "Quantity" ? "Quantity_desc" : "Quantity";
            this.ViewData["BuypriceSortParm"] = sortOrder == "BuyPrice" ? "BuyPrice_desc" : "BuyPrice";
            this.ViewData["RetailpriceSortParm"] = sortOrder == "RetailPrice" ? "RetailPrice_desc" : "RetailPrice";
            this.ViewData["CurrentSort"] = sortOrder;

            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            this.ViewData["CurrentFilter"] = searchString;

            var pageIndex = pageNumber ?? 1;
            var pageSize = 3;
            
            var products = await this.productService.GetAllProductsAsync((pageIndex - 1) * pageSize, pageSize, searchString, sortOrder);
            var productsCount = await this.productService.GetProductsCountAsync(0, int.MaxValue, searchString, sortOrder);

            var productViewModelList = products.Select(this.productMapper.MapFrom);
            var result = PaginatedList<ProductViewModel>.Create(productViewModelList, productsCount, pageIndex, pageSize, CanEdit());
            //return View(await PaginatedList<Student>.CreateAsync(students.AsNoTracking(), pageNumber ?? 1, pageSize));

            //var items = await source.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();

            return this.View(result);
        }

        // GET: Products list JSON for AJAX
        public async Task<IActionResult> ProductList(string term)
        {
            return this.Json(await this.productService.GetAllProductsIdName(0, 10,term));
        }

        public async Task<IActionResult> ProductData(int id)
        {
            var product = await this.productService.FindProductByIdAsync(id);
            return this.Json(product);
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var product = await this.productService.FindProductByIdAsync((int)id);
            if (product == null)
            {
                return this.NotFound();
            }

            var res = this.productMapper.MapFrom(product);
            res.CanEdit = CanEdit();

            return this.View(res);
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            return this.View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductViewModel productViewModel)
        {
            if (this.ModelState.IsValid)
            {
                await this.productService.CreateProductAsync(
                     productViewModel.Name,
                     productViewModel.Measure,
                     productViewModel.Quantity,
                     productViewModel.BuyPrice,
                     productViewModel.RetailPrice);

                return this.RedirectToAction(nameof(Index));
            }
            return this.View(productViewModel);
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var product = await this.productService.FindProductByIdAsync((int)id);
            if (product == null)
            {
                return this.NotFound();
            }
            return this.View(this.productMapper.MapFrom(product));
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ProductViewModel productViewModel)
        {
            if (id != productViewModel.ProductID)
            {
                return this.NotFound();
            }

            if (this.ModelState.IsValid)
            {
                try
                {
                    await this.productService.UpdateProductDetailAsync(
                         productViewModel.ProductID,
                         productViewModel.Name,
                         productViewModel.Measure,
                         productViewModel.Quantity,
                         productViewModel.BuyPrice,
                         productViewModel.RetailPrice);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await this.productService.ProductExistsAsync(productViewModel.ProductID))
                    {
                        return this.NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return this.RedirectToAction(nameof(Index));
            }
            return this.View(productViewModel);
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var product = await this.productService.FindProductByIdAsync((int)id);
            if (product == null)
            {
                return this.NotFound();
            }

            return this.View(this.productMapper.MapFrom(product));
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            productService.DeleteProduct(id);
            return this.RedirectToAction(nameof(Index));
        }
    }
}
