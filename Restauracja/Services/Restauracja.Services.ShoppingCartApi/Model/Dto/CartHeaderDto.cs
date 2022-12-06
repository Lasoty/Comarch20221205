using Restauracja.Common.Model.Dto;

namespace Restauracja.Services.ShoppingCartApi.Model.Dto;

public class CartHeaderDto : BaseDtoEntity
{
    public string UserId { get; set; }
    public string? CouponCode { get; set; }
    public double OrderTotal { get; set; }
}
