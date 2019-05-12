using System;
using System.Collections.Generic;
using System.Linq;
using StoreSystem.Data.Models;
using StoreSystem.Web.Models.ProductViewModels;

namespace StoreSystem.Web.Mappers
{
    public class ProductsCollectionModelMapper : IViewModelMapper<IReadOnlyCollection<Product>, ProductsCollectionViewModel>
    {
        private readonly IViewModelMapper<Product, ProductViewModel> productMapper;

        public ProductsCollectionModelMapper(
            IViewModelMapper<Product, ProductViewModel> productMapper)
        {
            this.productMapper = productMapper ?? throw new ArgumentNullException(nameof(productMapper));
        }

        public ProductsCollectionViewModel MapFrom(IReadOnlyCollection<Product> entity)
             => new ProductsCollectionViewModel
             {
                 Products = entity.Select(this.productMapper.MapFrom).ToList(),
             };
    }
}
