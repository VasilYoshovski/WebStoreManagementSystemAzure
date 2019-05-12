using StoreSystem.Data.Models;
using StoreSystem.Web.Models.PurchaseViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreSystem.Web.Mappers
{
    public class PurchasesCollectionViewModelMapper : IViewModelMapper<List<Purchase>, PurchasesCollectionViewModel>
    {
        private readonly IViewModelMapper<Purchase, PurchaseViewModel> purchaseMapper;

        public PurchasesCollectionViewModelMapper(IViewModelMapper<Purchase, PurchaseViewModel> purchaseMapper)
        {
            this.purchaseMapper = purchaseMapper ?? throw new ArgumentNullException(nameof(purchaseMapper));
        }

        public PurchasesCollectionViewModel MapFrom(List<Purchase> entity)
             => new PurchasesCollectionViewModel
             {
                 Purchases = entity.Select(p => purchaseMapper.MapFrom(p)).ToList()
             };
    }
}
