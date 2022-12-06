using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Restauracja.Services.CouponApi.Data;
using Restauracja.Services.CouponApi.Model.Dto;

namespace Restauracja.Services.CouponApi.Repositories;

public class CouponRepository : ICouponRepository
{
    private readonly ApplicationDbContext _db;
    protected IMapper _mapper;
    public CouponRepository(ApplicationDbContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }

    public async Task<CouponDto> GetCouponByCode(string couponCode)
    {
        var couponFromDb = await _db.Coupons.FirstOrDefaultAsync(u => u.CouponCode == couponCode);
        return _mapper.Map<CouponDto>(couponFromDb);
    }
}
