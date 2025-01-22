using System.Text;
using System.Text.Json.Serialization;
using System.Text.Json;
using Restaurant.WebApi.Core.Data;

namespace Mobile.App.HttpClients
{
    public class HttpService
    {
        private readonly JsonSerializerOptions _jsonOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            ReferenceHandler = ReferenceHandler.Preserve,
            Converters = { new JsonStringEnumConverter() },
            MaxDepth = 64
        };

        protected StringContent ObterConteudo(object dado)
        {
            return new StringContent(
                JsonSerializer.Serialize(dado, _jsonOptions),
                Encoding.UTF8,
                "application/json");
        }

        protected async Task<T> DeserializarObjetoResponse<T>(HttpResponseMessage responseMessage)
        {
            return JsonSerializer.Deserialize<T>(await responseMessage.Content.ReadAsStringAsync(), _jsonOptions);
        }

        protected bool TratarErrosResponse(HttpResponseMessage response)
        {
            switch ((int)response.StatusCode)
            {
                case 401:
                case 403:
                case 404:
                case 500:
                    throw new CustomHttpRequestException(response.StatusCode);

                case 400:
                    return false;
            }

            response.EnsureSuccessStatusCode();
            return true;
        }
    }
}
