using StoreSystem.Services.Dto;
using System.Collections.Generic;

namespace StoreSystem.Web.Models.SaleViewModels
{
    public class SaleIndexViewModel
    {
        public IEnumerable<SaleWithTotalDto> SalesList { get; set; }
        public IEnumerable<int> NotClosedSales { get; set; }
        public bool CanEdit { get; set; }
    }
}
