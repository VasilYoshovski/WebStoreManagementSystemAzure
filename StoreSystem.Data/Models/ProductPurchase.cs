using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace StoreSystem.Data.Models
{
    public class ProductPurchase
    {
        [Required]
        public int ProductID { get; set; }
        [JsonIgnore]
        //[ForeignKey("ProductID")]
        public Product Product { get; set; }

        [Required]
        public int PurchaseID { get; set; }
        [JsonIgnore]
        //[ForeignKey("PurchaseID")]
        public Purchase Purchase { get; set; }

        [Required]
        public decimal ProductQty { get; set; }

        [Required]
        public decimal ProductPrice { get; set; }
    }
}
