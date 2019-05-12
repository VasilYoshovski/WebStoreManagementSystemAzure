using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace StoreSystem.Data.Models
{
    // Add profile data for application users by adding properties to the StoreUser class
    public class StoreUser : IdentityUser
    {
        public int? SupplierId { get; set; }
        public Supplier Supplier { get; set; }

        public int? ClientId { get; set; }
        public Client Client { get; set; }

        public int? EmployeeId { get; set; }
        public Employee Employee { get; set; }
    }
}
