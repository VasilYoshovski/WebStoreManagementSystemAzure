using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace StoreSystem.Data.Models
{
    public class Offer
    {
        public int OfferID { get; set; }

        [Required]
        public DateTime ExpiredDate { get; set; }

        [Required]
        public DateTime OfferDate { get; set; }

        [Required]
        public DateTime DeliveryDate { get; set; }

        [Required]
        public int ClientID { get; set; }
        [JsonIgnore]
        //[ForeignKey("ClientID")]
        public Client Client { get; set; }

        public int? SaleID { get; set; }
        [JsonIgnore]
        //[ForeignKey("SaleID")]
        public Sale Sale { get; set; }

        [Required]
        public int AddressID { get; set; }
        [JsonIgnore]
        //[ForeignKey("AddressID")]
        public Address DeliveryAddress { get; set; }

        [Required]
        public int CityID { get; set; }
        [JsonIgnore]
        //[ForeignKey("CityID")]
        public City DeliveryCity { get; set; }

        [Required]
        public int CountryID { get; set; }
        [JsonIgnore]
        //[ForeignKey("CountryID")]
        public Country DeliveryCountry { get; set; }

        public decimal ProductDiscount { get; set; }

        [JsonIgnore]
        public ICollection<ProductOffer> ProductsInOffer { get; set; }

        public override string ToString()
        {
            return $"Offer ID: {SaleID}, created on: {OfferDate.Date:d}, client : {Client.Name}. " +
                $"Products can be delivered on {DeliveryDate:d}.";
        }
    }
}
