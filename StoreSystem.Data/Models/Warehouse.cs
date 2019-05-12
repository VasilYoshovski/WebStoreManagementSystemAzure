using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace StoreSystem.Data.Models
{
    public class Warehouse
    {
        public int WarehouseID { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public int AddressID { get; set; }
        [JsonIgnore]
        public Address Address { get; set; }

        [Required]
        public int CityID { get; set; }
        [JsonIgnore]
        public City City { get; set; }

        [Required]
        public int CountryID { get; set; }
        [JsonIgnore]
        public Country Country { get; set; }

        [JsonIgnore]
        public ICollection<Purchase> Purchases { get; set; }
    }
}
