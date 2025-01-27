using Restaurant.API.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface IProductRepository
{
    Task<IEnumerable<Product>> GetAllProductsAsync();
    Task<Product> GetProductByIdAsync(Guid id);
    Task<bool> AddProductAsync(Product product);
    Task<bool> UpdateProductAsync(Product product);
}
