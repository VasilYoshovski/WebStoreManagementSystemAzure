using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StoreSystem.Services.Dto;

namespace StoreSystem.Services
{
    public interface IProductSaleService
    {
        Task<List<IGrouping<int, ProductTotalDto>>> GetProductsTotalSaleQuantityAsync(string alies = "*");
    }
}