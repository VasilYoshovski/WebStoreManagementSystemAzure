using StoreSystem.Data.Models;
using StoreSystem.Web.Models.WarehouseViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreSystem.Web.Mappers
{
    public class WarehouseViewModelMapper : IViewModelMapper<Warehouse, WarehouseViewModel>
    {
        public WarehouseViewModel MapFrom(Warehouse entity)
             => new WarehouseViewModel
             {
                 WarehouseID = entity.WarehouseID,
                 Name = entity.Name,
                 AddressID = entity.AddressID,
                 AddressText = entity.Address.Name,
                 CityID = entity.CityID,
                 CityName = entity.City.Name,
                 CountryID = entity.CountryID,
                 CountryName = entity.Country.Name
             };
    }
}
