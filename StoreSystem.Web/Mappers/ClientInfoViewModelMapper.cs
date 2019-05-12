using StoreSystem.Data.Models;
using StoreSystem.Web.Models.ClientViewModels;

namespace StoreSystem.Web.Mappers
{
    public class ClientInfoViewModelMapper : IViewModelMapper<Client, ClientInfoViewModel>
    {
        public ClientInfoViewModel MapFrom(Client entity)
        =>
            new ClientInfoViewModel()
            {

                ClientID = entity.ClientID,
                Name = entity.Name,
                AddressName = entity.Address.Name,
                CityName = entity.City.Name,
                CountryName = entity.Country.Name,
                UIN = entity.UIN,
                StoreUserId = entity.StoreUserId,
                StoreUsername = ""
            };
    }
}
