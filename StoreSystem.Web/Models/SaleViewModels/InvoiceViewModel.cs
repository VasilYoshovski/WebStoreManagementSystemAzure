using StoreSystem.Web.Models.ClientViewModels;
using StoreSystem.Web.Models.ProductViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreSystem.Web.Models.SaleViewModels
{
    public class InvoiceViewModel
    {

        public SaleInfoViewModel SaleInfo { get; set; }
        public IReadOnlyList<ProductLineInfoViewModel> ProductsInSale { get; set; }
        public ClientInfoViewModel ClientInfo { get; set; }
        public decimal Total { get; set; }
    }
}
