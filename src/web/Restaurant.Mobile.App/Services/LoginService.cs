using Mobile.App.HttpClients;

namespace Restaurant.Mobile.App.Services
{
    public interface ILoginService
    {
        Task<bool> LoginAsync(string username, string password);
    }

    public class LoginService : ILoginService
    {

        private readonly IAuthHttpClient _authHttpClient;

        public LoginService(IAuthHttpClient authHttpClient)
        {
            _authHttpClient = authHttpClient;
        }

        public async Task<bool> LoginAsync(string username, string password)
        {
            var response = await _authHttpClient.LoginAsync(username, password);
            return response.ResponseResult.Errors.Mensagens.Count() == 0;
        }
    }
}
