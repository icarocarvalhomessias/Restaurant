using Restaurant.API.Models;

namespace Restaurant.API.Services.Interfaces;

public interface IProductService
{
    Task<bool> AddProductAsync(Product product);
    Task<bool> DeleteProductAsync(Product product);
    Task<IEnumerable<Product>> GetAllProductsAsync();
    Task<Product> GetProductByIdAsync(Guid id);
    Task<bool> UpdateProductAsync(Product product);
}