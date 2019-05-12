using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace StoreSystem.Data.Models
{
    public class Product
    {
        public int ProductID { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        public string Measure { get; set; }

        [Required]
        public decimal Quantity { get; set; }

        [Required]
        public decimal BuyPrice { get; set; }

        [Required]
        public decimal RetailPrice { get; set; }

        [Required]
        public bool IsDeleted { get; set; }

        [JsonIgnore]
        public ICollection<ProductSupplier> ProductSuppliers { get; set; }

        [JsonIgnore]
        public ICollection<ProductSale> ProductSales { get; set; }

        [JsonIgnore]
        public ICollection<ProductOffer> ProductOffers { get; set; }

        [JsonIgnore]
        public ICollection<ProductPurchase> ProductPurchases { get; set; }

        public override string ToString()
        {
            return $"ID: {ProductID}, Name: {Name}, Measure: {Measure}, Quantity: {Quantity}, Price buy/retail: {BuyPrice:f2}/{RetailPrice:f2}";
        }
    }
}
