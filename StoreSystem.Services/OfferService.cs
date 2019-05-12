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

    public class OfferService : IOfferService
    {
        private readonly StoreSystemDbContext context;
        private readonly IDateTimeNowProvider dateNow;

        public OfferService(StoreSystemDbContext context, IDateTimeNowProvider dateNow)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
            this.dateNow = dateNow ?? throw new ArgumentNullException(nameof(dateNow));
        }

        public async Task<Offer> CreateOfferAsync(
            int clientId,
            decimal discount,
            DateTime expireDate,
            int addressId,
            int cityId,
            int countryId)
        {
            var newOffer = new Offer()
            {
                ClientID = clientId,
                OfferDate = this.dateNow.Now.Date,
                ExpiredDate = expireDate,
                ProductDiscount = discount,
                AddressID = addressId,
                CityID = cityId,
                CountryID = countryId
            };
            this.context.Offers.Add(newOffer);
            await this.context.SaveChangesAsync();
            return newOffer;
        }

        public async Task<bool> AddProductsByIdToOfferAsync(int offerID, params ProductIdQuantityDto[] productsIdListWithQuantity)
        {
            var offer = this.context.Offers.Find(offerID) ?? throw new ArgumentException(string.Format(
                                                                            Consts.ObjectIDNotExist,
                                                                            nameof(Offer),
                                                                            offerID));
            Task<Product> product;

            offer.ProductsInOffer = await Task.WhenAll(productsIdListWithQuantity.Select(async pwq =>
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
                return new ProductOffer() { Product = prodSync, Quantity = pwq.Quantity };

            }));
            return await this.context.SaveChangesAsync() > 0 ? true : false;
        }

        public Task<Offer> GetOfferWithProductsByIDAsync(int offerId)
        {
            return this.context.Offers
                .Include(s => s.ProductsInOffer)
                .FirstOrDefaultAsync(o => o.OfferID == offerId);
        }

        public async Task<Offer> GetOfferByIDAsync(int offerId)
        {
            return (await this.context.Offers.FindAsync(offerId)) ?? throw new ArgumentException(string.Format(
                                                                            Consts.ObjectIDNotExist,
                                                                            nameof(Offer),
                                                                            offerId));
        }

        public async Task<ICollection<ProductOffer>> GetAllProductsInOfferAsync(int offerID)
        {
            var offer = await this.context.Offers
                .Include(x => x.ProductsInOffer)
                .ThenInclude(x => x.Product)
                .FirstOrDefaultAsync(x => x.OfferID == offerID)
                    ?? throw new ArgumentException(string.Format(
                                         Consts.ObjectIDNotExist,
                                         nameof(Offer),
                                         offerID));

            return offer.ProductsInOffer;
        }

        public async Task<IReadOnlyCollection<OfferWithTotalDto>> GetOffersWithTotalAsync
            (int? offerID = null, string clientName = null, int? clientID = null,
            DateTime? startDate = null, DateTime? endDate = null)
        {
            var query = this.context.Offers
                .Include(x => x.Client)
                .AsQueryable();

            if (offerID != null) query = query.Where(x => x.OfferID == offerID);

            if (clientName != null)
            {
                query = query.Where(x => x.Client.Name == clientName);
            }

            if (clientID != null)
            {
                query = query.Where(x => x.Client.ClientID == clientID);
            }

            if (startDate != null && endDate != null)
                query = query.Where(x => x.OfferDate >= startDate && x.OfferDate <= endDate);

            //var res = await query.ToList().Select( x =>
            //        new OfferWithTotalDto()
            //        {
            //            OfferID = x.OfferID,
            //            ClientName = x.Client.Name,
            //            Discount = x.ProductDiscount,
            //            CreatedOn = x.OfferDate,
            //            Total = GetOfferQuantity(x.OfferID, null, null, null, null)
            //        }

            //);
            var list = await query.ToListAsync();
            var res = new List<OfferWithTotalDto>();
            foreach (var x in list)
            {
                res.Add(
                    new OfferWithTotalDto()
                    {
                        OfferID = x.OfferID,
                        ClientName = x.Client.Name,
                        Discount = x.ProductDiscount,
                        CreatedOn = x.OfferDate,
                        Total = await GetOfferQuantityAsync(x.OfferID, null, null, null, null)
                    }
                    );
            }

            return res;
        }

        public async Task<Offer> GetOfferInfoAsync(int? offerId)
        {
            var offer = await this.context.Offers.FindAsync(offerId)
                    ?? throw new ArgumentException(string.Format(
                             Consts.ObjectIDNotExist,
                             nameof(Offer),
                             offerId));

            var offerInfo = this.context.Offers
                    .Include(x => x.Client)
                    .Include(x => x.DeliveryAddress)
                    .Include(x => x.DeliveryCity)
                    .Include(x => x.DeliveryCountry)
                    .FirstOrDefaultAsync(x => x.OfferID == offerId);

            return await offerInfo;
        }

        public Task<decimal> GetOfferQuantityAsync
            (int? offerID = null, string clientName = null, int? clientID = null,
            DateTime? startDate = null, DateTime? endDate = null)
        {
            var query = this.context.Offers
                .Include(x => x.ProductsInOffer)
                .ThenInclude(x => x.Product).AsQueryable();

            if (offerID != null) query = query.Where(x => x.OfferID == offerID);

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
                query = query.Where(x => x.OfferDate >= startDate && x.OfferDate <= endDate);

            return query
                .SelectMany(x => x.ProductsInOffer)
                .Select(x => x.Quantity * x.Product.RetailPrice * (1 - x.Offer.ProductDiscount / 100))
                .SumAsync();
        }

        public async Task<bool> UpdateOfferAsync(int offerID, int clientId, decimal discount, DateTime orderDate,
            double daysToDelivery, DateTime deliveryDate, int addressId, int cityId, int countryId)
        {
            var offer = await this.context.Offers.FindAsync(offerID)
                    ?? throw new ArgumentException(string.Format(
                                         Consts.ObjectIDNotExist,
                                         nameof(Offer),
                                         offerID));

            if (daysToDelivery < 0)
            {
                throw new ArgumentException(string.Format(
                                  Consts.DateError,
                                  offer.OfferDate.Date.AddDays(daysToDelivery),
                                  offer.OfferDate.Date));
            }

            offer.ProductDiscount = discount;
            offer.OfferDate = orderDate;
            offer.CityID = cityId;
            offer.DeliveryDate = deliveryDate;
            offer.AddressID = addressId;
            offer.CountryID = countryId;
            offer.ClientID = clientId;
            offer.ExpiredDate = orderDate.AddDays(daysToDelivery);

            var tChanges = await this.context.SaveChangesAsync();
            return (tChanges > 0) ? true : false;
        }

        public async Task<bool> DeleteOfferAsync(int offerID)
        {
            var offer = await this.context.Offers.FindAsync(offerID)
                    ?? throw new ArgumentException(string.Format(
                                         Consts.ObjectIDNotExist,
                                         nameof(Offer),
                                         offerID));

            this.context.Offers.Remove(offer);

            var prodOffers = (await this.context.ProductOffers.Where(x => x.OfferID == offerID).ToListAsync());
            foreach (var ps in prodOffers)
            {
                (await context.Products.FindAsync(ps.ProductID)).Quantity += ps.Quantity;
                this.context.ProductOffers.Remove(ps);
            }

            var tChanges = await this.context.SaveChangesAsync();
            return (tChanges > 0) ? true : false;
        }


    }
}
