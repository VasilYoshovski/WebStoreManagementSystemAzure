using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.Models
{
    public class ProductSale
    {
        public int ProductID { get; set; }
        public Product Product { get; set; }

        public int SaleID { get; set; }
        public Sale Sale { get; set; }

        public decimal ProductDiscount { get; set; }

    }
}
