using Microsoft.EntityFrameworkCore;
using StoreSystem.Data.DbContext;
using StoreSystem.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreSystem.Services
{
    public class WarehouseService : IWarehouseService
    {
        private readonly StoreSystemDbContext context;

        public WarehouseService(StoreSystemDbContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<Warehouse> FindWarehouseByIDAsync(int id)
        {
            return await this.context.Warehouses
                .Include(w => w.Address)
                .Include(w => w.City)
                .Include(w => w.Country)
                .FirstOrDefaultAsync(x => x.WarehouseID == id);
        }

        public async Task<Warehouse> GetWarehouseByIDAsync(int id)
        {
            return await FindWarehouseByIDAsync(id) ?? throw new ArgumentException($"Can not find warehouse with ID {id}");
        }

        public async Task<Warehouse> FindWarehouseByNameAsync(string warehouseName)
        {
            return await this.context.Warehouses
                .Include(w => w.Address)
                .Include(w => w.City)
                .Include(w => w.Country)
                .FirstOrDefaultAsync(x => x.Name == warehouseName);
        }

        public async Task<Warehouse> GetWarehouseByNameAsync(string warehouseName)
        {
            return await FindWarehouseByNameAsync(warehouseName) ?? throw new ArgumentException($"Can not find warehouse with name {warehouseName}");
        }

        public async Task<Warehouse> CreateWarehouseAsync(
            string warehouseName,
            int countryID,
            int cityID,
            int addressID,
            bool save = true)
        {
            if (string.IsNullOrEmpty(warehouseName))
            {
                throw new ArgumentNullException($"WarehouseName could not be null or empty!");
            }

            if (string.IsNullOrWhiteSpace(warehouseName))
            {
                throw new ArgumentException($"WarehouseName could not be WhiteSpace!");
            }

            var warehouse = await this.context.Warehouses.FirstOrDefaultAsync(x => x.Name == warehouseName);
            if (warehouse == null)
            {
                if (null == await this.context.Countries.FirstOrDefaultAsync(x => x.CountryID == countryID))
                {
                    throw new ArgumentException($"Country with ID {countryID} does not exist!");
                }
                if (null == await this.context.Cities.FirstOrDefaultAsync(x => x.CityID == cityID))
                {
                    throw new ArgumentException($"City with ID {countryID} does not exist!");
                }
                if (null == await this.context.Addresses.FirstOrDefaultAsync(x => x.AddressID == addressID))
                {
                    throw new ArgumentException($"Address with ID {countryID} does not exist!");
                }
                warehouse = new Warehouse {
                    CountryID = countryID,
                    Name = warehouseName,
                    CityID = cityID,
                    AddressID = addressID
                };
                await this.context.Warehouses.AddAsync(warehouse);
                await this.context.SaveChangesAsync();
            }
            else
            {
                throw new ArgumentException($"Warehouse with name {warehouseName} already exists!");
            }
            return warehouse;
        }

        public async Task<Warehouse> UpdateWarehouseAsync(
            int id,
            string warehouseName,
            int countryID,
            int cityID,
            int addressID,
            bool save = true)
        {
            if (string.IsNullOrEmpty(warehouseName))
            {
                throw new ArgumentNullException($"WarehouseName could not be null or empty!");
            }

            if (string.IsNullOrWhiteSpace(warehouseName))
            {
                throw new ArgumentException($"WarehouseName could not be WhiteSpace!");
            }

            var warehouse = await FindWarehouseByIDAsync(id);
            if (warehouse == null)
            {
                throw new ArgumentException($"Warehouse with ID {id} does not exist!");
            }
            else
            {
                warehouse.Name = warehouseName;
                warehouse.CountryID = countryID;
                warehouse.CityID = cityID;
                warehouse.AddressID = addressID;
                try
                {
                    context.Update(warehouse);
                    await context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    throw;
                }
            }
            return warehouse;
        }

        public async Task<bool> DeleteWarehouseAsync(int id)
        {
            var warehouse = await FindWarehouseByIDAsync(id);
            if (null != warehouse)
            {
                this.context.Warehouses.Remove(warehouse);
                await context.SaveChangesAsync();
            }
            else
            {
                throw new ArgumentException($"Warehouse with ID {id} does not exist!");
            }
            return true;
        }

        public async Task<bool> AddPurchaseToWarehouse(int warehouseID, int purchaseID)
        {
            var warehouse = await this.context.Warehouses
                .Include(p => p.Purchases)
                .Where(w => w.WarehouseID == warehouseID)
                .FirstOrDefaultAsync() ?? throw new ArgumentException($"Can not find warehouse with ID: {warehouseID}.");
            if (warehouse.Purchases.Any(x => x.PurchaseID == purchaseID))
            {
                throw new ArgumentException($"Purchase with ID: {purchaseID} already included for warehouse with ID: {warehouseID}.");
            }
            var purchase = await this.context.Purchases.FindAsync(purchaseID) ?? throw new ArgumentException($"Can not find purchase with ID: {purchaseID}.");
            warehouse.Purchases.Add(purchase);
            return await this.context.SaveChangesAsync() > 0 ? true : false;
        }

        public async Task<List<Warehouse>> GetAllWarehousesAsync()
        {
            var query = this.context.Warehouses
                .Include(w => w.Address)
                .Include(w => w.City)
                .Include(w => w.Country)
                .OrderBy(x => x.Name);
            return await query.ToListAsync();
        }

        public async Task<List<Warehouse>> GetAllWarehousesByFilterAsync(int from, int to, string contains)
        {
            var allWarehouses = await GetAllWarehousesAsync();
            if (allWarehouses != null)
            {
                if (!string.IsNullOrWhiteSpace(contains))
                {
                    contains = contains.Trim().ToLower();
                    var filteredWarehouses = allWarehouses
                        .Where(x => x.Name.ToLower().Contains(contains))
                        .Skip(from)
                        .Take(to);
                    return filteredWarehouses.ToList();
                }
            }
            return allWarehouses;
        }
    }
}
