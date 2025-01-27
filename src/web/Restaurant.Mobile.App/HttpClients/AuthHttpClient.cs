using Restaurant.WebApi.Core.Communication;
using Restaurant.WebApi.Core.Data;
using System.Net.Http.Headers;
using System.Text.Json;

namespace Mobile.App.HttpClients
{
    internal class AuthHttpClient : HttpService, IRestaurantHttpClient
    {
        private readonly HttpClient _httpClient;

        public AuthHttpClient()
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri("http://localhost:5263/api/")
            };
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<HttpResponseMessage> GetAsync(string uri)
        {
            var url = new Uri($"http://localhost:5263/api/{uri}");
            return await _httpClient.GetAsync(url);
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

        public async Task<HttpResponseMessage> PostAsync(string uri, object data)
        {
            var url = new Uri($"http://localhost:5263/api/{uri}");
            var jsonContent = ObterConteudo(data);
            var response = await _httpClient.PostAsync(url, jsonContent);

            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                // Log the error details for debugging
                Console.WriteLine($"Error: {response.StatusCode}, Content: {errorContent}");
            }

            return response;
        }

        public async Task<HttpResponseMessage> PutAsync(string uri, object data)
        {
            var url = new Uri($"http://localhost:5263/api/{uri}");
            var jsonContent = ObterConteudo(data);
            return await _httpClient.PutAsync(url, jsonContent);
        }

        public async Task<HttpResponseMessage> DeleteAsync(string uri)
        {
            var url = new Uri($"http://localhost:5263/api/{uri}");
            return await _httpClient.DeleteAsync(url);
        }
    }
}
