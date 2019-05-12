using System.Collections.Generic;
using System.Threading.Tasks;
using StoreSystem.Data.Models;

namespace StoreSystem.Services
{
    public interface ISupplierService
    {
        Task<bool> AddProductToSupplierAsync(int supplierID, int productID);
        Task<bool> AddPurchaseToSupplierAsync(int supplierID, int purchaseID);
        Task<Supplier> CreateSupplierAsync(string supplierName, string uin, int countryId, int cityId, int addressId, string userId, bool isDeleted = false, bool save = true);
        Task<Supplier> UpdateSupplierAsync(int supplierID, string supplierName, string uin, int countryId, int cityId, int addressId, string userId, bool isDeleted, bool save = true);
        Task<bool> DeleteSupplierAsync(int id);
        Task<bool> RemoveSupplierAsync(int id);
        Task<Supplier> FindSupplierByIDAsync(int id);
        Task<Supplier> GetSupplierByIDAsync(int id);
        Task<Supplier> FindSupplierByNameAsync(string supplierName);
        Task<Supplier> GetSupplierByNameAsync(string supplierName);
        Task<bool> AddPurchasesToSupplierAsync(int supplierID, int[] puchasesList);
        Task<List<Supplier>> GetAllSuppliersAsync();
        Task<List<Supplier>> GetAllSuppliersByFilterAsync(int from, int to, string contains);
    }
}
