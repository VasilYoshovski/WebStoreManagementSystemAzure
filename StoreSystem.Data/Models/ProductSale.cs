using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace StoreSystem.Data.Models
{
    public class ProductSale
    {
        public int ProductID { get; set; }
        [JsonIgnore]
        public Product Product { get; set; }

        public int SaleID { get; set; }
        [JsonIgnore]
        public Sale Sale { get; set; }

        public decimal Quantity { get; set; }

        public override string ToString()
        {
            return $"ID: {ProductID}, Name: {Product.Name}, Measure: {Product.Measure}, Quantity: {Quantity}, S. price: {Product.RetailPrice*(1-Sale.ProductDiscount):f2}, Total: {Product.RetailPrice * (1 - Sale.ProductDiscount)*Quantity:f2}";
        }
    }
}
