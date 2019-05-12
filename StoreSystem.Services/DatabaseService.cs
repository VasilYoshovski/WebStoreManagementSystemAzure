using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using StoreSystem.Data.DbContext;
using StoreSystem.Data.Models;
using StoreSystem.Services.DatabaseServices;
using StoreSystem.Services.DatabaseServices.Contracts;
using StoreSystem.Services.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreSystem.Services
{
    public class DatabaseService : IDatabaseService
    {
        private readonly UserManager<StoreUser> userManager;
        private readonly IDatabaseJSONConnectivity<string> jSONConnectivityService;
        private readonly StoreSystemDbContext context;
        private readonly IDateTimeNowProvider dateTimeProvider;

        public DatabaseService(
            StoreSystemDbContext context,
            IDateTimeNowProvider dateTimeProvider,
            UserManager<StoreUser> userManager,
            IDatabaseJSONConnectivity<string> JSONConnectivityService)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
            this.dateTimeProvider = dateTimeProvider;
            this.userManager = userManager;
            jSONConnectivityService = JSONConnectivityService;
        }

        public void DatabaseAutoPopulate()
        {
            // Do nothing for now

            //TODO if needed
            //(new DatabaseAutoCreator(this.context, this.dateTimeProvider, this.userManager, this.jSONConnectivityService))
            //    .PopulateDatabase();
        }

        public List<Address> GetAddresses()
        {
            return this.context.Addresses.ToList();
        }

        public List<City> GetCities()
        {
            return this.context.Cities.ToList();
        }

        public List<Client> GetClients()
        {
            return this.context.Clients.ToList();
        }

        public List<Country> GetCountries()
        {
            return this.context.Countries.ToList();
        }

        public List<Offer> GetOffers()
        {
            return this.context.Offers.ToList();
        }

        public List<Product> GetProducts()
        {
            return this.context.Products.ToList();
        }

        public List<ProductOffer> GetProductOffer()
        {
            return this.context.ProductOffers.ToList();
        }

        public List<ProductPurchase> GetProductPurchase()
        {
            return this.context.ProductPurchase.ToList();
        }

        public List<ProductSale> GetProductSale()
        {
            return this.context.ProductSales.ToList();
        }

        public List<ProductSupplier> GetProductSupplier()
        {
            return this.context.productSupplier.ToList();
        }

        public List<Purchase> GetPurchases()
        {
            return this.context.Purchases.ToList();
        }

        public List<Sale> GetSales()
        {
            return this.context.Sales.ToList();
        }

        public List<Supplier> GetSupplies()
        {
            return this.context.Suppliers.ToList();
        }

        public List<Warehouse> GetWarehouses()
        {
            return this.context.Warehouses.ToList();
        }

        public void SaveChanges()
        {
            this.context.SaveChanges();
        }

        public void SetAddresses(List<Address> list)
        {
            this.context.Addresses.AddRange(list);
            //using (var transaction = this.context.Database.BeginTransaction())
            //{
            //    this.context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT StoreSystem.Addresses ON;");
            //    this.context.Addresses.AddRange(list);
            //    this.context.SaveChanges();
            //    this.context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT StoreSystem.Addresses OFF");
            //    transaction.Commit();
            //}
        }

        public void SetCities(List<City> list)
        {
            this.context.Cities.AddRange(list);
            //using (var transaction = this.context.Database.BeginTransaction())
            //{
            //    this.context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT StoreSystem.Cities ON;");
            //    this.context.Cities.AddRange(list);
            //    this.context.SaveChanges();
            //    this.context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT StoreSystem.Cities OFF");
            //    transaction.Commit();
            //}
        }

        public void SetCountries(List<Country> list)
        {
            this.context.Countries.AddRange(list);
            //using (var transaction = this.context.Database.BeginTransaction())
            //{
            //    this.context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT StoreSystem.Countries ON;");
            //    this.context.Countries.AddRange(list);
            //    this.context.SaveChanges();
            //    this.context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT StoreSystem.Countries OFF");
            //    transaction.Commit();
            //}
        }

        public void SetWarehouses(List<Warehouse> list)
        {
            this.context.Warehouses.AddRange(list);
            //using (var transaction = this.context.Database.BeginTransaction())
            //{
            //    this.context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT StoreSystem.Warehouses ON;");
            //    this.context.Warehouses.AddRange(list);
            //    this.context.SaveChanges();
            //    this.context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT StoreSystem.Warehouses OFF");
            //    transaction.Commit();
            //}
        }

        public void SetProducts(List<Product> list)
        {
            this.context.Products.AddRange(list);
            //using (var transaction = this.context.Database.BeginTransaction())
            //{
            //    this.context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT StoreSystem.Products ON;");
            //    this.context.Products.AddRange(list);
            //    this.context.SaveChanges();
            //    this.context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT StoreSystem.Products OFF");
            //    transaction.Commit();
            //}
        }

        public void SetClients(List<Client> list)
        {
            this.context.Clients.AddRange(list);
            //using (var transaction = this.context.Database.BeginTransaction())
            //{
            //    this.context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT StoreSystem.Clients ON;");
            //    this.context.Clients.AddRange(list);
            //    this.context.SaveChanges();
            //    this.context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT StoreSystem.Clients OFF");
            //    transaction.Commit();
            //}
        }

        public void SetSupplies(List<Supplier> list)
        {
            this.context.Suppliers.AddRange(list);
            //using (var transaction = this.context.Database.BeginTransaction())
            //{
            //    this.context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT StoreSystem.Suppliers ON;");
            //    this.context.Suppliers.AddRange(list);
            //    this.context.SaveChanges();
            //    this.context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT StoreSystem.Suppliers OFF");
            //    transaction.Commit();
            //}
        }

        public void SetOffers(List<Offer> list)
        {
            this.context.Offers.AddRange(list);
            //using (var transaction = this.context.Database.BeginTransaction())
            //{
            //    this.context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT StoreSystem.Offers ON;");
            //    this.context.Offers.AddRange(list);
            //    this.context.SaveChanges();
            //    this.context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT StoreSystem.Offers OFF");
            //    transaction.Commit();
            //}
        }

        public void SetPurchases(List<Purchase> list)
        {
            this.context.Purchases.AddRange(list);
            //using (var transaction = this.context.Database.BeginTransaction())
            //{
            //    this.context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT StoreSystem.Purchases ON;");
            //    this.context.Purchases.AddRange(list);
            //    this.context.SaveChanges();
            //    this.context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT StoreSystem.Purchases OFF");
            //    transaction.Commit();
            //}
        }

        public void SetSales(List<Sale> list)
        {
            this.context.Sales.AddRange(list);
            //using (var transaction = this.context.Database.BeginTransaction())
            //{
            //    this.context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT StoreSystem.Sales ON;");
            //    this.context.Sales.AddRange(list);
            //    this.context.SaveChanges();
            //    this.context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT StoreSystem.Sales OFF");
            //    transaction.Commit();
            //}
        }

        public void SetProductOffer(List<ProductOffer> list)
        {
            this.context.ProductOffers.AddRange(list);
            //using (var transaction = this.context.Database.BeginTransaction())
            //{
            //    this.context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT StoreSystem.ProductOffers ON;");
            //    this.context.ProductOffers.AddRange(list);
            //    this.context.SaveChanges();
            //    this.context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT StoreSystem.ProductOffers OFF");
            //    transaction.Commit();
            //}
        }

        public void SetProductPurchase(List<ProductPurchase> list)
        {
            this.context.ProductPurchase.AddRange(list);
            //using (var transaction = this.context.Database.BeginTransaction())
            //{
            //    this.context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT StoreSystem.ProductPurchase ON;");
            //    this.context.ProductPurchase.AddRange(list);
            //    this.context.SaveChanges();
            //    this.context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT StoreSystem.ProductPurchase OFF");
            //    transaction.Commit();
            //}
        }

        public void SetProductSale(List<ProductSale> list)
        {
            this.context.ProductSales.AddRange(list);
            //using (var transaction = this.context.Database.BeginTransaction())
            //{
            //    this.context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT StoreSystem.ProductSales ON;");
            //    this.context.ProductSales.AddRange(list);
            //    this.context.SaveChanges();
            //    this.context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT StoreSystem.ProductSales OFF");
            //    transaction.Commit();
            //}
        }

        public void SetProductSupplier(List<ProductSupplier> list)
        {
            this.context.productSupplier.AddRange(list);
            //using (var transaction = this.context.Database.BeginTransaction())
            //{
            //    this.context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT StoreSystem.productSupplier ON;");
            //    this.context.productSupplier.AddRange(list);
            //    this.context.SaveChanges();
            //    this.context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT StoreSystem.productSupplier OFF");
            //    transaction.Commit();
            //}
        }

        public async Task<List<StoreUser>> GetUsersByRoleAsync(string role)
        {
            var roleResult = await this.context.Roles.Where(r => r.Name == role).FirstOrDefaultAsync();
            if (roleResult == null)
            {
                return new List<StoreUser>();
            }
            else
            {
                var UsersRolesList = await this.context.UserRoles.Where(r => r.RoleId == roleResult.Id).ToListAsync();
                var visitorUsersList = await this.context.StoreUsers
                    .Where(x => UsersRolesList.Any(f => f.UserId == x.Id)).ToListAsync();
                return visitorUsersList;
            }
        }

        public async Task<List<(string userName, string userID)>> GetUsersNameAndIDByRoleAsync(string role)
        {
            var visitorUsersList = (await GetUsersByRoleAsync(role))
                .Select(m => (m.UserName, m.Id.ToString()));
            return visitorUsersList.ToList();
        }

        public async Task<bool> ChangeUserRoleAsync(string userId, string roleName)
        {
            var user = await this.userManager.FindByIdAsync(userId);
            if (user != null)
            {
                var userRoles = await this.userManager.GetRolesAsync(user);
                if (userRoles.Any(r => r == roleName))
                {
                    //throw new ArgumentException($"User with ID {userId} already has {roleName} role");
                }

                if (this.userManager.AddToRoleAsync(user, roleName).Result.Succeeded)
                {
                    foreach (var roleToRemove in userRoles)
                    {
                        if (!this.userManager.RemoveFromRoleAsync(user, roleToRemove).Result.Succeeded)
                        {
                            throw new ArgumentException($"{roleToRemove} role could not be deleted from the database for user with ID {userId}, but should be, because his role has already been set to {roleName}");
                        }
                    }
                }
                else
                {
                    //throw new ArgumentException($"{roleName} role not set to the user with ID {userId}");
                    return false;
                }
            }
            else
            {
                throw new ArgumentException($"User with ID {userId} not found");
            }

            return true;
        }

        public async Task<bool> SetRoleToNewUserAsync(string userId, string roleName)
        {
            var result = await ChangeUserRoleAsync(userId, roleName);
            if (result == false)
            {
                var user = await this.userManager.FindByIdAsync(userId);
                if (this.userManager.DeleteAsync(user).Result.Succeeded)
                {
                    throw new ArgumentException($"User with ID {userId} could not be deleted from the database, but should be, because his role could not be set to {roleName}");
                }
                throw new ArgumentException($"{roleName} role not set to the user with ID {userId} and the user is deleted.");
            }
            return true;
        }
    }
}
