using Microsoft.AspNetCore.Mvc;
using Restaurant.API.Controllers.Inputs;
using Restaurant.API.Models;
using Restaurant.API.Services.Interfaces;
using Restaurant.WebApi.Core.Controller;

namespace Restaurant.API.Controllers;

[Route("api/[controller]")]
public class OrdersController : MainController
{
    private readonly IOrderService _orderService;

    public OrdersController(IOrderService orderService)
    {
        _orderService = orderService;
    }

    [HttpGet]
    public async Task<IEnumerable<Order>> GetAllOrders()
    {
        return await _orderService.GetAllOrdersAsync();
    }

    [HttpGet("{id}")]
    public async Task<Order> GetOrderById(Guid id)
    {
        return await _orderService.GetOrderByIdAsync(id);
    }

    [HttpPost]
    public async Task<ActionResult> AddOrder(OrderInput orderInput)
    {
        var products = orderInput.Products.ToDictionary(x => x.ProductId, x => x.Quantity);
        var result = await _orderService.AddOrderAsync(
            orderInput.ClientName,
            orderInput.ClientAddress,
            orderInput.ClientPhone,
            products,
            orderInput.UsuarioId
        );

        if (!result)
        {
            return BadRequest();
        }

        return Ok();
    }

    [HttpPut("{orderId}")]
    public async Task<IActionResult> UpdateOrder(Guid orderId, OrderInput orderInput)
    {
        var products = orderInput.Products.ToDictionary(x => x.ProductId, x => x.Quantity);
        if (await _orderService.UpdateOrderAsync(orderId, orderInput.ClientName, orderInput.ClientAddress, orderInput.ClientPhone, products, orderInput.UsuarioId))
        {
            return Ok();
        }
        return BadRequest();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteOrder(Guid id)
    {
        var order = await _orderService.GetOrderByIdAsync(id);
        if (order == null)
        {
            return NotFound();
        }

        if (await _orderService.DeleteOrderAsync(order))
        {
            return Ok();
        }
        return BadRequest();
    }
}
