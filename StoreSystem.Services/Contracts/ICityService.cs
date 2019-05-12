using System.Collections.Generic;
using System.Threading.Tasks;
using StoreSystem.Data.Models;

namespace StoreSystem.Services
{
    public interface ICityService
    {
        Task<City> CreateCityAsync(string name, bool save = true);
        Task<City> FindCityByIDAsync(int cityID);
        Task<City> FindCityByNameAsync(string CityString);
        Task<List<City>> GetAllCitiesAsync();
        Task<List<City>> GetAllCitiesByFilterAsync(int from, int to, string contains);
        Task<int> PopulateCitiesFromListAsync(List<string> citiesList);
        Task<int> PopulateCitiesFromJSONAsync(string jPath, string jName);
        Task<bool> DeleteCityAsync(int id);
        Task<City> UpdateCityAsync(int id, string name, bool save = true);
    }
}
