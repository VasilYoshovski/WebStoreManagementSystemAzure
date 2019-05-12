using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace StoreSystem.Data.Models
{
    public class Purchase
    {
        public int PurchaseID { get; set; }

        [Required]
        public DateTime PurchaseDate { get; set; }

        [Required]
        public DateTime DeadlineDate { get; set; }

        [Required]
        public DateTime DeliveryDate { get; set; }

        [Required]
        public int SupplierID { get; set; }
        [JsonIgnore]
        public Supplier Supplier { get; set; }

        [Required]
        public int WarehouseID { get; set; }
        [JsonIgnore]
        public Warehouse Warehouse { get; set; }

        [JsonIgnore]
        public ICollection<ProductPurchase> ProductsТоPurchase { get; set; }

        public override string ToString()
        {
            return $"Purchase ID: {PurchaseID}, created on: {PurchaseDate.Date}, deadline date: {DeadlineDate.Date}. " +
                ((DeliveryDate > PurchaseDate) ? $"Purchase is closed on {DeliveryDate}." : "Purchase is still not closed.");
        }

    }
}
