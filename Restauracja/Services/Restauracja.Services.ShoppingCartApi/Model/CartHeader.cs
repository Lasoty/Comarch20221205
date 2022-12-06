using Restauracja.Services.ProductApi.Model;

namespace Restauracja.Services.ShoppingCartApi.Model;

public class CartHeader : BaseEntity
{
    public string UserId { get; set; }
    public string? CouponCode { get; set; }
}
