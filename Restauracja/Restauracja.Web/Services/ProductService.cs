using Restauracja.Common.Model;
using Restauracja.Web.Models;
using Restauracja.Web.Services.Interfaces;

namespace Restauracja.Web.Services
{
    public class ProductService : BaseService, IProductService
    {
        public ProductService(IHttpClientFactory httpClient) : base(httpClient)
        {
        }

        public async Task<T> CreateProductAsync<T>(ProductDto productDto) where T : Result
        {
            return await SendAsync<T>(new ApiRequest
            {
                ApiType = ApiType.POST,
                Data= productDto,
                Url = ProductApiBase + "/api/Products"
            });
        }

        public async Task<T> DeleteProductAsync<T>(long id) where T : Result
        {
            return await SendAsync<T>(new ApiRequest
            {
                ApiType = ApiType.DELETE,
                Url = ProductApiBase + "/api/Products/" + id
            });
        }

        public async Task<T> GetAllProductsAsync<T>() where T : Result
        {
            return await SendAsync<T>(new ApiRequest
            {
                ApiType = ApiType.GET,
                Url = ProductApiBase + "/api/Products"
            });
        }

        public async Task<T> GetProductByIdAsync<T>(long id) where T : Result
        {
            return await SendAsync<T>(new ApiRequest
            {
                ApiType = ApiType.GET,
                Url = ProductApiBase + "/api/Products/" + id
            });
        }

        public async Task<T> UpdateProductAsync<T>(ProductDto productDto) where T : Result
        {
            return await SendAsync<T>(new ApiRequest
            {
                ApiType = ApiType.PUT,
                Data = productDto,
                Url = ProductApiBase + "/api/Products/" + productDto.Id
            });
        }
    }
}
