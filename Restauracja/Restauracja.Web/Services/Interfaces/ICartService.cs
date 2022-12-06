using Restauracja.Web.Models;

namespace Restauracja.Web.Services.Interfaces;

public interface ICartService
{
    Task<T> GetCartByUserIdAsync<T>(string userId);
    Task<T> AddToCartAsync<T>(CartDto cartDto);
    Task<T> UpdateCartAsync<T>(CartDto cartDto);
    Task<T> RemoveFromCartAsync<T>(int cartId);
    Task<T> ApplyCoupon<T>(CartDto cartDto);
    Task<T> RemoveCoupon<T>(string userId);
    Task<T> Checkout<T>(CartHeaderDto cartHeader);
}
