using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreSystem.Web.Models.PurchaseViewModels
{
    public class PurchasesCollectionViewModel
    {
        public IReadOnlyCollection<PurchaseViewModel> Purchases { get; set; }
    }
}
