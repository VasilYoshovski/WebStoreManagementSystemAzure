using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StoreSystem.Web.Models.PurchaseViewModels
{
    public class PurchaseViewModel
    {
        [Required]
        public int PurchaseID { get; set; }

        [Required]
        public DateTime PurchaseDate { get; set; }

        [Required]
        public DateTime DeadlineDate { get; set; }

        [Required]
        public DateTime DeliveryDate { get; set; }

        [Required]
        [MinLength(3)]
        [Display(Name = "Supplier name")]
        public string SupplierName { get; set; }

        [Required]
        public int SupplierID { get; set; }

        //TODO
        //[Required]
        //public List<string> products { get; set; }
        //public List<ProductViewModel> products { get; set; }

        //public override string ToString()
        //{
        //    return $"Purchase ID: {PurchaseID}, created on: {PurchaseDate.Date}, deadline date: {DeadlineDate.Date}. " +
        //        ((DeliveryDate > PurchaseDate) ? $"Purchase is closed on {DeliveryDate}." : "Purchase is still not closed.");
        //}
    }
}
