using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataLayer.Models
{
    public class Product
    {
        public int ProductID { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Measure { get; set; }
        [Required]
        public decimal Quantity { get; set; }
        [Required]
        public decimal BuyPrice { get; set; }
        [Required]
        public decimal RetailPrice { get; set; }

        public int SupplierID { get; set; }
        public Supplier Supplier { get; set; }

        public ICollection<ProductSale> ProductSales { get; set; }

        public ICollection<ProductOffer> ProductOffers { get; set; }
    }
}
