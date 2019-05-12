using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace StoreSystem.Data.Models
{
    public class ProductOffer
    {
        public int ProductID { get; set; }
        [JsonIgnore]
        public Product Product { get; set; }

        public int OfferID { get; set; }
        [JsonIgnore]
        public Offer Offer { get; set; }

        public decimal Quantity { get; set; }


        public override string ToString()
        {
            return $"ID: {ProductID}, Name: {Product.Name}, Measure: {Product.Measure}, Quantity: {Quantity}, S. price: {Product.RetailPrice * (1 - Offer.ProductDiscount):f2}, Total: {Product.RetailPrice * (1 - Offer.ProductDiscount) * Quantity:f2}";
        }
    }
}
