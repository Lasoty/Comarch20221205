using Restauracja.Common.Model;
using Restauracja.Web.Models;

namespace Restauracja.Web.Services.Interfaces;

public interface IProductService : IBaseService
{
    Task<T> GetAllProductsAsync<T>() where T : Result;
    Task<T> GetProductByIdAsync<T>(long id) where T : Result;
    Task<T> CreateProductAsync<T>(ProductDto productDto) where T : Result;
    Task<T> UpdateProductAsync<T>(ProductDto productDto) where T : Result;
    Task<T> DeleteProductAsync<T>(long id) where T : Result;
}
