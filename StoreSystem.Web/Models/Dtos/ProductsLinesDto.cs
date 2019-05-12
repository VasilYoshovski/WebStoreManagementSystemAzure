using StoreSystem.Services.Dto;
using System.Collections.Generic;

namespace StoreSystem.Web.Models.Dtos
{
    public class ProductsLinesDto
    {
        public int SOId { get; set; }
        public List<ProductIdQuantityDto> Products { get; set; }
    }
}
