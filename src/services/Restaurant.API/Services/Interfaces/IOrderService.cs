using Restaurant.API.Models;

namespace Restaurant.API.Services.Interfaces;

public interface IOrderService
{
    Task<bool> AddOrderAsync(Order order);
    Task<bool> DeleteOrderAsync(Order order);
    Task<IEnumerable<Order>> GetAllOrdersAsync();
    Task<Order> GetOrderByIdAsync(Guid id);
    Task<bool> UpdateAsync(Order order);
}