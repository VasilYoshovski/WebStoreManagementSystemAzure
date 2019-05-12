using StoreSystem.Data.Models;
using StoreSystem.Web.Models.SaleViewModels;

namespace StoreSystem.Web.Mappers
{
    public class SaleInfoViewModelMapper : IViewModelMapper<Sale, SaleInfoViewModel>
    {
        public SaleInfoViewModel MapFrom(Sale entity)
        =>
            new SaleInfoViewModel()
            {
                SaleID = entity.SaleID,
                ClientName = entity.Client.Name,
                AddressName = entity.DeliveryAddress.Name,
                CityName = entity.DeliveryCity.Name,
                CountryName = entity.DeliveryCountry.Name,
                OrderDate = entity.OrderDate,
                OfferID = entity.OfferID,
                DeliveryDate = entity.DeliveryDate,
                DeadlineDate = entity.DeadlineDate,
                ProductDiscount = entity.ProductDiscount,
            };
    }
}
