using AutoMapper;
using Restauracja.Services.CouponApi.Model.Dto;
using Restauracja.Services.CouponApi.Model;

namespace Restauracja.Services.CouponApi.Mappings;

public class CouponDtoProfile : Profile
{
	public CouponDtoProfile()
	{
        CreateMap<CouponDto, Coupon>().ReverseMap();
    }
}
