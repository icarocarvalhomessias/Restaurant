
using Restaurant.WebApi.Core.Data;

namespace Mobile.App.HttpClients
{
    public interface IAuthHttpClient
    {
        Task<UserResponseLogin> LoginAsync(string email, string password);
    }
}