using StoreSystem.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StoreSystem.Services
{
    public interface IWarehouseService
    {
        Task<bool> AddPurchaseToWarehouseAsync(int warehouseID, int purchaseID);
        Task<Warehouse> CreateWarehouseAsync(string warehouseName, int countryID, int cityID, int addressID, bool save = true);
        Task<Warehouse> UpdateWarehouseAsync(int id, string warehouseName, int countryID, int cityID, int addressID, bool save = true);
        Task<bool> DeleteWarehouseAsync(int id);
        Task<Warehouse> FindWarehouseByIDAsync(int id);
        Task<Warehouse> GetWarehouseByIDAsync(int id);
        Task<Warehouse> FindWarehouseByNameAsync(string warehouseName);
        Task<Warehouse> GetWarehouseByNameAsync(string warehouseName);
        Task<List<Warehouse>> GetAllWarehousesAsync();
        Task<List<Warehouse>> GetAllWarehousesByFilterAsync(int from, int to, string contains);
    }
}
