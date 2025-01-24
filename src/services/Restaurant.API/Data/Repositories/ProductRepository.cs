using Microsoft.EntityFrameworkCore;
using Restaurant.API.Data;
using Restaurant.API.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

public class ProductRepository : IProductRepository
{
    private readonly ApplicationDbContext _context;

    public ProductRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Product>> GetAllProductsAsync()
    {
        return await _context.Products.AsNoTracking().ToListAsync();
    }

    public async Task<Product> GetProductByIdAsync(Guid id)
    {
        return await _context.Products.FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<bool> AddProductAsync(Product product)
    {
        await _context.Products.AddAsync(product);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> UpdateProductAsync(Product product)
    {
        _context.Products.Update(product);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> DeleteProductAsync(Product product)
    {
        _context.Products.Remove(product);
        return await _context.SaveChangesAsync() > 0;
    }
}
