using Microsoft.EntityFrameworkCore;
using StoreSystem.Data.DbContext;
using StoreSystem.Data.Models;
using StoreSystem.Services.Dto;
using StoreSystem.Services.Providers;
using StoreSystem.Services.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreSystem.Services
{

    public class SaleService : ISaleService
    {
        private readonly StoreSystemDbContext context;
        private readonly IDateTimeNowProvider dateNow;

        public SaleService(StoreSystemDbContext context, IDateTimeNowProvider dateNow)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
            this.dateNow = dateNow ?? throw new ArgumentNullException(nameof(dateNow));
        }

        public async Task<Sale> CreateSaleAsync(
            Client client,
            decimal discount,
            double daysToDelivery,
            Address address,
            City city,
            Country country,
            int? offerID = null,
            bool save = true)
        {
            var newSale = new Sale()
            {
                Client = client,
                ProductDiscount = discount,
                OrderDate = this.dateNow.Now.Date,
                DeadlineDate = this.dateNow.Now.AddDays(daysToDelivery),
                DeliveryAddress = address,
                DeliveryCity = city,
                DeliveryCountry = country,
                OfferID = offerID
            };
            this.context.Sales.Add(newSale);
            await this.context.SaveChangesAsync();
            return newSale;
        }

        public async Task<Sale> CreateSaleAsync(
            int clientId,
            decimal discount,
            DateTime deadlineDate,
            int addressId,
            int cityId,
            int countryId,
            int? offerID = null)
        {
            var newSale = new Sale()
            {
                ClientID = clientId,
                OrderDate = this.dateNow.Now.Date,
                DeadlineDate = deadlineDate,
                ProductDiscount = discount,
                AddressID = addressId,
                CityID = cityId,
                CountryID = countryId,
                OfferID = offerID,
            };
            this.context.Sales.Add(newSale);
            await this.context.SaveChangesAsync();
            return newSale;
        }

        public async Task<Sale> CreateSaleByOfferIDAsync(int offerID)
        {
            var offer = await this.context.Offers.FindAsync(offerID) ?? throw new ArgumentException(string.Format(
                                                                            Consts.ObjectIDNotExist,
                                                                            nameof(Offer),
                                                                            offerID));


            var sale = await this.CreateSaleAsync(offer.Client, offer.ProductDiscount, (offer.ExpiredDate - offer.OfferDate).Days, offer.DeliveryAddress, offer.DeliveryCity, offer.DeliveryCountry, offerID, save: false);
            if (offer.ProductsInOffer != null && offer.ProductsInOffer?.Count > 0)
            {
                var products = offer.ProductsInOffer.Select(x => new ProductIdQuantityDto(x.ProductID, x.Quantity)).ToArray();
                await this.AddProductsByIdToSaleAsync(sale.SaleID, products);
            }
            else
            {
                await this.context.SaveChangesAsync();
            }
            return sale;
        }

        public async Task<bool> AddProductsByIdToSaleAsync(int saleID, params ProductIdQuantityDto[] productsIdListWithQuantity)
        {
            var sale = this.context.Sales.Find(saleID) ?? throw new ArgumentException(string.Format(
                                                                            Consts.ObjectIDNotExist,
                                                                            nameof(Sale),
                                                                            saleID));
            if (productsIdListWithQuantity != null && productsIdListWithQuantity?.Length > 0)
            {
                Task<Product> product;

                sale.ProductsInSale = await Task.WhenAll(productsIdListWithQuantity.Select(async pwq =>
                {
                    product = this.context.Products.FindAsync(pwq.ProductID) ?? throw new ArgumentException(string.Format(
                                                                                Consts.ObjectIDNotExist,
                                                                                nameof(Product),
                                                                                pwq.ProductID));
                    var prodSync = await product;
                    if (prodSync.Quantity < pwq.Quantity)
                    {
                        throw new ArgumentException(string.Format(
                                          Consts.QuantityNotEnough,
                                          prodSync.Name,
                                          prodSync.Quantity));
                    }
                    prodSync.Quantity -= pwq.Quantity;
                    return new ProductSale() { Product = prodSync, Quantity = pwq.Quantity };

                }));
                return await this.context.SaveChangesAsync() > 0 ? true : false;
            }
            return false;
        }

        public Task<Sale> GetSaleWithProductsByIDAsync(int saleId)
        {
            return this.context.Sales
                .Include(s => s.ProductsInSale)
                .FirstOrDefaultAsync(o => o.SaleID == saleId);
        }

        public async Task<ICollection<ProductSale>> GetAllProductsInSaleAsync(int saleID)
        {
            var sale = await this.context.Sales
                .Include(x => x.ProductsInSale)
                .ThenInclude(x => x.Product)
                .FirstOrDefaultAsync(x => x.SaleID == saleID)
                    ?? throw new ArgumentException(string.Format(
                                         Consts.ObjectIDNotExist,
                                         nameof(Sale),
                                         saleID));

            return sale.ProductsInSale;
        }

        public async Task<IReadOnlyCollection<SaleWithTotalDto>> GetSalesWithTotalAsync
            (int? saleID = null, string clientName = null, int? clientID = null,
            DateTime? startDate = null, DateTime? endDate = null)
        {
            var query = this.context.Sales
                .Include(x => x.Client)
                .AsQueryable();

            if (saleID != null) query = query.Where(x => x.SaleID == saleID);

            if (clientName != null)
            {
                query = query.Where(x => x.Client.Name == clientName);
            }

            if (clientID != null)
            {
                query = query.Where(x => x.Client.ClientID == clientID);
            }

            if (startDate != null && endDate != null)
                query = query.Where(x => x.OrderDate >= startDate && x.OrderDate <= endDate);

            //var res = await query.ToList().Select( x =>
            //        new SaleWithTotalDto()
            //        {
            //            SaleID = x.SaleID,
            //            ClientName = x.Client.Name,
            //            Discount = x.ProductDiscount,
            //            CreatedOn = x.OrderDate,
            //            Total = GetSaleQuantity(x.SaleID, null, null, null, null)
            //        }

            //);
            var list = await query.ToListAsync();
            var res = new List<SaleWithTotalDto>();
            foreach (var x in list)
            {
                res.Add(
                    new SaleWithTotalDto()
                    {
                        SaleID = x.SaleID,
                        ClientName = x.Client.Name,
                        Discount = x.ProductDiscount,
                        CreatedOn = x.OrderDate,
                        DeadlineDate = x.DeadlineDate,
                        Total = await this.GetSaleQuantityAsync(x.SaleID, null, null, null, null)
                    }
                    );
            }

            return res;
        }

        public async Task<Sale> GetSaleByIDAsync(int saleId)
        {
            return (await this.context.Sales.FindAsync(saleId)) ?? throw new ArgumentException(string.Format(
                                                                            Consts.ObjectIDNotExist,
                                                                            nameof(Offer),
                                                                            saleId));
        }

        public async Task<Sale> GetSaleInfoAsync(int? saleId)
        {
            var sale = await this.context.Sales.FindAsync(saleId)
                    ?? throw new ArgumentException(string.Format(
                             Consts.ObjectIDNotExist,
                             nameof(Sale),
                             saleId));

            var saleInfo = this.context.Sales
                    .Include(x => x.Client)
                    .Include(x => x.DeliveryAddress)
                    .Include(x => x.DeliveryCity)
                    .Include(x => x.DeliveryCountry)
                    .FirstOrDefaultAsync(x => x.SaleID == saleId);

            return await saleInfo;
        }

        public Task<decimal> GetSaleQuantityAsync
            (int? saleID = null, string clientName = null, int? clientID = null,
            DateTime? startDate = null, DateTime? endDate = null)
        {
            var query = this.context.Sales
                .Include(x => x.ProductsInSale)
                .ThenInclude(x => x.Product).AsQueryable();

            if (saleID != null) query = query.Where(x => x.SaleID == saleID);

            if (clientName != null)
            {
                query = query.Include(x => x.Client);
                query = query.Where(x => x.Client.Name == clientName);
            }

            if (clientID != null)
            {
                query = query.Include(x => x.Client);
                query = query.Where(x => x.Client.ClientID == clientID);
            }

            if (startDate != null && endDate != null)
                query = query.Where(x => x.OrderDate >= startDate && x.OrderDate <= endDate);

            return query
                .SelectMany(x => x.ProductsInSale)
                .Select(x => x.Quantity * x.Product.RetailPrice * (1 - x.Sale.ProductDiscount / 100))
                .SumAsync();
        }

        public async Task<bool> UpdateSaleAsync(int saleID, int clientId, decimal discount, DateTime orderDate,
            double daysToDelivery, DateTime deliveryDate, int addressId, int cityId, int countryId, int? offerID = null)
        {
            var sale = await this.context.Sales.FindAsync(saleID)
                    ?? throw new ArgumentException(string.Format(
                                         Consts.ObjectIDNotExist,
                                         nameof(Sale),
                                         saleID));

            if (daysToDelivery < 0)
            {
                throw new ArgumentException(string.Format(
                                  Consts.DateError,
                                  sale.OrderDate.Date.AddDays(daysToDelivery),
                                  sale.OrderDate.Date));
            }

            sale.ProductDiscount = discount;
            sale.OrderDate = orderDate;
            if (offerID != null) sale.OfferID = offerID;
            sale.CityID = cityId;
            sale.DeliveryDate = deliveryDate;
            sale.AddressID = addressId;
            sale.CountryID = countryId;
            sale.ClientID = clientId;
            sale.DeadlineDate = orderDate.AddDays(daysToDelivery);

            var tChanges = await this.context.SaveChangesAsync();
            return (tChanges > 0) ? true : false;
        }

        public async Task<ICollection<int>> GetNotClosedSalesAsync()
        {
            return await this.context.Sales.Where(x => x.DeliveryDate < x.OrderDate).Select(x => x.SaleID).ToListAsync();
        }

        public async Task<bool> DeleteSaleAsync(int saleID)
        {
            var sale = await this.context.Sales.FindAsync(saleID)
                    ?? throw new ArgumentException(string.Format(
                                         Consts.ObjectIDNotExist,
                                         nameof(Sale),
                                         saleID));

            this.context.Sales.Remove(sale);

            var prodSales = (await this.context.ProductSales.Where(x => x.SaleID == saleID).ToListAsync());
            foreach (var ps in prodSales)
            {
                (await this.context.Products.FindAsync(ps.ProductID)).Quantity += ps.Quantity;
                this.context.ProductSales.Remove(ps);
            }

            var tChanges = await this.context.SaveChangesAsync();
            return (tChanges > 0) ? true : false;
        }


    }
}
