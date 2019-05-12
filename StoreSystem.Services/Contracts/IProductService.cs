using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StoreSystem.Data.Models;
using StoreSystem.Services.Dto;

namespace StoreSystem.Services
{
    public interface IProductService
    {
        Task<Product> CreateProductAsync(string name, string measure, decimal quantity, decimal buyPrice, decimal retailPrice, bool save = true);
        Task<Product> FindProductByNameAsync(string productName);
        Task<Product> FindProductByIdAsync(int productId);
        Task<bool> ProductExistsAsync(int productId);
        Task<IReadOnlyCollection<Product>> GetAllProductsAsync(int from, int to, string contains = "*", string sortOrder = "Name", bool haveQuantity = false);
        Task<Product> GetProductByNameAsync(string productName);
        Task<bool> UpdateProductDetailAsync(int productID, string name, string measure, decimal? quantity, decimal? buyPrice, decimal? retailPrice);
        Task<int> GetProductsCountAsync(int from, int to, string contains = "*", string sortOrder = "Name", bool haveQuantity = false);
        Task<IReadOnlyCollection<ProductIdNameDto>> GetAllProductsIdName(int from, int to, string contains = "*", string sortOrder = "Name", bool haveQuantity = false);
        Task<bool> DeleteProduct(int productId);

    }
}