using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using StoreSystem.Data.DbContext;
using StoreSystem.Data.Models;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace StoreSystem.Tests.Services
{
    internal static class DbSeed
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

                seedContext.Cities.AddRange(
                    new City
                    {
                        Name = "valid city 1"
                    },
                    new City
                    {
                        Name = "valid city 2"
                    }
                );

                seedContext.Countries.AddRange(
                    new Country
                    {
                        Name = "valid country 1"
                    },
                    new Country
                    {
                        Name = "valid country 2"
                    }
                );

                seedContext.Clients.AddRange(
                    new Client
                    {
                        Name = "valid client 1",
                        AddressID = 1,
                        CityID = 1,
                        CountryID = 1,
                        UIN = "123456789",
          
                    },
                    new Client
                    {
                        Name = "valid client 2",
                        AddressID = 2,
                        CityID = 3,
                        CountryID = 2,
                        UIN = "987654321",
             
                    }
                );
                
                seedContext.Products.AddRange(
                    new Product
                    {
                        Name = "valid product 1",
                        Measure = "v1",
                        Quantity = 1,
                        BuyPrice = 1,
                        RetailPrice = 1
                    },
                    new Product
                    {
                        Name = "valid product 2",
                        Measure = "v2",
                        Quantity = 2,
                        BuyPrice = 2,
                        RetailPrice = 2
                    },
                    new Product
                    {
                        Name = "valid product 3",
                        Measure = "v3",
                        Quantity = 3,
                        BuyPrice = 3,
                        RetailPrice = 3
                    }
                );

                seedContext.Sales.Add(
                    new Sale
                    {
                        ClientID = 1,
                        AddressID = 1,
                        CityID = 1,
                        CountryID = 1,
                        OfferID = 1,
                        ProductDiscount = 0.1m,
                        DeadlineDate = new DateTime(2019, 1, 1),
                        DeliveryDate = new DateTime(1, 1, 1),
                        OrderDate = new DateTime(2019, 1, 1),
                        ProductsInSale = new List<ProductSale>
                        {
                            new ProductSale{ProductID = 1, Quantity =1 },
                            new ProductSale{ProductID = 2, Quantity =1 }
                        }
                    }
                );

                seedContext.Sales.Add(
                    new Sale
                    {
                        ClientID = 1,
                        AddressID = 1,
                        CityID = 1,
                        CountryID = 1,
                        OfferID = 1,
                        ProductDiscount = 0.1m,
                        DeadlineDate = new DateTime(2019, 2, 2),
                        DeliveryDate = new DateTime(2019, 2, 2),
                        OrderDate = new DateTime(2019, 2, 2),
                        ProductsInSale = new List<ProductSale>
                        {
                            new ProductSale{ProductID = 3, Quantity =1 },
                        }
                    }
                );

                seedContext.Sales.Add(
                    new Sale
                    {
                        ClientID = 2,
                        AddressID = 2,
                        CityID = 2,
                        CountryID = 2,
                        OfferID = 2,
                        ProductDiscount = 0.1m,
                        DeadlineDate = new DateTime(2019, 3, 3),
                        DeliveryDate = new DateTime(2019, 3, 3),
                        OrderDate = new DateTime(2019, 3, 3),
                    }
                    );
                seedContext.Sales.Add(
                    new Sale
                    {
                        ClientID = 2,
                        AddressID = 2,
                        CityID = 2,
                        CountryID = 2,
                        OfferID = 2,
                        ProductDiscount = 0.1m,
                        DeadlineDate = new DateTime(2019, 4, 4),
                        DeliveryDate = new DateTime(2019, 4, 4),
                        OrderDate = new DateTime(2019, 4, 4),
                        ProductsInSale = new List<ProductSale>
                        {
                            new ProductSale{ProductID = 1, Quantity =1 },
                            new ProductSale{ProductID = 3, Quantity =1 }
                        }
                    }
                    );


                seedContext.Offers.AddRange(
                    new Offer
                    {
                        ClientID = 1,
                        AddressID = 1,
                        CityID = 1,
                        CountryID = 1,
                        ProductDiscount = 0.1m,
                        ExpiredDate = new DateTime(2019, 1, 1),
                        DeliveryDate = new DateTime(2019, 1, 1),
                        OfferDate = new DateTime(2019, 1, 1),
                        ProductsInOffer = new List<ProductOffer>
                        {
                            new ProductOffer{ProductID = 1, Quantity =1 },
                        }
                    }
                );

                var entityTracked = seedContext.SaveChanges();
                return entityTracked > 0 ? true : false;
            }
        }
    }
}
