using Mobile.App.HttpClients;
using Restaurant.Mobile.App.Models;
using Restaurant.Mobile.App.Models.Inputs;
using System.Net.Http.Json;

public interface IOrderService
{
    Task<bool> AddOrderAsync(OrderInput orderInput);
    Task<IEnumerable<OrderModel>> GetAllOrdersAsync();
    Task<OrderModel> GetOrderByIdAsync(Guid id);
}

public class OrderService : IOrderService
{
    private readonly IRestaurantHttpClient _restaurantHttpClient;

    public OrderService(IRestaurantHttpClient restaurantHttp)
    {
        _restaurantHttpClient = restaurantHttp;
    }

    public async Task<IEnumerable<OrderModel>> GetAllOrdersAsync()
    {
        var response = await _restaurantHttpClient.GetAsync("Order");

        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<IEnumerable<OrderModel>>();
    }

    public async Task<bool> AddOrderAsync(OrderInput orderInput)
    {
        var response = await _restaurantHttpClient.PostAsync("Order", orderInput);
        return response.IsSuccessStatusCode;
    }

    public async Task<OrderModel> GetOrderByIdAsync(Guid id)
    {
        var response = await _restaurantHttpClient.GetAsync($"Order/{id}");
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<OrderModel>();
    }
}
