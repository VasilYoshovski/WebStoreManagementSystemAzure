using StoreSystem.Data.Models;
using StoreSystem.Web.Models.SupplierViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreSystem.Web.Mappers
{
    public class SupplierViewModelMapper : IViewModelMapper<Supplier, SupplierViewModel>
    {
        public SupplierViewModel MapFrom(Supplier entity)
             => new SupplierViewModel
             {
                 SupplierID = entity.SupplierID,
                 Name = entity.Name,
                 UIN = entity.UIN,
                 AddressID = entity.AddressID,
                 //AddressText = entity.Address.Name,
                 CityID = entity.CityID,
                 //CityName = entity.City.Name,
                 CountryID = entity.CountryID,
                 //CountryName = entity.Country.Name,
                 MicrosoftUserID = entity.StoreUserId,
                 IsDeleted = entity.IsDeleted
             };
    }
}
