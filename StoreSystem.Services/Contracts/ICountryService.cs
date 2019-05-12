using System.Collections.Generic;
using System.Threading.Tasks;
using StoreSystem.Data.Models;

namespace StoreSystem.Services
{
    public interface ICountryService
    {
        Task<Country> CreateCountryAsync(string name, bool save = true);
        Task<Country> FindCountryByIDAsync(int countryID);
        Task<Country> FindCountryByNameAsync(string CountryString);
        Task<List<Country>> GetAllCountriesAsync();
        Task<List<Country>> GetAllCountriesByFilterAsync(int from, int to, string contains);
        Task<int> PopulateCountriesFromListAsync(List<string> citiesList);
        Task<int> PopulateCountriesFromJSONAsync(string jPath, string jName);
        Task<bool> DeleteCountryAsync(int id);
        Task<Country> UpdateCountryAsync(int id, string name, bool save = true);
    }
}
