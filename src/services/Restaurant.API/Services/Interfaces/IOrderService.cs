using Restaurant.API.Models;

namespace Restaurant.API.Services.Interfaces;

public interface IOrderService
{
    Task<bool> AddOrderAsync(string clientName, string clientAddress, string clientPhone, Dictionary<Guid, int> productQuantities, Guid? usuarioId);
    Task<bool> DeleteOrderAsync(Order order);
    Task<IEnumerable<Order>> GetAllOrdersAsync();
    Task<Order> GetOrderByIdAsync(Guid id);
    Task<bool> UpdateOrderAsync(Guid orderId, string? clientName, string? clientAddress, string? clientPhone, Dictionary<Guid, int> productQuantities, Guid? usuarioId);
}
