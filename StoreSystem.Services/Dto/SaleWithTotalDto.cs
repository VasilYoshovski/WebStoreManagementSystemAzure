using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace StoreSystem.Services.Dto
{
    public class SaleWithTotalDto
    {
        [Display(Name = "Sale number")]
        public int SaleID { get; set; }

        [Display(Name = "Client")]
        public string ClientName { get; set; }

        public decimal Discount { get; set; }

        [Display(Name = "Sale date")]
        public DateTime CreatedOn { get; set; }

        [Display(Name = "Deadline date")]
        public DateTime DeadlineDate { get; set; }

        public decimal Total { get; set; }
    }
}
