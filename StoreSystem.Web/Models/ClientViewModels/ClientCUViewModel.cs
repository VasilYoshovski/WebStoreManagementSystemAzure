using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StoreSystem.Web.Models.ClientViewModels
{
    public class ClientCUViewModel
    {
        //"ClientID,Name,UIN,AddressID,CityID,CountryID,StoreUserId"
        [Required]
        public int ClientID { get; set; }

        [Required]
        [Display(Name = "Client name")]
        public string Name { get; set; }

        [MinLength(9)]
        [MaxLength(9)]
        public string UIN { get; set; }

        [Required]
        [Display(Name = "Client address")]
        public int AddressId { get; set; }

        [Required]
        [Display(Name = "Client city")]
        public int CityId { get; set; }

        [Required]
        [Display(Name = "Client country")]
        public int CountryId { get; set; }

        [Required]
        public string StoreUserId { get; set; }
    }
}
