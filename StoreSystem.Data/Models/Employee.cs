using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace StoreSystem.Data.Models
{
    public class Employee
    {
        public int EmployeeId { get; set; }

        public string JobPosition { get; set; }

        [Required]
        public bool IsDeleted { get; set; }

        public string StoreUserId { get; set; }
        [JsonIgnore]
        public StoreUser StoreUser { get; set; }

    }
}
