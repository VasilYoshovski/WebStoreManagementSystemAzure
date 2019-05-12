using Microsoft.EntityFrameworkCore;
using StoreSystem.Data.DbContext;
using StoreSystem.Data.Models;
using StoreSystem.Services.Dto;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreSystem.Services
{
    public class ProductService : IProductService
    {
        private readonly StoreSystemDbContext context;

        public ProductService(StoreSystemDbContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<Product> CreateProductAsync(string name, string measure, decimal quantity, decimal buyPrice, decimal retailPrice, bool save = true)
        {
            if (await this.context.Products.Where(x => x.IsDeleted == false).AnyAsync(x => x.Name == name))
            {
                throw new ArgumentException("Product exists");
            }
            var newProduct = new Product()
            {
                Name = name,
                Measure = measure,
                Quantity = quantity,
                BuyPrice = buyPrice,
                RetailPrice = retailPrice
            };

            this.context.Products.Add(newProduct);

            if (save) { await this.context.SaveChangesAsync(); }

            return newProduct;
        }

        public Task<Product> FindProductByNameAsync(string productName)
        {
            var product = this.context.Products.FirstOrDefaultAsync(p => p.Name == productName);
            return product;
        }//

        public Task<bool> ProductExistsAsync(int id)
        {
            return context.Products.Where(x => x.IsDeleted == false).AnyAsync(x => x.ProductID == id);
        }

        public Task<Product> FindProductByIdAsync(int productId)
        {
            return this.context.Products.FindAsync(productId);
        }

        public Task<Product> GetProductByNameAsync(string productName)
        {
            var product = this.context.Products.FirstOrDefaultAsync(p => p.Name == productName)
                ?? throw new ArgumentException("Product can not be found");
            return product;
        }//


        private IQueryable<Product> GetAllProductQuery(int from, int to, string contains = "*", string sortOrder = "Name", bool haveQuantity = false)
        {

            var query = this.context.Products.Where(x => x.IsDeleted == false);

            if (contains != "*" && !string.IsNullOrEmpty(contains))
            {
                query = query.Where(x => x.Name.Contains(contains));
            }

            if (haveQuantity)
            {
                query = query.Where(x => x.Quantity > 0);
            }

            //=====sorting====
            //sortOder must match property name (case sens.)

            if (string.IsNullOrEmpty(sortOrder))
            {
                sortOrder = "Name";
            }

            bool descending = false;
            if (sortOrder.EndsWith("_desc"))
            {
                sortOrder = sortOrder.Substring(0, sortOrder.Length - 5);
                descending = true;
            }

            if (descending)
            {
                query = query.OrderByDescending(e => EF.Property<object>(e, sortOrder));
            }
            else
            {
                query = query.OrderBy(e => EF.Property<object>(e, sortOrder));
            }

            //======end of sorting========

            query = query.Skip(from).Take(to);

            return query;
        }

        public async Task<IReadOnlyCollection<ProductIdNameDto>> GetAllProductsIdName(int from, int to, string contains = "*", string sortOrder = "name", bool haveQuantity = false)
        {
            var query = GetAllProductQuery(from, to, contains, sortOrder, haveQuantity);
            var res = query.Select(x => new ProductIdNameDto()
            {
                ProductId = x.ProductID,
                Name = x.Name
            });

            return await res.ToListAsync();
        }

        public async Task<IReadOnlyCollection<Product>> GetAllProductsAsync(int from, int to, string contains = "*", string sortOrder = "name", bool haveQuantity = false)
        {
            var query = GetAllProductQuery(from, to, contains, sortOrder, haveQuantity);
            return await query.ToListAsync();
        }

        public Task<int> GetProductsCountAsync(int from, int to, string contains = "*", string sortOrder = "name", bool haveQuantity = false)
        {
            var query = GetAllProductQuery(from, to, contains, sortOrder, haveQuantity);
            return query.CountAsync();
        }

        public async Task<bool> UpdateProductDetailAsync
            (int productID, string name, string measure, decimal? quantity, decimal? buyPrice, decimal? retailPrice)
        {
            var product = await this.context.Products.FindAsync(productID);

            if (name != null)
            {
                var fProd = await this.context.Products.FirstOrDefaultAsync(x => x.Name == name);

                if (fProd != null && fProd.ProductID != productID)
                {
                    throw new ArgumentException($"Product with name \"{name}\" exists");
                }
                product.Name = name;
            }

            if (measure != null) product.Measure = measure;
            if (quantity != null) product.Quantity = (decimal) quantity;
            if (buyPrice != null) product.BuyPrice = (decimal) buyPrice;
            if (retailPrice != null) product.RetailPrice = (decimal) retailPrice;

            var tChanges = await this.context.SaveChangesAsync();

            return (tChanges > 0) ? true : false;
        }

        public async Task<bool> DeleteProduct(int productId)
        {
            var product = await this.context.Products.FindAsync(productId);
            product.IsDeleted = true;
            return (await this.context.SaveChangesAsync()) > 0 ? true : false;
        }


    }
}
