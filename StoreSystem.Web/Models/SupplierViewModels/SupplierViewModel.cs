using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StoreSystem.Web.Models.SupplierViewModels
{
    public class SupplierViewModel
    {
        [Required]
        public int SupplierID { get; set; }

        [Required]
        [MinLength(3)]
        [Display(Name = "Supplier name")]
        public string Name { get; set; }

        [Required]
        [MaxLength(9)]
        [MinLength(9)]
        public string UIN { get; set; }

        [Required]
        public bool IsDeleted;

        [Required]
        public int AddressID { get; set; }
        //[Required]
        //[MinLength(3)]
        //[Display(Name = "Address")]
        //public string AddressText { get; set; }

        [Required]
        public int CityID { get; set; }
        //[Required]
        //[MinLength(3)]
        //[Display(Name = "City")]
        //public string CityName { get; set; }

        [Required]
        public int CountryID { get; set; }
        //[Required]
        //[MinLength(3)]
        //[Display(Name = "Country")]
        //public string CountryName { get; set; }

        [Required(ErrorMessage = "First create user with role Visitor and then create supplier assigned to the Visitor user")]
        public string MicrosoftUserID { get; set; }

        //public ICollection<Purchase> Purchases { get; set; }

        //public ICollection<ProductSupplier> ProductsOfSupplier { get; set; }

        //[Required]
        //public string StoreUserId { get; set; }
        //public StoreUser StoreUser { get; set; }
    }
}
