using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StoreSystem.Web.Models.WarehouseViewModels
{
    public class WarehouseViewModel
    {
        [Required]
        public int WarehouseID { get; set; }

        [Required]
        [MinLength(3)]
        [Display(Name = "Warehouse name")]
        public string Name { get; set; }

        [Required]
        public int AddressID { get; set; }

        [Required]
        [MinLength(3)]
        [Display(Name = "Address name")]
        public string AddressText { get; set; }

        [Required]
        public int CityID { get; set; }

        [Required]
        [MinLength(3)]
        [Display(Name = "City name")]
        public string CityName { get; set; }

        [Required]
        public int CountryID { get; set; }

        [Required]
        [MinLength(3)]
        [Display(Name = "Country name")]
        public string CountryName { get; set; }
    }
}
