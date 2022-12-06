using Restauracja.Web.Models;
using Restauracja.Web.Services.Interfaces;
using System.Text;
using System.Text.Json;
using IdentityModel.Client;
using static IdentityModel.OidcConstants;
using Restauracja.Common.Model;

namespace Restauracja.Web.Services;

public class BaseService : IBaseService
{
    private readonly JsonSerializerOptions jsonOptions = new(JsonSerializerDefaults.Web);
    public Result ResponseModel { get; set; }
    public IHttpClientFactory httpClient { get; set; }

    public static string CouponAPIBase { get; set; }
    public static string ProductAPIBase { get; set; }
    public static string ShoppingCartAPIBase { get; set; }

    public BaseService(IHttpClientFactory httpClient)
    {
        this.ResponseModel = new Result();
        this.httpClient = httpClient;
    }

    public async Task<T> SendAsync<T>(ApiRequest apiRequest)
    {
        try
        {
            HttpClient client = httpClient.CreateClient("MangoAPI");
            HttpRequestMessage message = new();
            message.Headers.Add("Accept", "application/json");
            string token = await RequestToken();
            message.SetBearerToken(token);
            message.RequestUri = new Uri(apiRequest.Url);
            client.DefaultRequestHeaders.Clear();
            if (apiRequest.Data != null)
            {
                string temp = JsonSerializer.Serialize(apiRequest.Data);
                message.Content = new StringContent(JsonSerializer.Serialize(apiRequest.Data),
                    Encoding.UTF8, "application/json");
            }

            HttpResponseMessage? apiResponse = null;
            message.Method = apiRequest.ApiType switch
            {
                ApiType.POST => HttpMethod.Post,
                ApiType.PUT => HttpMethod.Put,
                ApiType.DELETE => HttpMethod.Delete,
                _ => HttpMethod.Get,
            };
            apiResponse = await client.SendAsync(message);

            string apiContent = await apiResponse.Content.ReadAsStringAsync();
            T? apiResponseDto = JsonSerializer.Deserialize<T>(apiContent, jsonOptions);
            return apiResponseDto;

        }
        catch (Exception e)
        {
            Result dto = new()
            {
                Message = Convert.ToString(e.Message),
                IsSuccess = false
            };
            string res = JsonSerializer.Serialize(dto);
            T? apiResponseDto = JsonSerializer.Deserialize<T>(res, jsonOptions);
            return apiResponseDto;
        }
    }

    private async Task<string> RequestToken()
    {
        var client = new HttpClient();
        var disco = await client.GetDiscoveryDocumentAsync("https://localhost:5001");
        if (disco.IsError)
        {
            Console.WriteLine(disco.Error);
            return null;
        }

        var tokenResponse = await client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
        {
            Address = disco.TokenEndpoint,

            ClientId = "client",
            ClientSecret = "secret",
            Scope = "api1"
        });

        if (tokenResponse.IsError)
        {
            Console.WriteLine(tokenResponse.Error);
            return null;
        }

        return tokenResponse.AccessToken;
    }

    public void Dispose()
    {
        GC.SuppressFinalize(true);
    }
}
