using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Restauracja.Common.Model;
using Restauracja.Web.Models;
using Restauracja.Web.Services;
using Restauracja.Web.Services.Interfaces;

namespace Restauracja.Web.Controllers;

public class CartController : Controller
{
    private readonly IProductService _productService;
    private readonly ICartService _cartService;
    private readonly ICouponService _couponService;

    public CartController(IProductService productService, ICartService cartService, ICouponService couponService)
    {
        _productService = productService;
        _cartService = cartService;
        _couponService = couponService;
    }

    public async Task<IActionResult> CartIndex()
    {
        return View(await LoadCartDtoBasedOnLoggedInUser());
    }

    [HttpPost]
    [ActionName("ApplyCoupon")]
    public async Task<IActionResult> ApplyCoupon(CartDto cartDto)
    {
        var userId = User.Claims.Where(u => u.Type == "sub")?.FirstOrDefault()?.Value;
        var accessToken = await HttpContext.GetTokenAsync("access_token");
        var response = await _cartService.ApplyCoupon<Result>(cartDto);

        if (response != null && response.IsSuccess)
        {
            return RedirectToAction(nameof(CartIndex));
        }
        return View();
    }

    [HttpPost]
    [ActionName("RemoveCoupon")]
    public async Task<IActionResult> RemoveCoupon(CartDto cartDto)
    {
        var userId = User.Claims.Where(u => u.Type == "sub")?.FirstOrDefault()?.Value;
        var response = await _cartService.RemoveCoupon<Result>(cartDto.CartHeader.UserId);

        if (response != null && response.IsSuccess)
        {
            return RedirectToAction(nameof(CartIndex));
        }
        return View();
    }

    public async Task<IActionResult> Remove(int cartDetailsId)
    {
        var userId = User.Claims.Where(u => u.Type == "sub")?.FirstOrDefault()?.Value;
        var response = await _cartService.RemoveFromCartAsync<Result>(cartDetailsId);


        if (response != null && response.IsSuccess)
        {
            return RedirectToAction(nameof(CartIndex));
        }
        return View();
    }

    [HttpGet("Checkout")]
    public async Task<IActionResult> Checkout()
    {
        return View(await LoadCartDtoBasedOnLoggedInUser());
    }

    [HttpPost("Checkout")]
    public async Task<IActionResult> Checkout(CartDto cartDto)
    {
        try
        {
            var response = await _cartService.Checkout<Result>(cartDto.CartHeader);
            return RedirectToAction(nameof(Confirmation));
        }
        catch (Exception e)
        {
            return View(cartDto);
        }
    }


    [HttpGet]
    public async Task<IActionResult> Confirmation()
    {
        return View();
    }

    private async Task<CartDto> LoadCartDtoBasedOnLoggedInUser()
    {
        var userId = User.Claims.Where(u => u.Type == "sub")?.FirstOrDefault()?.Value;
        var response = await _cartService.GetCartByUserIdAsync<Result<CartDto>>(userId);

        CartDto cartDto = new();
        if (response != null && response.IsSuccess)
        {
            cartDto = response.Data;
        }

        if (cartDto.CartHeader != null)
        {
            if (!string.IsNullOrEmpty(cartDto.CartHeader.CouponCode))
            {
                var resultCoupon = await _couponService.GetCoupon<Result<CouponDto>>(cartDto.CartHeader.CouponCode);
                if (resultCoupon != null && resultCoupon.IsSuccess)
                {
                    var couponObj = resultCoupon.Data;
                    cartDto.CartHeader.DiscountTotal = couponObj.DiscountAmount;
                }
            }

            foreach (var detail in cartDto.CartDetails)
            {
                cartDto.CartHeader.OrderTotal += (detail.Product.Price * detail.Count);
            }

            cartDto.CartHeader.OrderTotal -= cartDto.CartHeader.DiscountTotal;
        }
        return cartDto;
    }
}
