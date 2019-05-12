using System.ComponentModel.DataAnnotations;

namespace StoreSystem.Web.Models.ProductViewModels
{
    //"ProductID,Name,Measure,Quantity,BuyPrice,RetailPrice"
    public class ProductViewModel
    {
        [Required]
        public int ProductID { get; set; }

        [Required]
        [MinLength(3)]
        [Display(Name = "Product name")]
        public string Name { get; set; }

        [Required]
        public string Measure { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Product quantity must be greater than zero!")]
        public decimal Quantity { get; set; }

        [Required]
        [Display(Name = "Buyed price")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Product buyed price must be greater than zero!")]
        public decimal BuyPrice { get; set; }

        [Required]
        [Display(Name = "Retail price")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Product retail price must be greater than zero!")]
        public decimal RetailPrice { get; set; }

        public bool CanEdit { get; set; }
    }
}
