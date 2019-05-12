using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreSystem.Web.Models.WarehouseViewModels
{
    public class WarehousesCollectionViewModel
    {
        public IReadOnlyCollection<WarehouseViewModel> Warehouses { get; set; }
    }
}
