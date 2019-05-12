using System;
using System.Collections.Generic;
using System.Text;

namespace StoreSystem.Services.Dto
{
    public class ProductIdQuantityDto
    {
        public int ProductID { get; set; }
        public Decimal Quantity { get; set; }

        public ProductIdQuantityDto(int productID, decimal quantity)
        {
            this.ProductID = productID;
            this.Quantity = quantity;
        }
    }
}
