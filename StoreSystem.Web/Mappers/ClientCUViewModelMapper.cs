using StoreSystem.Data.Models;
using StoreSystem.Web.Models.ClientViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreSystem.Web.Mappers
{
    public class ClientCUViewModelMapper : IViewModelMapper<Client, ClientCUViewModel>
    {
        public ClientCUViewModel MapFrom(Client entity)
        =>
            new ClientCUViewModel()
            {

                ClientID = entity.ClientID,
                Name = entity.Name,
                AddressId = entity.AddressID,
                CityId = entity.CityID,
                CountryId = entity.CountryID,
                UIN = entity.UIN,

            };
    }
}

