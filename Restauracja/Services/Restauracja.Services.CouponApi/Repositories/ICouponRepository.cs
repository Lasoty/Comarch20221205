using Restauracja.Services.CouponApi.Model.Dto;

namespace Restauracja.Services.CouponApi.Repositories;

public interface ICouponRepository
{
    Task<CouponDto> GetCouponByCode(string couponCode);
}
