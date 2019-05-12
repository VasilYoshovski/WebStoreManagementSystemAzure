using StoreSystem.Data.Models;
using StoreSystem.Web.Models.SaleViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreSystem.Web.Mappers
{
    public class SaleCUViewModelMapper : IViewModelMapper<Sale, SaleCUViewModel>
    {
        public SaleCUViewModel MapFrom(Sale entity)
        =>
            new SaleCUViewModel()
            {
                SaleID = entity.SaleID,
                ClientID = entity.ClientID,
                AddressID = entity.AddressID,
                CityID = entity.CityID,
                CountryID = entity.CountryID,
                OrderDate = entity.OrderDate,
                OfferID = entity.OfferID,
                DeliveryDate = entity.DeliveryDate,
                DeadlineDate = entity.DeadlineDate,
                ProductDiscount = entity.ProductDiscount,
            };
    }
    
}
