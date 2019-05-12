using System.ComponentModel.DataAnnotations;

namespace StoreSystem.Web.Models.ProductViewModels
{
    public class ProductLineInfoViewModel
    {
        [Display(Name = "Product num")]
        public int ProductID { get; set; }

        [Display(Name = "Product name")]
        public string Name { get; set; }

        public string Measure { get; set; }

        public decimal Quantity { get; set; }

        public decimal Price { get; set; }

        public decimal Total { get; set; }
    }
}
