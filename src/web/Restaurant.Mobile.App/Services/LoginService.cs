using Mobile.App.HttpClients;

namespace Restaurant.Mobile.App.Services
{
    public interface ILoginService
    {
        Task<bool> LoginAsync(string username, string password);
    }

    public class LoginService : ILoginService
    {

        private readonly IRestaurantHttpClient _restaurantHttpClient;

        public LoginService(IRestaurantHttpClient restaurantHttp)
        {
            _restaurantHttpClient = restaurantHttp;
        }

        public async Task<bool> LoginAsync(string username, string password)
        {
            var response = await _restaurantHttpClient.LoginAsync(username, password);
            return response.ResponseResult.Errors.Mensagens.Count() == 0;
        }
    }
}
