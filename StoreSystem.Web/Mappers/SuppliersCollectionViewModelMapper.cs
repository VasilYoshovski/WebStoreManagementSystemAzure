using StoreSystem.Data.Models;
using StoreSystem.Web.Models.SupplierViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreSystem.Web.Mappers
{
    public class SuppliersCollectionViewModelMapper : IViewModelMapper<List<Supplier>, SuppliersCollectionViewModel>
    {
        private readonly IViewModelMapper<Supplier, SupplierViewModel> supplierMapper;

        public SuppliersCollectionViewModelMapper(IViewModelMapper<Supplier, SupplierViewModel> supplierMapper)
        {
            this.supplierMapper = supplierMapper ?? throw new ArgumentNullException(nameof(supplierMapper));
        }

        public SuppliersCollectionViewModel MapFrom(List<Supplier> entity)
             => new SuppliersCollectionViewModel
             {
                 Suppliers = entity.Select(d => supplierMapper.MapFrom(d)).ToList()
             };
    }
}
