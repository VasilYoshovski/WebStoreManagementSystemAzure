using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreSystem.Web.Models.SupplierViewModels
{
    public class SuppliersCollectionViewModel
    {
        public IReadOnlyCollection<SupplierViewModel> Suppliers { get; set; }
    }
}
