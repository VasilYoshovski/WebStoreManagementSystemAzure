using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace StoreSystem.Data.Models
{
    public class Sale
    {
        public int SaleID { get; set; }

        [Required]
        public DateTime OrderDate { get; set; }

        [Required]
        public DateTime DeadlineDate { get; set; }

        public DateTime DeliveryDate { get; set; } //TODO nullable datetie

        public int? OfferID { get; set; }
        [JsonIgnore]
        public Offer Offer { get; set; }

        [Required]
        public int ClientID { get; set; }
        [JsonIgnore]
        public Client Client { get; set; }

        [Required]
        public int AddressID { get; set; }
        [JsonIgnore]
        public Address DeliveryAddress { get; set; }

        [Required]
        public int CityID { get; set; }
        [JsonIgnore]
        public City DeliveryCity { get; set; }

        [Required]
        public int CountryID { get; set; }
        [JsonIgnore]
        public Country DeliveryCountry { get; set; }

        [JsonIgnore]
        public ICollection<ProductSale> ProductsInSale { get; set; }

        public decimal ProductDiscount { get; set; }

        public override string ToString()
        {
            return $"Sale ID: {SaleID}, created on: {OrderDate.Date:d}, deadline date: {DeadlineDate.Date:d}. "+
                ((DeliveryDate>OrderDate)?$"Sale is closed on {DeliveryDate:d}.":"Sale is still not closed.") ;
        }
    }
}
