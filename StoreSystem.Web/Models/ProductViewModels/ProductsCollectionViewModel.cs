using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreSystem.Web.Models.ProductViewModels
{
    public class ProductsCollectionViewModel
    {
        public IReadOnlyCollection<ProductViewModel> Products { get; set; }
        public bool CanEdit { get; set; }

    }
}
