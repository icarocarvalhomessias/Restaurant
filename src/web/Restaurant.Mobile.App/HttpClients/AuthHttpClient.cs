using Restaurant.WebApi.Core.Communication;
using Restaurant.WebApi.Core.Data;
using System.Net.Http.Headers;
using System.Text.Json;

namespace Mobile.App.HttpClients
{
    internal class AuthHttpClient : HttpService, IAuthHttpClient
    {
        private readonly HttpClient _httpClient;

        public AuthHttpClient()
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri("http://localhost:5263/api/auth/authenticate")
            };
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<UserResponseLogin> LoginAsync(string email, string password)
        {
            var url = new Uri("http://localhost:5263/api/auth/authenticate");
            var loginData = new UserLogin(email, password);
            var jsonContent = new StringContent(JsonSerializer.Serialize(loginData), System.Text.Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(url, jsonContent);

            try
            {
                if (!TratarErrosResponse(response))
                {
                    return new UserResponseLogin
                    {
                        ResponseResult = await DeserializarObjetoResponse<ResponseResult>(response)
                    };
                }
            }
            catch
            {
                return new UserResponseLogin
                {
                    ResponseResult = await DeserializarObjetoResponse<ResponseResult>(response)
                };
            }

            return await DeserializarObjetoResponse<UserResponseLogin>(response);
        }
    }
}
