using Restauracja.Services.ProductApi.Model;

namespace Restauracja.Services.CouponApi.Model;

public class Coupon : BaseEntity
{
    public string CouponCode { get; set; }
    public double DiscountAmount { get; set; }
}
