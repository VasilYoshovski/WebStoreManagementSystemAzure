using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using StoreSystem.Data.Models;

namespace StoreSystem.Services
{
    public interface IPurchaseService
    {
        Task<bool> AddProductsToPurchaseAsync(int PurchaseID, params KeyValuePair<string, decimal>[] productsListWithQuantity);
        Task<Purchase> CreatePurchaseAsync(int supplierId, int warehouseId, DateTime purchaseDate, DateTime deliveryDate, DateTime deadlineDate, bool save = true);
        Task<Purchase> FindPurchaseByIDAsync(int id);
        Task<Purchase> GetPurchaseByIDAsync(int id);
        Task<List<Purchase>> GetAllPurchasesAsync();
        Task<bool> DeletePurchaseAsync(int id);
        Task<Purchase> UpdatePurchaseAsync(int purchaseId, int supplierId, int warehouseId, DateTime purchaseDate, DateTime deliveryDate, DateTime deadlineDate, bool save = true);
    }
}
