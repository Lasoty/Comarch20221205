using Restauracja.Common.Model.Dto;

namespace Restauracja.Services.CouponApi.Model.Dto;

public class CouponDto : BaseDtoEntity
{
    public string CouponCode { get; set; }
    public double DiscountAmount { get; set; }
}
