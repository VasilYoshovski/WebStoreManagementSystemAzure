using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using StoreSystem.Data.DbContext;
using StoreSystem.Data.Models;

namespace StoreSystem.Tests.Services.AddressServiceTests.Utils
{
    class DbSeedAddress
    {
        internal static DbContextOptions<StoreSystemDbContext> GetOptions(string databaseName)
        {
            var provider = new ServiceCollection()
                .AddEntityFrameworkInMemoryDatabase()
                .BuildServiceProvider();

            var options = new DbContextOptionsBuilder<StoreSystemDbContext>()
                .UseInMemoryDatabase(databaseName: databaseName)
                .UseInternalServiceProvider(provider)
                .Options;

            return options;
        }

        internal static bool SeedDatabase(DbContextOptions<StoreSystemDbContext> options)
        {
            using (var seedContext = new StoreSystemDbContext(options))
            {
                seedContext.Addresses.AddRange(
                    new Address
                    {
                        Name = "valid address 1"
                    },
                    new Address
                    {
                        Name = "valid address 2"
                    }
                );

                var entityTracked = seedContext.SaveChanges();
                return entityTracked > 0 ? true : false;
            }
        }
    }
}
