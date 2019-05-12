using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using StoreSystem.Data.Models;
using StoreSystem.Services.Dto;

namespace StoreSystem.Services
{
    public interface ISaleService
    {
        Task<bool> AddProductsByIdToSaleAsync(int saleID, params ProductIdQuantityDto[] productsIdListWithQuantity);
        Task<Sale> CreateSaleAsync(Client client, decimal discount, double daysToDelivery, Address address, City city, Country country, int? offerID = null, bool save = true);
        Task<Sale> CreateSaleAsync(int clientId, decimal discount, DateTime deadlineDate, int addressId, int cityId, int countryId, int? offerID = null);
        Task<Sale> CreateSaleByOfferIDAsync(int offerID);
        Task<bool> DeleteSaleAsync(int saleID);
        Task<ICollection<ProductSale>> GetAllProductsInSaleAsync(int saleID);
        Task<ICollection<int>> GetNotClosedSalesAsync();
        Task<decimal> GetSaleQuantityAsync(int? saleID = null, string clientName = null, int? clientID = null, DateTime? startDate = null, DateTime? endDate = null);
        Task<Sale> GetSaleWithProductsByIDAsync(int saleId);
        Task<bool> UpdateSaleAsync(int saleID, int clientId, decimal discount, DateTime orderDate, double daysToDelivery, DateTime deliveryDate, int addressId, int cityId, int countryId, int? offerID = null);
        Task<IReadOnlyCollection<SaleWithTotalDto>> GetSalesWithTotalAsync
            (int? saleID = null, string clientName = null, int? clientID = null,
            DateTime? startDate = null, DateTime? endDate = null);
        Task<Sale> GetSaleInfoAsync(int? saleId);
    }
}