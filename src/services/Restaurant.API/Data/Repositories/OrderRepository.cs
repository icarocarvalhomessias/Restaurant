using Microsoft.EntityFrameworkCore;
using Restaurant.API.Data;
using Restaurant.API.Data.Repositories;
using Restaurant.API.Models;

public class OrderRepository : IOrderRepository
{
    private readonly ApplicationDbContext _context;

    public OrderRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Order>> GetAllOrdersAsync()
    {
        return await _context.Orders.ToListAsync();
    }

    public async Task<Order> GetOrderByIdAsync(Guid id)
    {
        return await _context.Orders.FindAsync(id);
    }

    public async Task<bool> AddOrderAsync(Order order)
    {
        _context.Orders.Add(order);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> UpdateAsync(Order order)
    {
        _context.Orders.Update(order);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> DeleteOrderAsync(Order order)
    {
        _context.Orders.Remove(order);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<IEnumerable<Order>> GetAllOrdersWithProductsAsync()
    {
        return await _context.Orders
            .Include(o => o.Products)
            .ToListAsync();
    }
}
