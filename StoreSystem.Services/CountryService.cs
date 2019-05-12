using Microsoft.EntityFrameworkCore;
using StoreSystem.Data.DbContext;
using StoreSystem.Data.Models;
using StoreSystem.Services.DatabaseServices.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreSystem.Services
{
    public class CountryService : ICountryService
    {
        private readonly StoreSystemDbContext context;
        private readonly IDatabaseJSONConnectivity<string> jSONConnectivityService;

        public CountryService(StoreSystemDbContext context, IDatabaseJSONConnectivity<string> JSONConnectivityService)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
            jSONConnectivityService = JSONConnectivityService;
        }

        public async Task<Country> FindCountryByIDAsync(int countryID)
        {
            return await this.context.Countries.FindAsync(countryID);
        }

        public async Task<Country> FindCountryByNameAsync(string CountryString)
        {
            return await this.context.Countries.FirstOrDefaultAsync(x => x.Name == CountryString);
        }

        public async Task<Country> CreateCountryAsync(string name, bool save = true)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException($"Country name could not be null or empty!");
            }

            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException($"Country name could not be WhiteSpace!");
            }

            var country = await this.context.Countries.FirstOrDefaultAsync(x => x.Name == name);
            if (country == null)
            {
                country = new Country
                {
                    Name = name
                };
                await this.context.Countries.AddAsync(country);
                if (save)
                {
                    await this.context.SaveChangesAsync();
                }
            }
            else
            {
                throw new ArgumentException($"Country with name {name} already exists!");
            }
            return country;
        }

        public async Task<Country> UpdateCountryAsync(int id, string name, bool save = true)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException($"Country name could not be null or empty!");
            }

            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException($"Country name could not be WhiteSpace!");
            }

            var country = await FindCountryByIDAsync(id);
            if (country == null)
            {
                throw new ArgumentException($"Country with ID {id} does not exist!");
            }
            else
            {
                country.Name = name;
                try
                {
                    context.Update(country);
                    await context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    throw;
                }
            }
            return country;
        }

        public async Task<int> PopulateCountriesFromListAsync(List<string> countriesList)
        {
            int countriesCount = 0;
            foreach (var name in countriesList)
            {
                var countryName = name.Trim();
                if (countryName.Length < 2)
                {
                    continue;
                }
                var country = await this.context.Countries.FirstOrDefaultAsync(x => x.Name == countryName);

                if (country == null)
                {
                    country = new Country { Name = countryName };
                    this.context.Countries.Add(country);
                    await this.context.SaveChangesAsync();
                    countriesCount++;
                }
            }
            return countriesCount;
        }

        public async Task<int> PopulateCountriesFromJSONAsync(string jPath, string jName)
        {
            var countriesList = this.jSONConnectivityService.ReadJSON(jPath, jName);
            return await PopulateCountriesFromListAsync(countriesList);
        }

        public async Task<List<Country>> GetAllCountriesAsync()
        {
            var query = this.context.Countries
                .OrderBy(x => x.Name);
            return await query.ToListAsync();
        }

        public async Task<List<Country>> GetAllCountriesByFilterAsync(int from, int to, string contains)
        {
            var allCountries = await GetAllCountriesAsync();
            if (allCountries != null)
            {
                if (!string.IsNullOrWhiteSpace(contains))
                {
                    contains = contains.Trim().ToLower();
                    var filteredCountries = allCountries
                        .Where(x => x.Name.ToLower().Contains(contains))
                        .Skip(from)
                        .Take(to);
                    return filteredCountries.ToList();
                }
            }
            return allCountries;
        }

        public async Task<bool> DeleteCountryAsync(int id)
        {
            var country = await FindCountryByIDAsync(id);
            if (null != country)
            {
                this.context.Countries.Remove(country);
                await context.SaveChangesAsync();
            }
            else
            {
                throw new ArgumentException($"Country with ID {id} does not exist!");
            }
            return true;
        }
    }
}
