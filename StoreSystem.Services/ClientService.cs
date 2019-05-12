using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using StoreSystem.Data;
using StoreSystem.Data.DbContext;
using StoreSystem.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreSystem.Services
{
    public class ClientService : IClientService
    {
        private readonly StoreSystemDbContext context;
        private readonly UserManager<StoreUser> userManager;
        private readonly IDatabaseService databaseService;

        public ClientService(
            StoreSystemDbContext context,
            UserManager<StoreUser> userManager,
            IDatabaseService databaseService)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
            this.userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            this.databaseService = databaseService;
        }

        //no
        public async Task<Client> CreateClientAsync(string name, string UIN, Address address, City city, Country country, StoreUser storeUser)
        {
            if (await this.context.Clients.AnyAsync(x => x.Name == name))
            {
                throw new ArgumentException($"There is client with name {name}");
            }
            var client = new Client()
            {
                Name = name,
                UIN = UIN,
                Address = address,
                City = city,
                Country = country,
                StoreUser = storeUser
            };
            this.context.Clients.Add(client);
            await this.context.SaveChangesAsync();
            return client;
        }

        public async Task<Client> CreateClientAsync(
            string name,
            string UIN,
            int addressId,
            int cityId,
            int countryId,
            string userId,
            bool save = true)
        {
            if (await this.context.Clients.Where(x => x.IsDeleted==false).AnyAsync(x => x.Name == name))
            {
                throw new ArgumentException($"There is client with name {name}");
            }
            var client = new Client()
            {
                Name = name,
                UIN = UIN,
                AddressID = addressId,
                CityID = cityId,
                CountryID = countryId,
                StoreUserId = userId,
                IsDeleted=false
            };
            this.context.Clients.Add(client);
            var roleResult = await this.databaseService.SetRoleToNewUserAsync(client.StoreUserId, ROLES.Client);
            await this.context.SaveChangesAsync();

            var user = await this.context.StoreUsers.FindAsync(userId);
            user.ClientId = client.ClientID;
            await this.context.SaveChangesAsync();

            return client;
        }

        public Task<Client> FindClientByNameAsync(string clientName)
        {
            return this.context.Clients.FirstOrDefaultAsync(x => x.Name == clientName);
        }

        public Task<Client> FindClientByIDAsync(int clientID)
        {
            return this.context.Clients.FindAsync(clientID);
        }

        public Task<Client> FindClientWithAddressAsync(string clientName)
        {
            return this.context.Clients
                               .Include(x => x.Address)
                               .Include(x => x.City)
                               .Include(x => x.Country)
                               .FirstOrDefaultAsync(x => x.Name == clientName);
        }

        public Task<Client> FindClientWithAddressAsync(int clientId)
        {
            return this.context.Clients
                               .Include(x => x.Address)
                               .Include(x => x.City)
                               .Include(x => x.Country)
                               .FirstOrDefaultAsync(x => x.ClientID == clientId);
        }

        public Task<List<Client>> GetAllClientsWithAddressAsync(int from, int to, string contains = "*")
        {
            var query = this.context.Clients
                .Include(x => x.Address)
                .Include(x => x.City)
                .Include(x => x.Country)
                .Include(x => x.StoreUser)
                .Where(x => x.IsDeleted==false)
                .OrderBy(x => x.Name)
                .AsQueryable();

            if (contains != "*")
            {
                query = query.Where(x => x.Name.Contains(contains));
            }
            return query.ToListAsync();
        }
        public Task<List<Client>> GetAllClientsAsync(int from, int to, string contains = "*")
        {
            var query = this.context.Clients.OrderBy(x => x.Name).Where(x => x.IsDeleted==false);
            if (contains != "*")
            {
                query = query.Where(x => x.Name.Contains(contains));
            }
            return query.Skip(from).Take(to).ToListAsync();
        }

        public async Task<bool> UpdateClientAsync(int clientID, string name, string UIN, Address address, City city, Country country)
        {
            const string nonModify = "-";

            var client = await this.context.Clients.FindAsync(clientID);

            if (name != nonModify)
            {
                if (await this.context.Clients.AnyAsync(x => x.Name == name))
                {
                    throw new ArgumentException($"There is client with name {name}");
                }
                client.Name = name;
            }

            if (UIN != nonModify) client.UIN = UIN;
            if (address != null) client.Address = address;
            if (city != null) client.City = city;
            if (country != null) client.Country = country;

            return (await this.context.SaveChangesAsync()) > 0 ? true : false;
        }

        public async Task<bool> UpdateClientAsync(int clientID, string name, string UIN, int addressId, int cityId, int countryId)
        {
            var client = await this.context.Clients.FindAsync(clientID);

            if (name != client.Name)
            {
                if (await this.context.Clients.Where(x => x.IsDeleted == false).AnyAsync(x => x.Name == name))
                {
                    throw new ArgumentException($"There is client with name {name}");
                }
                client.Name = name;
            }

            client.UIN = UIN;
            client.AddressID = addressId;
            client.CityID = cityId;
            client.CountryID = countryId;

            return (await this.context.SaveChangesAsync()) > 0 ? true : false;
        }
        public async Task<ICollection<Sale>> GetClientSalesAsync(int clientID)
        {
            var client = await context.Clients
                .Include(x => x.Sales)
                .FirstOrDefaultAsync(x => x.ClientID == clientID) // We use FIrstOrDefault because it's faster than SIngleOrDefault
                ?? throw new ArgumentException($"Client with ID {clientID} can not be found");
            return client.Sales;
        }

        public async Task<ICollection<Sale>> GetClientSalesAsync(string clientName)
        {
            var client = (await context.Clients
                .Include(x => x.Sales)
                .FirstOrDefaultAsync(x => x.Name == clientName)) // We use FIrstOrDefault because it's faster than SIngleOrDefault
                ?? throw new ArgumentException($"Client with name {clientName} can not be found");
            return client.Sales;
        }

        public async Task<string> DeleteClient(int clientId)
        {
            var client = await this.context.Clients.FindAsync(clientId);
            var userId = client.StoreUserId;
            client.IsDeleted = true;
            return (await this.context.SaveChangesAsync()) > 0 ? userId : null;
        }

    }
}
