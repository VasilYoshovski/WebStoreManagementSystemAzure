using StoreSystem.Data.Models;
using StoreSystem.Web.Models.ProductViewModels;

namespace StoreSystem.Web.Mappers
{
    public class ProductInOfferInfoViewModelMapper : IViewModelMapper<ProductOffer, ProductLineInfoViewModel>
    {
        public ProductLineInfoViewModel MapFrom(ProductOffer entity)
             => new ProductLineInfoViewModel
             {
                 ProductID = entity.ProductID,
                 Name = entity.Product.Name,
                 Quantity = entity.Quantity,
                 Measure = entity.Product.Measure,
                 Price = entity.Product.RetailPrice,
                 Total = entity.Product.RetailPrice * entity.Quantity * (1 - entity.Offer.ProductDiscount / 100)
             };
    }
}
