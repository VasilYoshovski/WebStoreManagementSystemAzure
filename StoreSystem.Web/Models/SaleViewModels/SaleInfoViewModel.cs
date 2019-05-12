using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace StoreSystem.Web.Models.SaleViewModels
{
    public class SaleInfoViewModel
    {
        [Display(Name = "Sale number")]
        public int SaleID { get; set; }

        [Required]
        [Display(Name = "Created on")]
        public DateTime OrderDate { get; set; }

        [Required]
        [Display(Name = "Deadline date")]
        public DateTime DeadlineDate { get; set; }

        [Display(Name = "Delivery date")]
        public DateTime DeliveryDate { get; set; } //TODO nullable datetie

        [Display(Name = "Offer number")]
        public int? OfferID { get; set; }

        [Required]
        [Display(Name = "Client")]
        public string ClientName { get; set; }

        [Display(Name = "Address")]
        public string AddressName { get; set; }

        [Display(Name = "City")]
        public string CityName { get; set; }

        [Display(Name = "Country")]
        public string CountryName { get; set; }

        [Display(Name = "Sale discount (%)")]
        public decimal ProductDiscount { get; set; }
    }
}
