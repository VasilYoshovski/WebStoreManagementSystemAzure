using StoreSystem.Data.Models;
using StoreSystem.Web.Models.OfferViewModels;

namespace StoreSystem.Web.Mappers
{
    public class OfferInfoViewModelMapper : IViewModelMapper<Offer, OfferInfoViewModel>
    {
        public OfferInfoViewModel MapFrom(Offer entity)
        =>
            new OfferInfoViewModel()
            {
                OfferID = entity.OfferID,
                ClientName = entity.Client.Name,
                AddressName = entity.DeliveryAddress.Name,
                CityName = entity.DeliveryCity.Name,
                CountryName = entity.DeliveryCountry.Name,
                OrderDate = entity.OfferDate,
                DeliveryDate = entity.DeliveryDate,
                DeadlineDate = entity.ExpiredDate,
                ProductDiscount = entity.ProductDiscount,
            };
    }
}
