using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using StoreSystem.Data;
using StoreSystem.Data.DbContext;
using StoreSystem.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreSystem.Services
{
    public class SupplierService : ISupplierService
    {
        private readonly UserManager<StoreUser> userManager;
        private readonly IDatabaseService databaseService;
        private readonly StoreSystemDbContext context;

        public SupplierService(
            StoreSystemDbContext context,
            UserManager<StoreUser> userManager,
            IDatabaseService databaseService)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
            this.userManager = userManager;
            this.databaseService = databaseService;
        }

        public async Task<Supplier> FindSupplierByIDAsync(int id)
        {
            return await this.context.Suppliers
                .Include(s => s.Address)
                .Include(s => s.City)
                .Include(s => s.Country)
                .Include(s => s.StoreUser)
                .Include(s => s.Purchases)
                .Include(s => s.ProductsOfSupplier)
                .FirstOrDefaultAsync(s => s.SupplierID == id);
        }

        public async Task<Supplier> GetSupplierByIDAsync(int id)
        {
            return await FindSupplierByIDAsync(id) ?? throw new ArgumentException($"Can not find warehouse with ID {id}");
        }

        public async Task<Supplier> FindSupplierByNameAsync(string supplierName)
        {
            return await this.context.Suppliers
                .Include(s => s.Address)
                .Include(s => s.City)
                .Include(s => s.Country)
                .Include(s => s.StoreUser)
                .Include(s => s.Purchases)
                .Include(s => s.ProductsOfSupplier)
                .FirstOrDefaultAsync(x => x.Name == supplierName);
        }

        public async Task<Supplier> GetSupplierByNameAsync(string supplierName)
        {
            return await FindSupplierByNameAsync(supplierName) ?? throw new ArgumentException($"Can not find warehouse with name {supplierName}");
        }

        public async Task<Supplier> CreateSupplierAsync(
            string supplierName,
            string uin,
            int countryId,
            int cityId,
            int addressId,
            string userId,
            bool isDeleted = true,
            bool save = true)
        {
            if (null != (await this.context.Suppliers.FirstOrDefaultAsync(x => x.Name == supplierName)))
            {
                throw new ArgumentException($"Supplier with name {supplierName} already exists!");
            }
            var supplier = new Supplier
            {
                Name = supplierName,
                UIN = uin,
                CountryID = countryId,
                CityID = cityId,
                AddressID = addressId,
                StoreUserId = userId,
                IsDeleted = isDeleted
            };
            await this.context.Suppliers.AddAsync(supplier);
            await this.context.SaveChangesAsync();
            var roleResult = await this.databaseService.SetRoleToNewUserAsync(supplier.StoreUserId, ROLES.Supplier);

            return supplier;
        }

        public async Task<Supplier> UpdateSupplierAsync(
            int id,
            string supplierName,
            string uin,
            int countryId,
            int cityId,
            int addressId,
            string userId,
            bool isDeleted,
            bool save = true)
        {
            if (string.IsNullOrEmpty(supplierName))
            {
                throw new ArgumentNullException($"supplierName could not be null or empty!");
            }

            if (string.IsNullOrWhiteSpace(supplierName))
            {
                throw new ArgumentException($"supplierName could not be WhiteSpace!");
            }

            var supplier = await FindSupplierByIDAsync(id);
            if (supplier == null)
            {
                throw new ArgumentException($"Supplier with ID {id} does not exist!");
            }
            else
            {
                supplier.Name = supplierName;
                supplier.UIN = uin;
                supplier.CountryID = countryId;
                supplier.CityID = cityId;
                supplier.AddressID = addressId;
                supplier.StoreUserId = userId;
                supplier.IsDeleted = isDeleted;
                try
                {
                    context.Update(supplier);
                    await context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    throw;
                }
            }

            return supplier;
        }

        public async Task<bool> RemoveSupplierAsync(int id)
        {
            var supplier = await FindSupplierByIDAsync(id);
            if (null != supplier)
            {
                var identityID = supplier.StoreUserId;
                this.context.Suppliers.Remove(supplier);
                await context.SaveChangesAsync();
                await this.databaseService.ChangeUserRoleAsync(identityID, ROLES.Visitor);
            }
            else
            {
                throw new ArgumentException($"Warehouse with ID {id} does not exist!");
            }
            return true;
        }

        public async Task<bool> DeleteSupplierAsync(int id)
        {
            var supplier = await FindSupplierByIDAsync(id);
            if (null != supplier)
            {
                var identityID = supplier.StoreUserId;
                supplier.IsDeleted = true;
                supplier.StoreUserId = null;
                await UpdateSupplierAsync(
                    id,
                    supplier.Name,
                    supplier.UIN,
                    supplier.CountryID,
                    supplier.CityID,
                    supplier.AddressID,
                    supplier.StoreUserId,
                    supplier.IsDeleted);
                await context.SaveChangesAsync();
                await this.databaseService.ChangeUserRoleAsync(identityID, ROLES.Visitor);
            }
            else
            {
                throw new ArgumentException($"Warehouse with ID {id} does not exist!");
            }
            return true;
        }

        public async Task<bool> AddPurchaseToSupplierAsync(int supplierID, int purchaseID)
        {
            var supplier = await this.context.Suppliers
                .Include(s => s.Purchases)
                .Include(s => s.ProductsOfSupplier)
                .FirstOrDefaultAsync(s => s.SupplierID == supplierID) ?? throw new ArgumentException($"Can not find supplier with ID: {supplierID}.");
            if (supplier.Purchases == null)
            {
                supplier.Purchases = new List<Purchase>();
            }
            if (supplier.Purchases.Any(x => x.PurchaseID == purchaseID))
            {
                //return true;
                throw new ArgumentException($"Purchase with ID: {purchaseID} already included for supplier with ID: {supplierID}.");
            }
            var purchase = await this.context.Purchases
                .Include(p => p.ProductsТоPurchase)
                .FirstOrDefaultAsync(p => p.PurchaseID == purchaseID) ?? throw new ArgumentException($"Can not find purchase with ID: {purchaseID}.");
            supplier.Purchases.Add(purchase);
            return await this.context.SaveChangesAsync() > 0 ? true : false;
        }

        public async Task<bool> AddPurchasesToSupplierAsync(int supplierID, int[] puchasesList)
        {
            var existingPurchases = await this.context.Purchases.ToArrayAsync();
            var supplier = await this.context.Suppliers
                .Include(s => s.Purchases)
                .FirstOrDefaultAsync(s => s.SupplierID == supplierID) ?? throw new ArgumentException($"Can not find supplier with ID: {supplierID}.");
            if (supplier.Purchases == null)
            {
                supplier.Purchases = new List<Purchase>();
            }
            foreach (var purchaseId in puchasesList)
            {
                if (existingPurchases.All(x => x.PurchaseID != purchaseId))
                {
                    throw new ArgumentException($"Purchase with ID: {purchaseId} does not exist.");
                }
                if (supplier.Purchases.Any(x => x.PurchaseID == purchaseId))
                {
                    throw new ArgumentException($"Purchase with ID: {purchaseId} already included for supplier with ID: {supplierID}.");
                }
                // Add the purchases
                supplier.Purchases.Add(existingPurchases.Where(e => e.PurchaseID == purchaseId).FirstOrDefault());
            }
            return (await this.context.SaveChangesAsync()) > 0 ? true : false;
        }

        public async Task<bool> AddProductToSupplierAsync(int supplierID, int productID)
        {
            var supplier = await this.context.Suppliers
                .Include(s => s.Purchases)
                .Include(s => s.ProductsOfSupplier)
                .FirstOrDefaultAsync(x => x.SupplierID == supplierID) ?? throw new ArgumentException($"Can not find supplier with ID: {supplierID}.");
            if (supplier.ProductsOfSupplier == null)
            {
                supplier.ProductsOfSupplier = new List<ProductSupplier>();
            }
            if (supplier.ProductsOfSupplier.Any(x => x.ProductID == productID))
            {
                //return true;
                throw new ArgumentException($"Product with ID: {productID} already included for supplier with ID: {supplierID}.");
            }
            var product = (await this.context.Products.FindAsync(productID)) ?? throw new ArgumentException($"Can not find product with ID: {productID}.");
            var productSupplierToAdd = new ProductSupplier()
            {
                ProductID = productID,
                SupplierID = supplierID
            };
            supplier.ProductsOfSupplier.Add(productSupplierToAdd);
            return (await this.context.SaveChangesAsync()) > 0 ? true : false;
        }

        public async Task<List<Supplier>> GetAllSuppliersAsync()
        {
            var query = this.context.Suppliers
                .Include(s => s.City)
                .Include(s => s.Country)
                .Include(s => s.Address)
                .Include(s => s.StoreUser)
                .OrderBy(x => x.Name);
            return await query.ToListAsync();
        }

        public async Task<List<Supplier>> GetAllSuppliersByFilterAsync(int from, int to, string contains)
        {
            var allSuppliers = await GetAllSuppliersAsync();
            if (allSuppliers != null)
            {
                if (!string.IsNullOrWhiteSpace(contains))
                {
                    contains = contains.Trim().ToLower();
                    var filteredSuppliers = allSuppliers
                        .Where(x => x.Name.ToLower().Contains(contains))
                        .Skip(from)
                        .Take(to);
                    return filteredSuppliers.ToList();
                }
            }
            return allSuppliers;
        }
    }
}
