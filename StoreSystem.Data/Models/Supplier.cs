using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace StoreSystem.Data.Models
{
    public class Supplier
    {
        public int SupplierID { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [MaxLength(9)]
        [MinLength(9)]
        public string UIN { get; set; }

        [Required]
        public bool IsDeleted { get; set; }

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

        [JsonIgnore]
        public ICollection<ProductSupplier> ProductsOfSupplier { get; set; }

        public string StoreUserId { get; set; }
        [JsonIgnore]
        public StoreUser StoreUser { get; set; }

        public override string ToString()
        {
            var phoneNumber = StoreUser?.PhoneNumber == null ? "" : StoreUser.PhoneNumber;
            var email = StoreUser?.Email == null ? "" : StoreUser.Email;
            return $"ID: {SupplierID}, Name: {Name}, UIN: {UIN}, Contact info: {phoneNumber}, {email}".TrimEnd(',');
        }
    }
}
