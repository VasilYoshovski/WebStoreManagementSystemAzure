using StoreSystem.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StoreSystem.Services
{
    public interface IDatabaseService
    {
        void DatabaseAutoPopulate();
        List<Address> GetAddresses();
        List<City> GetCities();
        List<Client> GetClients();
        List<Country> GetCountries();
        List<Offer> GetOffers();
        List<ProductOffer> GetProductOffer();
        List<ProductPurchase> GetProductPurchase();
        List<Product> GetProducts();
        List<ProductSale> GetProductSale();
        List<ProductSupplier> GetProductSupplier();
        List<Purchase> GetPurchases();
        List<Sale> GetSales();
        List<Supplier> GetSupplies();
        List<Warehouse> GetWarehouses();
        void SaveChanges();
        void SetAddresses(List<Address> list);
        void SetCities(List<City> list);
        void SetClients(List<Client> list);
        void SetCountries(List<Country> list);
        void SetOffers(List<Offer> list);
        void SetProductOffer(List<ProductOffer> list);
        void SetProductPurchase(List<ProductPurchase> list);
        void SetProducts(List<Product> list);
        void SetProductSale(List<ProductSale> list);
        void SetProductSupplier(List<ProductSupplier> list);
        void SetPurchases(List<Purchase> list);
        void SetSales(List<Sale> list);
        void SetSupplies(List<Supplier> list);
        void SetWarehouses(List<Warehouse> list);
        Task<List<StoreUser>> GetUsersByRoleAsync(string role);
        Task<List<(string userName, string userID)>> GetUsersNameAndIDByRoleAsync(string role);
        Task<bool> ChangeUserRoleAsync(string userId, string roleName);
        Task<bool> SetRoleToNewUserAsync(string userId, string roleName);
    }
}
