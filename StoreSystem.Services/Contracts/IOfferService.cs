using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using StoreSystem.Data.Models;
using StoreSystem.Services.Dto;

namespace StoreSystem.Services
{
    public interface IOfferService
    {
        Task<bool> AddProductsByIdToOfferAsync(int offerID, params ProductIdQuantityDto[] productsIdListWithQuantity);
        Task<Offer> CreateOfferAsync(int clientId, decimal discount, DateTime expireDate, int addressId, int cityId, int countryId);
        Task<bool> DeleteOfferAsync(int offerID);
        Task<ICollection<ProductOffer>> GetAllProductsInOfferAsync(int offerID);
        Task<Offer> GetOfferByIDAsync(int offerId);
        Task<Offer> GetOfferInfoAsync(int? offerId);
        Task<decimal> GetOfferQuantityAsync(int? offerID = null, string clientName = null, int? clientID = null, DateTime? startDate = null, DateTime? endDate = null);
        Task<IReadOnlyCollection<OfferWithTotalDto>> GetOffersWithTotalAsync(int? offerID = null, string clientName = null, int? clientID = null, DateTime? startDate = null, DateTime? endDate = null);
        Task<Offer> GetOfferWithProductsByIDAsync(int offerId);
        Task<bool> UpdateOfferAsync(int offerID, int clientId, decimal discount, DateTime orderDate, double daysToDelivery, DateTime deliveryDate, int addressId, int cityId, int countryId);
    }
}