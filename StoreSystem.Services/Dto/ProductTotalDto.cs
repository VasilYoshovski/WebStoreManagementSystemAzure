using System;
using System.Collections.Generic;
using System.Text;

namespace StoreSystem.Services.Dto
{
    public class ProductTotalDto
    {
        public string Product { get; set; }
        public int ProductID { get; set; }
        public decimal Quantity { get; set; }
        public decimal Total { get; set; }

        public override bool Equals(object obj)
        {
            return this.Product.Equals(((ProductTotalDto)obj).Product) &&
                this.ProductID.Equals(((ProductTotalDto)obj).ProductID) &&
                this.Quantity.Equals(((ProductTotalDto)obj).Quantity) &&
                this.Total.Equals(((ProductTotalDto)obj).Total);
        }
        public bool Equals(ProductTotalDto x, ProductTotalDto y)
        {
            return x.Product.Equals(y.Product) &&
                x.ProductID.Equals(y.ProductID) &&
                x.Quantity.Equals(y.Quantity) &&
                x.Total.Equals(y.Total);
        }
    }
}
