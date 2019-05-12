using Microsoft.EntityFrameworkCore;
using StoreSystem.Data.DbContext;
using StoreSystem.Data.Models;
using StoreSystem.Services.DatabaseServices.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreSystem.Services
{
    public class CityService : ICityService
    {
        private readonly StoreSystemDbContext context;
        private readonly IDatabaseJSONConnectivity<string> jSONConnectivityService;

        public CityService(StoreSystemDbContext context, IDatabaseJSONConnectivity<string> JSONConnectivityService)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
            jSONConnectivityService = JSONConnectivityService;
        }

        public async Task<City> FindCityByNameAsync(string CityString)
        {
            return await this.context.Cities.FirstOrDefaultAsync(x => x.Name == CityString);
        }

        public async Task<City> FindCityByIDAsync(int cityID)
        {
            return await this.context.Cities.FindAsync(cityID);
        }

        public async Task<City> CreateCityAsync(string name, bool save = true)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException($"City name could not be null or empty!");
            }

            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException($"City name could not be WhiteSpace!");
            }

            var city = await this.context.Cities.FirstOrDefaultAsync(x => x.Name == name);
            if (city == null)
            {
                city = new City
                {
                    Name = name
                };
                await this.context.Cities.AddAsync(city);
                if (save)
                {
                    await this.context.SaveChangesAsync();
                }
            }
            else
            {
                throw new ArgumentException($"City with name {name} already exists!");
            }
            return city;
        }

        public async Task<City> UpdateCityAsync(int id, string name, bool save = true)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException($"City name could not be null or empty!");
            }

            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException($"City name could not be WhiteSpace!");
            }

            var city = await FindCityByIDAsync(id);
            if (city == null)
            {
                throw new ArgumentException($"City with ID {id} does not exist!");
            }
            else
            {
                city.Name = name;
                try
                {
                    context.Update(city);
                    await context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    throw;
                }
            }
            return city;
        }

        public async Task<int> PopulateCitiesFromListAsync(List<string> citiesList)
        {
            int citiesCount = 0;
            foreach (var name in citiesList)
            {
                var cityName = name.Trim();
                if (cityName.Length < 2)
                {
                    continue;
                }
                var city = await this.context.Cities.FirstOrDefaultAsync(x => x.Name == cityName);

                if (city == null)
                {
                    city = new City { Name = cityName };
                    this.context.Cities.Add(city);
                    await this.context.SaveChangesAsync();
                    citiesCount++;
                }
            }
            return citiesCount;
        }

        public async Task<int> PopulateCitiesFromJSONAsync(string jPath, string jName)
        {
            var citiesList = this.jSONConnectivityService.ReadJSON(jPath, jName);
            return await PopulateCitiesFromListAsync(citiesList);
        }

        public async Task<List<City>> GetAllCitiesAsync()
        {
            var query = this.context.Cities
                .OrderBy(x => x.Name);
            return await query.ToListAsync();
        }

        public async Task<List<City>> GetAllCitiesByFilterAsync(int from, int to, string contains)
        {
            var allCities = await GetAllCitiesAsync();
            if (allCities != null)
            {
                if (!string.IsNullOrWhiteSpace(contains))
                {
                    contains = contains.Trim().ToLower();
                    var filteredCities = allCities
                        .Where(x => x.Name.ToLower().Contains(contains))
                        .Skip(from)
                        .Take(to);
                    return filteredCities.ToList();
                }
            }
            return allCities;
        }

        public async Task<bool> DeleteCityAsync(int id)
        {
            var city = await FindCityByIDAsync(id);
            if (null != city)
            {
                this.context.Cities.Remove(city);
                await context.SaveChangesAsync();
            }
            else
            {
                throw new ArgumentException($"City with ID {id} does not exist!");
            }
            return true;
        }
    }
}
