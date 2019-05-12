using System.ComponentModel.DataAnnotations;

namespace StoreSystem.Web.Models.ClientViewModels
{
    public class ClientInfoViewModel
    {
        //"ClientID,Name,UIN,AddressID,CityID,CountryID,StoreUserId"

        [Display(Name = "Client num")]
        public int ClientID { get; set; }

        [Display(Name = "Client name")]
        public string Name { get; set; }

        [Display(Name = "Client UIN")]
        public string UIN { get; set; }

        [Display(Name = "Client address")]
        public string AddressName { get; set; }

        [Display(Name = "Client city")]
        public string CityName { get; set; }

        [Display(Name = "Client country")]
        public string CountryName { get; set; }

        [Display(Name = "Client username")]
        public string StoreUsername { get; set; }

        public string StoreUserId { get; set; }
    }
}
