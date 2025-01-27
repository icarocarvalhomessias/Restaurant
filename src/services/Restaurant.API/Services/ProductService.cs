using Restaurant.API.Models;
using Restaurant.API.Services.Interfaces;

namespace Restaurant.API.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;

    public ProductService(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<IEnumerable<Product>> GetAllProductsAsync()
    {
        return await _productRepository.GetAllProductsAsync();
    }

    public async Task<Product> GetProductByIdAsync(Guid id)
    {
        return await _productRepository.GetProductByIdAsync(id);
    }

    public async Task<bool> AddProductAsync(Product product)
    {
        return await _productRepository.AddProductAsync(product);
    }

    public async Task<bool> UpdateProductAsync(Product product)
    {
        return await _productRepository.UpdateProductAsync(product);
    }

    public async Task<bool> DeleteProductAsync(Product product)
    {
        product.Deactivate();
        return await _productRepository.UpdateProductAsync(product);
    }
}
