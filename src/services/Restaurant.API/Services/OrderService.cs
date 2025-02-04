using Restaurant.API.Data.Repositories;
using Restaurant.API.Models;
using Restaurant.API.Services.Interfaces;

namespace Restaurant.API.Services;

public class OrderService : IOrderService
{
    private readonly IOrderRepository _orderRepository;
    private readonly IProductService _productService;
    private readonly IUsuarioService _usuarioService;

    public OrderService(IOrderRepository orderRepository, IProductService productService, IUsuarioService usuarioService)
    {
        _orderRepository = orderRepository;
        _productService = productService;
        _usuarioService = usuarioService;
    }

    public async Task<IEnumerable<Order>> GetAllOrdersAsync()
    {
        return await _orderRepository.GetAllOrdersWithProductsAsync();
    }

    public async Task<Order> GetOrderByIdAsync(Guid id)
    {
        return await _orderRepository.GetOrderByIdAsync(id);
    }

    public async Task<bool> AddOrderAsync(string clientName, string clientAddress, string clientPhone, Dictionary<Guid, int> productQuantities, Guid? usuarioId)
    {
        var order = Order.GenerateOrder(clientName, clientAddress, clientPhone, productQuantities);

        ValidateOrder(order);

        // Save the order first to get the OrderId
        var result = await _orderRepository.AddOrderAsync(order);
        if (!result)
        {
            return false;
        }

        // Now add the products to the order
        await ValidateAndAddProducts(order, productQuantities);

        return await _orderRepository.UpdateAsync(order);
    }

    public async Task<bool> UpdateOrderAsync(Guid orderId, string? clientName, string? clientAddress, string? clientPhone, Dictionary<Guid, int> productQuantities, Guid? usuarioId)
    {
        var existingOrder = await _orderRepository.GetOrderByIdAsync(orderId);
        if (existingOrder == null)
        {
            throw new ArgumentException("Order not found.");
        }

        if (!string.IsNullOrWhiteSpace(clientName))
        {
            existingOrder.ClientName = clientName;
        }

        if (!string.IsNullOrWhiteSpace(clientAddress))
        {
            existingOrder.ClientAddress = clientAddress;
        }

        if (!string.IsNullOrWhiteSpace(clientPhone))
        {
            existingOrder.ClientPhone = clientPhone;
        }

        if (usuarioId.HasValue && usuarioId != Guid.Empty)
        {
            var usuario = await _usuarioService.GetById(usuarioId.Value);
            if (usuario == null)
            {
                throw new ArgumentException("Usuario not found.");
            }
            existingOrder.UsuarioId = usuarioId.Value;
        }
        else
        {
            existingOrder.UsuarioId = null;
        }

        ValidateOrder(existingOrder);
        await ValidateAndAddProducts(existingOrder, productQuantities);

        return await _orderRepository.UpdateAsync(existingOrder);
    }

    public async Task<bool> DeleteOrderAsync(Order order)
    {
        return await _orderRepository.DeleteOrderAsync(order);
    }


    private void ValidateOrder(Order order)
    {
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
    }

    private async Task ValidateAndAddProducts(Order order, Dictionary<Guid, int> productQuantities)
    {
        order.OrderProducts.Clear();
        foreach (var productQuantity in productQuantities)
        {
            var productId = productQuantity.Key;
            var quantity = productQuantity.Value;

            var product = await _productService.GetProductByIdAsync(productId);
            if (product == null || !product.Active)
            {
                throw new ArgumentException($"Product with ID {productId} does not exist or is not active.");
            }

            var orderProduct = order.OrderProducts.FirstOrDefault(op => op.ProductId == productId);
            if (orderProduct == null)
            {
                orderProduct = new OrderProduct
                {
                    OrderId = order.Id,
                    ProductId = productId,
                    Quantity = quantity
                };
                order.OrderProducts.Add(orderProduct);
            }
            else
            {
                orderProduct.Quantity += quantity;
            }
        }
    }

}
