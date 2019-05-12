using StoreSystem.Data.Models;
using StoreSystem.Web.Models.OfferViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreSystem.Web.Mappers
{
    public class OfferCUViewModelMapper : IViewModelMapper<Offer, OfferCUViewModel>
    {
        public OfferCUViewModel MapFrom(Offer entity)
        =>
            new OfferCUViewModel()
            {
                OfferID = entity.OfferID,
                ClientID = entity.ClientID,
                AddressID = entity.AddressID,
                CityID = entity.CityID,
                CountryID = entity.CountryID,
                OrderDate = entity.OfferDate,
                DeliveryDate = entity.DeliveryDate,
                DeadlineDate = entity.ExpiredDate,
                ProductDiscount = entity.ProductDiscount,
            };
    }

}
