using StoreSystem.Data.Models;
using StoreSystem.Web.Models.PurchaseViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreSystem.Web.Mappers
{
    public class PurchaseViewModelMapper : IViewModelMapper<Purchase, PurchaseViewModel>
    {
        public PurchaseViewModel MapFrom(Purchase entity)
             => new PurchaseViewModel
             {
                 PurchaseID = entity.PurchaseID,
                 PurchaseDate = entity.PurchaseDate,
                 DeadlineDate = entity.DeadlineDate,
                 DeliveryDate = entity.DeliveryDate,
                 //products = entity.ProductsТоPurchase,
                 SupplierName = entity.Supplier.Name,
                 SupplierID = entity.SupplierID
             };
    }
}
