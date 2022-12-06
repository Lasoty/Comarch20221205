using Restauracja.Web.Models;
using Restauracja.Web.Services.Interfaces;

namespace Restauracja.Web.Services;

public class CartService : BaseService, ICartService
{
    public CartService(IHttpClientFactory clientFactory) : base(clientFactory)
    {}

    public async Task<T> AddToCartAsync<T>(CartDto cartDto)
    {
        return await SendAsync<T>(new ApiRequest()
        {
            ApiType = ApiType.POST,
            Data = cartDto,
            Url = ShoppingCartAPIBase + "/api/Cart/AddCart"
        });
    }

    public async Task<T> ApplyCoupon<T>(CartDto cartDto)
    {
        return await this.SendAsync<T>(new ApiRequest()
        {
            ApiType = ApiType.POST,
            Data = cartDto,
            Url = ShoppingCartAPIBase + "/api/Cart/ApplyCoupon",
        });
    }

    public async Task<T> GetCartByUserIdAsync<T>(string userId)
    {
        return await SendAsync<T>(new ApiRequest()
        {
            ApiType = ApiType.GET,
            Url = ShoppingCartAPIBase + "/api/Cart/GetCart/" + userId,
        });
    }

    public async Task<T> RemoveFromCartAsync<T>(int cartId)
    {
        return await SendAsync<T>(new ApiRequest()
        {
            ApiType = ApiType.POST,
            Data = cartId,
            Url = ShoppingCartAPIBase + "/api/Cart/RemoveCart",
        });
    }

    public async Task<T> RemoveCoupon<T>(string userId)
    {
        return await SendAsync<T>(new ApiRequest()
        {
            ApiType = ApiType.POST,
            Data = userId,
            Url = ShoppingCartAPIBase + "/api/Cart/RemoveCoupon"
        });
    }

    public async Task<T> UpdateCartAsync<T>(CartDto cartDto)
    {
        return await SendAsync<T>(new ApiRequest()
        {
            ApiType = ApiType.POST,
            Data = cartDto,
            Url = ShoppingCartAPIBase + "/api/Cart/UpdateCart"
        });
    }

    public async Task<T> Checkout<T>(CartHeaderDto cartHeader)
    {
        return await SendAsync<T>(new ApiRequest()
        {
            ApiType = ApiType.POST,
            Data = cartHeader,
            Url = ShoppingCartAPIBase + "/api/Cart/checkout"
        });
    }
}
