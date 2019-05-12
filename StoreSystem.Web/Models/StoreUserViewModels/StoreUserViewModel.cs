using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StoreSystem.Web.Models.StoreUserViewModels
{
    public class StoreUserViewModel
    {
        [Required]
        [MinLength(1)]
        public string UserID { get; set; }

        [Required]
        [MinLength(1)]
        public string UserName;

        [Required]
        [MinLength(5)]
        public string Email { get; set; }

        [Required]
        public string Role { get; set; }

        [Required]
        public bool IsProtected;

        public string Phone { get; set; }
    }
}
