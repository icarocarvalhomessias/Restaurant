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
        return await _context.Orders.Include(o => o.OrderProducts)
                .ThenInclude(op => op.Product).ToListAsync();
    }

    public async Task<Order> GetOrderByIdAsync(Guid id)
    {
        return await _context.Orders
            .Include(o => o.OrderProducts)
                .ThenInclude(op => op.Product)
            .FirstOrDefaultAsync(o => o.Id == id);
    }

    public async Task<bool> AddOrderAsync(Order order)
    {
        // Ensure OrderId is set for each OrderProduct
        foreach (var orderProduct in order.OrderProducts)
        {
            orderProduct.OrderId = order.Id;
        }

        _context.Orders.Add(order);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> UpdateAsync(Order order)
    {
        // Get the existing order from the database
        var existingOrder = await _context.Orders
            .Include(o => o.OrderProducts)
            .FirstOrDefaultAsync(o => o.Id == order.Id);

        if (existingOrder == null)
        {
            throw new ArgumentException("Order not found.");
        }

        // Update the order details
        _context.Entry(existingOrder).CurrentValues.SetValues(order);

        // Update the order products
        foreach (var orderProduct in order.OrderProducts)
        {
            orderProduct.OrderId = order.Id; // Ensure OrderId is set

            var existingOrderProduct = existingOrder.OrderProducts
                .FirstOrDefault(op => op.ProductId == orderProduct.ProductId);

            if (existingOrderProduct == null)
            {
                // Ensure the product is attached to the context
                var product = await _context.Products.FindAsync(orderProduct.ProductId);
                if (product == null)
                {
                    throw new ArgumentException($"Product with ID {orderProduct.ProductId} not found.");
                }
                orderProduct.Product = product;

                // Add new product
                existingOrder.OrderProducts.Add(orderProduct);
            }
            else
            {
                // Update existing product quantity
                existingOrderProduct.Quantity = orderProduct.Quantity;
            }
        }

        // Remove products that are no longer in the order
        foreach (var existingOrderProduct in existingOrder.OrderProducts.ToList())
        {
            if (!order.OrderProducts.Any(op => op.ProductId == existingOrderProduct.ProductId))
            {
                existingOrder.OrderProducts.Remove(existingOrderProduct);
            }
        }

        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> DeleteOrderAsync(Order order)
    {
        // Remove OrderProducts associated with the Order
        var orderProducts = _context.OrderProducts.Where(op => op.OrderId == order.Id);
        _context.OrderProducts.RemoveRange(orderProducts);

        _context.Orders.Remove(order);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<IEnumerable<Order>> GetAllOrdersWithProductsAsync()
    {
        return await _context.Orders
            .Include(o => o.OrderProducts)
                .ThenInclude(op => op.Product)
            .ToListAsync();
    }
}
