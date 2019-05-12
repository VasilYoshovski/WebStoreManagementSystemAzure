using System.Collections.Generic;
using System.Threading.Tasks;
using StoreSystem.Data.Models;

namespace StoreSystem.Services
{
    public interface IAddressService
    {
        Task<Address> CreateAddressAsync(string addressString, bool save = true);
        Task<Address> FindAddressByIDAsync(int addressID);
        Task<Address> FindAddressByNameAsync(string addressString);
        Task<List<Address>> GetAllAddressesAsync();
        Task<List<Address>> GetAllAddressesByFilterAsync(int from, int to, string contains);
        Task<bool> DeleteAddressAsync(int id);
        Task<Address> UpdateAddressAsync(int id, string addressString, bool save = true);
    }
}
