using System.Collections.Generic;
using System.Threading.Tasks;
using StoreSystem.Data.Models;

namespace StoreSystem.Services
{
    public interface IClientService
    {
        Task<Client> CreateClientAsync(string name, string UIN, Address address, City city, Country country, StoreUser storeUser);
        Task<Client> CreateClientAsync(string name, string UIN, int addressId, int cityId, int countryId, string userId, bool save = true);
        Task<Client> FindClientByIDAsync(int clientID);
        Task<Client> FindClientByNameAsync(string clientName);
        Task<Client> FindClientWithAddressAsync(int clientId);
        Task<Client> FindClientWithAddressAsync(string clientName);
        Task<List<Client>> GetAllClientsAsync(int from = 0, int to = int.MaxValue, string contains = "*");
        Task<List<Client>> GetAllClientsWithAddressAsync(int from = 0, int to = int.MaxValue, string contains = "*");
        Task<ICollection<Sale>> GetClientSalesAsync(int clientID);
        Task<ICollection<Sale>> GetClientSalesAsync(string clientName);
        Task<bool> UpdateClientAsync(int clientID, string name, string UIN, Address address, City city, Country country);
        Task<bool> UpdateClientAsync(int clientID, string name, string UIN, int addressId, int cityId, int countryId);
        Task<string> DeleteClient(int clientId);


    }
}