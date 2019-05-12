using Microsoft.EntityFrameworkCore;
using StoreSystem.Data.DbContext;
using StoreSystem.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreSystem.Services
{
    public class AddressService : IAddressService
    {
        private readonly StoreSystemDbContext context;

        public AddressService(StoreSystemDbContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<Address> FindAddressByNameAsync(string addressString)
        {
            return await this.context.Addresses.FirstOrDefaultAsync(x => x.Name == addressString);
        }

        public async Task<Address> FindAddressByIDAsync(int addressID)
        {
            return await this.context.Addresses.FindAsync(addressID);
        }

        public async Task<Address> CreateAddressAsync(string addressString, bool save = true)
        {
            if (string.IsNullOrEmpty(addressString))
            {
                throw new ArgumentNullException($"Address could not be null or empty!");
            }

            if (string.IsNullOrWhiteSpace(addressString))
            {
                throw new ArgumentException($"Address could not be WhiteSpace!");
            }

            var address = await this.context.Addresses.FirstOrDefaultAsync(x => x.Name == addressString);
            if (address == null)
            {
                address = new Address
                {
                    Name = addressString
                };
                await this.context.Addresses.AddAsync(address);
                if (save)
                {
                    await this.context.SaveChangesAsync();
                }
            }
            else
            {
                throw new ArgumentException($"Address with name {addressString} already exists!");
            }
            return address;
        }

        public async Task<Address> UpdateAddressAsync(int id, string addressString, bool save = true)
        {
            if (string.IsNullOrEmpty(addressString))
            {
                throw new ArgumentNullException($"Address could not be null or empty!");
            }

            if (string.IsNullOrWhiteSpace(addressString))
            {
                throw new ArgumentException($"Address could not be WhiteSpace!");
            }

            var address = await FindAddressByIDAsync(id);
            if (address == null)
            {
                throw new ArgumentException($"Address with ID {id} does not exist!");
            }
            else
            {
                address.Name = addressString;
                try
                {
                    context.Update(address);
                    await context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    throw;
                }
            }
            return address;
        }

        public async Task<List<Address>> GetAllAddressesAsync()
        {
            var query = this.context.Addresses
                .OrderBy(x => x.Name);
            return await query.ToListAsync();
        }

        public async Task<List<Address>> GetAllAddressesByFilterAsync(int from, int to, string contains)
        {
            var allAddresses = await GetAllAddressesAsync();
            if (allAddresses != null)
            {
                if (!string.IsNullOrWhiteSpace(contains))
                {
                    contains = contains.Trim().ToLower();
                    var filteredAddresses = allAddresses
                        .Where(x => x.Name.ToLower().Contains(contains))
                        .Skip(from)
                        .Take(to);
                    return filteredAddresses.ToList();
                }
            }
            return allAddresses;
        }

        public async Task<bool> DeleteAddressAsync(int id)
        {
            var address = await FindAddressByIDAsync(id);
            if (null != address)
            {
                this.context.Addresses.Remove(address);
                await context.SaveChangesAsync();
            }
            else
            {
                throw new ArgumentException($"Address with ID {id} does not exist!");
            }
            return true;
        }
    }
}
