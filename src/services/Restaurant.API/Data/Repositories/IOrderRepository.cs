using Restaurant.API.Models;

namespace Restaurant.API.Data.Repositories
{
    public interface IOrderRepository
    {
        Task<bool> AddOrderAsync(Order order);
        Task<bool> DeleteOrderAsync(Order order);
        Task<IEnumerable<Order>> GetAllOrdersWithProductsAsync();
        Task<Order> GetOrderByIdAsync(Guid id);
        Task<bool> UpdateAsync(Order order);
    }

}
