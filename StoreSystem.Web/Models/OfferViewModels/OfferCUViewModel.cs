using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace StoreSystem.Web.Models.OfferViewModels
{
    public class OfferCUViewModel
    {
        [Display(Name = "Offer number")]
        public int OfferID { get; set; }

        [Required]
        [Display(Name = "Offer date")]
        public DateTime OrderDate { get; set; }

        [Required]
        [Display(Name = "Exired date")]
        public DateTime DeadlineDate { get; set; }

        [Display(Name = "Delivery date")]
        public DateTime DeliveryDate { get; set; } //TODO nullable datetie

        [Required]
        [Display(Name = "Client")]
        public int ClientID { get; set; }

        [Required]
        [Display(Name = "Address")]
        public int AddressID { get; set; }

        [Required]
        [Display(Name = "City")]
        public int CityID { get; set; }

        [Required]
        [Display(Name = "Country")]
        public int CountryID { get; set; }

        [Display(Name = "Offer discount (%)")]
        [DefaultValue(0)]
        [Range(0, 100, ErrorMessage = "Discount must be greater or equal zero and less or equal 100!")]
        public decimal ProductDiscount { get; set; }
    }
}
