
using Restaurant.WebApi.Core.Data;

namespace Mobile.App.HttpClients
{
    public interface IRestaurantHttpClient
    {
        Task<UserResponseLogin> LoginAsync(string email, string password);

        Task<HttpResponseMessage> GetAsync(string uri);

        Task<HttpResponseMessage> PostAsync(string uri, object data);

        Task<HttpResponseMessage> PutAsync(string uri, object data);

        Task<HttpResponseMessage> DeleteAsync(string uri);
    }
}