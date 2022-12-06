using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Restauracja.Services.ShoppingCartApi.Data;
using Restauracja.Services.ShoppingCartApi.Model;
using Restauracja.Services.ShoppingCartApi.Model.Dto;

namespace Restauracja.Services.ShoppingCartApi.Services;

internal class CartService : ICartService
{
    private readonly ApplicationDbContext _db;
    private IMapper _mapper;

    public CartService(ApplicationDbContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }

    public async Task<bool> ApplyCoupon(string userId, string couponCode)
    {
        var cartFromDb = await _db.CartHeaders.FirstOrDefaultAsync(u => u.UserId == userId);
        cartFromDb.CouponCode = couponCode;
        _db.CartHeaders.Update(cartFromDb);
        await _db.SaveChangesAsync();
        return true;
    }

    public async Task<bool> ClearCart(string userId)
    {
        var cartHeaderFromDb = await _db.CartHeaders.FirstOrDefaultAsync(u => u.UserId == userId);
        if (cartHeaderFromDb != null)
        {
            _db.CartDetails
                .RemoveRange(_db.CartDetails.Where(u => u.Id == cartHeaderFromDb.Id));
            _db.CartHeaders.Remove(cartHeaderFromDb);
            await _db.SaveChangesAsync();
            return true;

        }
        return false;
    }

    public async Task<CartDto> CreateUpdateCart(CartDto cartDto)
    {

        Cart cart = _mapper.Map<Cart>(cartDto);
        var cartDetails = cart.CartDetails!.FirstOrDefault();
        var cartHeader = cart.CartHeader;

        //check if product exists in database, if not create it!
        var prodInDb = await _db.Products
            .FirstOrDefaultAsync(u => u.Id == cartDetails
            .ProductId);
        if (prodInDb == null)
        {
            _db.Products.Add(cartDetails.Product);
            await _db.SaveChangesAsync();
        }


        //check if header is null
        var cartHeaderFromDb = await _db.CartHeaders.AsNoTracking()
            .FirstOrDefaultAsync(u => u.UserId == cart.CartHeader.UserId);        

        if (cartHeaderFromDb == null)
        {
            //create header and details
            _db.CartHeaders.Add(cart.CartHeader);
            await _db.SaveChangesAsync();
            cartDetails.CartHeaderId = cart.CartHeader.Id;
            cartDetails.Product = null;
            _db.CartDetails.Add(cartDetails);
            await _db.SaveChangesAsync();
        }
        else
        {
            //if header is not null
            //check if details has same product
            var cartDetailsFromDb = await _db.CartDetails.AsNoTracking().FirstOrDefaultAsync(
                u => u.ProductId == cartDetails.ProductId &&
                u.Id == cartHeaderFromDb.Id);

            if (cartDetailsFromDb == null)
            {
                //create details                
                cartDetails.Product = null;
                _db.CartDetails.Add(cartDetails);
                await _db.SaveChangesAsync();
            }
            else
            {
                //update the count / cart details
                cartDetails.Product = null;
                cartDetails.Count += cartDetailsFromDb.Count;
                cartDetails.Id = cartDetailsFromDb.Id;
                cartDetails.CartHeaderId = cartDetailsFromDb.CartHeaderId;
                _db.CartDetails.Update(cartDetails);
                await _db.SaveChangesAsync();
            }
        }

        return _mapper.Map<CartDto>(cart);

    }

    public async Task<CartDto> GetCartByUserId(string userId)
    {
        Cart cart = new()
        {
            CartHeader = await _db.CartHeaders.FirstOrDefaultAsync(u => u.UserId == userId)
        };

        cart.CartDetails = _db.CartDetails
            .Where(u => u.CartHeaderId == cart.CartHeader.Id).Include(u => u.Product);

        return _mapper.Map<CartDto>(cart);
    }

    public async Task<bool> RemoveCoupon(string userId)
    {
        var cartFromDb = await _db.CartHeaders.FirstOrDefaultAsync(u => u.UserId == userId);
        cartFromDb.CouponCode = "";
        _db.CartHeaders.Update(cartFromDb);
        await _db.SaveChangesAsync();
        return true;
    }

    public async Task<bool> RemoveFromCart(int cartDetailsId)
    {
        try
        {
            CartDetails cartDetails = await _db.CartDetails
                .FirstOrDefaultAsync(u => u.Id == cartDetailsId);

            int totalCountOfCartItems = _db.CartDetails
                .Where(u => u.CartHeaderId == cartDetails.CartHeaderId).Count();

            _db.CartDetails.Remove(cartDetails);
            if (totalCountOfCartItems == 1)
            {
                var cartHeaderToRemove = await _db.CartHeaders
                    .FirstOrDefaultAsync(u => u.Id == cartDetails.CartHeaderId);

                _db.CartHeaders.Remove(cartHeaderToRemove);
            }
            await _db.SaveChangesAsync();
            return true;
        }
        catch (Exception e)
        {
            return false;
        }
    }
}