using StoreSystem.Data.Models;
using StoreSystem.Web.Models.ProductViewModels;

namespace StoreSystem.Web.Mappers
{
    public class ProductViewModelMapper : IViewModelMapper<Product, ProductViewModel>
    {
        public ProductViewModel MapFrom(Product entity)
             => new ProductViewModel
             {
                 ProductID = entity.ProductID,
                 Name = entity.Name,
                 Quantity = entity.Quantity,
                 Measure = entity.Measure,
                 BuyPrice = entity.BuyPrice,
                 RetailPrice = entity.RetailPrice
             };
    }
}
