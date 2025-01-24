using Mobile.App.HttpClients;
using Restaurant.Mobile.App.Models;
using System.Net.Http.Json;

public interface IOrderService
{
    Task<IEnumerable<OrderModel>> GetAllOrdersAsync();
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
}
