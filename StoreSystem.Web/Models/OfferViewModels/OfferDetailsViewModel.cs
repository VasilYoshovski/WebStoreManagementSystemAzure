using StoreSystem.Web.Models.ProductViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreSystem.Web.Models.OfferViewModels
{
    public class OfferDetailsViewModel
    {
        public OfferInfoViewModel OfferInfo { get; set; }
        public IReadOnlyList<ProductLineInfoViewModel> ProductsInOffer { get; set; }
        public bool CanEdit { get; set; }

    }
}
