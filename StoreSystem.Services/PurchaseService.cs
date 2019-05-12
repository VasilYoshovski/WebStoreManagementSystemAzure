using Microsoft.EntityFrameworkCore;
using StoreSystem.Data.DbContext;
using StoreSystem.Data.Models;
using StoreSystem.Services.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreSystem.Services
{
    public class PurchaseService : IPurchaseService
    {
        private readonly StoreSystemDbContext context;
        private readonly IDateTimeNowProvider dateNow;

        public PurchaseService(StoreSystemDbContext context, IDateTimeNowProvider dateNow)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
            this.dateNow = dateNow ?? throw new ArgumentNullException(nameof(dateNow));
        }

        public async Task<Purchase> CreatePurchaseAsync(
            int supplierId,
            int warehouseId,
            DateTime purchaseDate,
            DateTime deliveryDate,
            DateTime deadlineDate,
            bool save = true)
        {
            var newPurchase = new Purchase()
            {
                SupplierID = supplierId,
                WarehouseID = warehouseId,
                PurchaseDate = purchaseDate,
                DeliveryDate = deliveryDate,
                DeadlineDate = deadlineDate
            };
            await this.context.Purchases.AddAsync(newPurchase);
            await context.SaveChangesAsync();
            return newPurchase;
        }

        public async Task<Purchase> FindPurchaseByIDAsync(int id)
        {
            return await this.context.Purchases
                .Include(p => p.Supplier)
                .Include(p => p.ProductsТоPurchase)
                .Include(p => p.Warehouse)
                .FirstOrDefaultAsync(p => p.PurchaseID == id);
        }

        public async Task<Purchase> GetPurchaseByIDAsync(int id)
        {
            return (await FindPurchaseByIDAsync(id)) ?? throw new ArgumentException($"Can not find purchase with ID {id}");
        }

        public async Task<List<Purchase>> GetAllPurchasesAsync()
        {
            return await this.context.Purchases
                .Include(p => p.Supplier)
                .Include(p => p.ProductsТоPurchase)
                .Include(p => p.Warehouse).ToListAsync();
        }

        public async Task<bool> AddProductsToPurchaseAsync(int PurchaseID, params KeyValuePair<string, decimal>[] productsListWithQuantity)
        {
            var purchase = (await this.context.Purchases.FindAsync(PurchaseID)) ?? throw new ArgumentException($"Can not find purchase with {PurchaseID}.");
            Product product;
            purchase.ProductsТоPurchase = productsListWithQuantity.Select(pwq =>
            {
                product = this.context.Products.FirstOrDefaultAsync(x => x.Name == pwq.Key).Result ?? throw new ArgumentException($"Can not find product {pwq.Key}");
                //if (product.Quantity < pwq.Value)
                //{
                //    throw new ArgumentException($"There is not enought quantity of {pwq.Key}");
                //}
                product.Quantity += pwq.Value; //was product.Quantity -= pwq.Value;
                return new ProductPurchase() { Product = product,ProductQty = pwq.Value };

            }).ToList();
            return (await this.context.SaveChangesAsync()) > 0 ? true : false;
        }

        public async Task<Purchase> UpdatePurchaseAsync(
            int purchaseId,
            int supplierId,
            int warehouseId,
            DateTime purchaseDate,
            DateTime deliveryDate,
            DateTime deadlineDate,
            bool save = true)
        {
            var purchase = await FindPurchaseByIDAsync(purchaseId);
            if (purchase == null)
            {
                throw new ArgumentException($"Purchase with ID {purchaseId} does not exist!");
            }
            else
            {
                purchase.SupplierID = supplierId;
                purchase.WarehouseID = warehouseId;
                purchase.PurchaseDate = purchaseDate;
                purchase.DeliveryDate = deliveryDate;
                purchase.DeadlineDate = deadlineDate;
                try
                {
                    context.Update(purchase);
                    await context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    throw;
                }
            }
            return purchase;
        }

        public async Task<bool> DeletePurchaseAsync(int id)
        {
            var purchase = await FindPurchaseByIDAsync(id);
            if (null != purchase)
            {
                this.context.Purchases.Remove(purchase);
                await context.SaveChangesAsync();
            }
            else
            {
                throw new ArgumentException($"Purchase with ID {id} does not exist!");
            }
            return true;
        }
    }
}
