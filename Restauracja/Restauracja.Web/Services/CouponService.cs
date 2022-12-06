using Restauracja.Web.Models;
using Restauracja.Web.Services.Interfaces;

namespace Restauracja.Web.Services;

public class CouponService : BaseService, ICouponService
{
    public CouponService(IHttpClientFactory clientFactory) : base(clientFactory)
    { }

    public async Task<T> GetCoupon<T>(string couponCode)
    {
        return await this.SendAsync<T>(new ApiRequest()
        {
            ApiType = ApiType.GET,
            Url = CouponAPIBase + "/api/coupon/" + couponCode
        });
    }
}
