using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace StoreSystem.Data.Models
{
    public class Address
    {
        public int AddressID { get; set; }

        [Required]
        public string Name { get; set; }

        // Warehouse will not be accessed by Address entity
        // public int? WarehouseID { get; set; }
        // public Warehouse Warehouse { get; set; }

        [JsonIgnore]
        public ICollection<Client> Clients { get; set; }

        [JsonIgnore]
        public ICollection<Offer> Offers { get; set; }

        [JsonIgnore]
        public ICollection<Supplier> Suppliers { get; set; }

        [JsonIgnore]
        public ICollection<Sale> Sales { get; set; }
    }
}
