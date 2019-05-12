using StoreSystem.Services.Dto;
using System.Collections.Generic;

namespace StoreSystem.Web.Models.OfferViewModels
{
    public class OfferIndexViewModel
    {
        public IEnumerable<OfferWithTotalDto> OffersList { get; set; }
        public bool CanEdit { get; set; }

    }
}
