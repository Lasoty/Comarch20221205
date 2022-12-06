using Microsoft.AspNetCore.Mvc;
using Restauracja.Common.Model;
using Restauracja.Services.CouponApi.Repositories;

namespace Restauracja.Services.CouponApi.Controllers
{

    [ApiController]
    [Route("api/[Controller]")]
    public class CouponAPIController : ControllerBase
    {
        private readonly ICouponRepository _couponRepository;

        public CouponAPIController(ICouponRepository couponRepository)
        {
            _couponRepository = couponRepository;
        }

        [HttpGet("{code}")]
        public async Task<IActionResult> GetDiscountForCode(string code)
        {
            try
            {
                var coupon = await _couponRepository.GetCouponByCode(code);
                return Ok(Result.Success(coupon));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
