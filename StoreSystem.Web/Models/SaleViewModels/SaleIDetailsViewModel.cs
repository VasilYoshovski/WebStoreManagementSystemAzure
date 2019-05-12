using StoreSystem.Web.Models.ProductViewModels;
using System.Collections.Generic;

namespace StoreSystem.Web.Models.SaleViewModels
{
    public class SaleIDetailsViewModel
    {
        public SaleInfoViewModel SaleInfo { get; set; }
        public IReadOnlyList<ProductLineInfoViewModel> ProductsInSale { get; set; }
        public bool CanEdit { get; set; }

    }
}
