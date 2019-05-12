using System;
using System.ComponentModel.DataAnnotations;

namespace StoreSystem.Web.Models.OfferViewModels
{
    public class OfferInfoViewModel
    {
        [Display(Name = "Offer number")]
        public int OfferID { get; set; }

        [Required]
        [Display(Name = "Created on")]
        public DateTime OrderDate { get; set; }

        [Required]
        [Display(Name = "Expired date")]
        public DateTime DeadlineDate { get; set; }

        [Display(Name = "Delivery date")]
        public DateTime DeliveryDate { get; set; }

        [Required]
        [Display(Name = "Client")]
        public string ClientName { get; set; }

        [Display(Name = "Address")]
        public string AddressName { get; set; }

        [Display(Name = "City")]
        public string CityName { get; set; }

        [Display(Name = "Country")]
        public string CountryName { get; set; }

        [Display(Name = "Offer discount (%)")]
        public decimal ProductDiscount { get; set; }
    }
}
