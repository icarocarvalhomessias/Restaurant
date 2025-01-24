using Restaurant.API.Data.Repositories;
using Restaurant.API.Models;
using Restaurant.API.Services.Interfaces;

namespace Restaurant.API.Services;

public class OrderService : IOrderService
{
    private readonly IOrderRepository _orderRepository;
    private readonly IProductService _productService;

    public OrderService(IOrderRepository orderRepository, IProductService productService)
    {
        _orderRepository = orderRepository;
        _productService = productService;
    }

    public async Task<IEnumerable<Order>> GetAllOrdersAsync()
    {
        return await _orderRepository.GetAllOrdersWithProductsAsync();
    }

    public async Task<Order> GetOrderByIdAsync(Guid id)
    {
        return await _orderRepository.GetOrderByIdAsync(id);
    }

    public async Task<bool> AddOrderAsync(Order order)
    {
        // Validate client details
        if (string.IsNullOrWhiteSpace(order.ClientName))
        {
            throw new ArgumentException("Client name is required.");
        }

        if (string.IsNullOrWhiteSpace(order.ClientAddress))
        {
            throw new ArgumentException("Client address is required.");
        }

        if (string.IsNullOrWhiteSpace(order.ClientPhone))
        {
            throw new ArgumentException("Client phone is required.");
        }

        // Validate product IDs
        if (order.ProductIds == null || !order.ProductIds.Any())
        {
            throw new ArgumentException("At least one product ID is required.");
        }
        order.Products = new List<Product>();

        foreach (var productId in order.ProductIds)
        {
            var product = await _productService.GetProductByIdAsync(productId);
            if (product == null || !product.Active)
            {
                throw new ArgumentException($"Product with ID {productId} does not exist or is not active.");
            }

            order.TotalValue += product.Price;
            order.Products.Add(product);
        }

        // Add the order
        return await _orderRepository.AddOrderAsync(order);
    }

    public async Task<bool> UpdateAsync(Order order)
    {
        return await _orderRepository.UpdateAsync(order);
    }

    public async Task<bool> DeleteOrderAsync(Order order)
    {
        return await _orderRepository.DeleteOrderAsync(order);
    }
}
