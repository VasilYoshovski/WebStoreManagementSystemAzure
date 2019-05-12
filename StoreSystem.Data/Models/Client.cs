using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace StoreSystem.Data.Models
{
    public class Client
    {
        public int ClientID { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string UIN { get; set; }

        [Required]
        public bool IsDeleted { get; set; }

        [Required]
        public int AddressID { get; set; }
        [JsonIgnore]
        //[ForeignKey("AddressID")]
        public Address Address { get; set; }

        [Required]
        public int CityID { get; set; }
        [JsonIgnore]
        //[ForeignKey("CityID")]
        public City City { get; set; }

        [Required]
        public int CountryID { get; set; }
        [JsonIgnore]
        //[ForeignKey("CountryID")]
        public Country Country { get; set; }

        [JsonIgnore]
        public ICollection<Offer> Offers { get; set; }

        [JsonIgnore]
        public ICollection<Sale> Sales { get; set; }

        public string StoreUserId { get; set; }
        [JsonIgnore]
        public StoreUser StoreUser { get; set; }

        public override string ToString()
        {
            return $"ID: {ClientID}, Name: {Name}, UIN: {UIN}, Contact info: {StoreUser.PhoneNumber}, {StoreUser.Email}".TrimEnd(',');
        }
    }
}
