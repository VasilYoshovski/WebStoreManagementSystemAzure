using System;
using System.Collections.Generic;
using System.Text;

namespace StoreSystem.Services.Dto
{
    public class ProductQuantityDto
    {
        public string Name { get; set; }
        public Decimal Quantity { get; set; }

        public ProductQuantityDto(string name, decimal quantity)
        {
            this.Name = name ?? throw new ArgumentNullException(nameof(name));
            this.Quantity = quantity;
        }
    }
}
