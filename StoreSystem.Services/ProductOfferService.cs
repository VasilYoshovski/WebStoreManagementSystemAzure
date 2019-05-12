using StoreSystem.Data.DbContext;
using StoreSystem.Data.Models;
using StoreSystem.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StoreSystem.Services
{
    public class ProductOfferService : IProductOfferService
    {
        private readonly StoreSystemDbContext context;

        public ProductOfferService(StoreSystemDbContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public ICollection<ProductIdQuantityDto> GetProductSalesByOfferID(int offerId)
        {
            return this.context.ProductOffers
                .Where(x => x.OfferID == offerId)
                .Select(x => new ProductIdQuantityDto(x.ProductID,x.Quantity))
                .ToList();
        }

    }
}
