using Microsoft.AspNetCore.Mvc;
using Restaurant.API.Controllers.Inputs;
using Restaurant.API.Models;
using Restaurant.API.Services.Interfaces;
using Restaurant.WebApi.Core.Controller;

namespace Restaurant.API.Controllers;

[Route("api/[controller]")]
public class OrderController : MainController
{
    private readonly IOrderService _orderService;

    public OrderController(IOrderService orderService)
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
        var order = new Order(orderInput.ClientName, orderInput.ClientAddress, orderInput.ClientPhone, orderInput.ProductIds);

        var result = await _orderService.AddOrderAsync(order);
        if (!result)
        {
            return BadRequest();
        }
        return CreatedAtAction(nameof(GetOrderById), new { id = order.Id }, order);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateOrder(Order order)
    {
        if (await _orderService.UpdateAsync(order))
        {
            return Ok();
        }
        return BadRequest();
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteOrder(Order order)
    {
        if (await _orderService.DeleteOrderAsync(order))
        {
            return Ok();
        }
        return BadRequest();
    }
}
