using System.Collections.Generic;
using StoreSystem.Services.Dto;

namespace StoreSystem.Services
{
    public interface IProductOfferService
    {
        ICollection<ProductIdQuantityDto> GetProductSalesByOfferID(int offerId);
    }
}