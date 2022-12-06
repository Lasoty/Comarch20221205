using Restauracja.Common.Model;
using Restauracja.Web.Models;
using Restauracja.Web.Services.Interfaces;
using System.Net.Http;
using System.Text;
using System.Text.Json;

namespace Restauracja.Web.Services
{
    public class BaseService : IBaseService
    {
        private readonly IHttpClientFactory httpClient;
        private readonly JsonSerializerOptions jsonOptions = new(JsonSerializerDefaults.Web);

        public BaseService(IHttpClientFactory httpClient)
        {
            this.httpClient = httpClient;
        }

        public static string ProductApiBase { get; set; }

        public async Task<T> SendAsync<T>(ApiRequest apiRequest) where T : Result
        {
            try
            {
                HttpClient client = httpClient.CreateClient("RestauracjaApi");
                HttpRequestMessage message = new();
                message.Headers.Add("Accept", "application/json");

                //request token

                message.RequestUri = new Uri(apiRequest.Url);
                client.DefaultRequestHeaders.Clear();

                if (apiRequest.Data != null)
                {
                    message.Content = new StringContent(JsonSerializer.Serialize(apiRequest.Data), 
                        Encoding.UTF8, "application/json");
                }

                message.Method = apiRequest.ApiType switch
                {
                    ApiType.POST => HttpMethod.Post,
                    ApiType.PUT => HttpMethod.Put,
                    ApiType.DELETE => HttpMethod.Delete,
                    _ => HttpMethod.Get,
                };

                HttpResponseMessage apiResponse = await client.SendAsync(message);

                string apiContent = await apiResponse.Content.ReadAsStringAsync();
                T? apiResponseDto = JsonSerializer.Deserialize<T>(apiContent, jsonOptions);
                return apiResponseDto;
            }
            catch (Exception ex)
            {
                Result dto = Result.Fail(ex.Message);

                string res = JsonSerializer.Serialize(dto);
                T? apiResponseDto = JsonSerializer.Deserialize<T>(res, jsonOptions);
                return apiResponseDto;
            }
        }
    }
}
