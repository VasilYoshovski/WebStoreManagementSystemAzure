using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace StoreSystem.Data.Models
{
    public class ProductSupplier
    {
        public int ProductID { get; set; }
        [JsonIgnore]
        public Product Product { get; set; }

        public int SupplierID { get; set; }
        [JsonIgnore]
        public Supplier Supplier { get; set; }
    }
}
