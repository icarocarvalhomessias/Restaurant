
using Restaurant.WebApi.Core.Data;

namespace Mobile.App.HttpClients
{
    public interface IRestaurantHttpClient
    {
        Task<UserResponseLogin> LoginAsync(string email, string password);

        //create getAsync
        Task<HttpResponseMessage> GetAsync(string uri);
    }
}