using Microsoft.AspNetCore.Mvc;
using Restauracja.Common.Model;
using Restauracja.MessageBus;
using Restauracja.Services.ShoppingCartApi.Model.Dto;
using Restauracja.Services.ShoppingCartApi.Services;

namespace Restauracja.Services.ShoppingCartApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CartController : Controller
{
    private readonly ICartService _cartService;
    private readonly IMessageBus _messageBus;

    public CartController(ICartService cartRepository, IMessageBus messageBus)
    {
        _cartService = cartRepository;
        _messageBus = messageBus;
    }

    [HttpGet("GetCart/{userId}")]
    public async Task<IActionResult> GetCart(string userId)
    {
        try
        {
            CartDto cartDto = await _cartService.GetCartByUserId(userId);
            return Ok(Result.Success(cartDto));
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, Result.Fail(ex.ToString()));
        }
    }

    [HttpPost("AddCart")]
    public async Task<IActionResult> AddCart(CartDto cartDto)
    {
        try
        {
            CartDto cartDt = await _cartService.CreateUpdateCart(cartDto);
            return Ok(Result.Success(cartDt));
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, Result.Fail(ex.ToString()));
        }
    }

    [HttpPost("UpdateCart")]
    public async Task<IActionResult> UpdateCart(CartDto cartDto)
    {
        try
        {
            CartDto cartDt = await _cartService.CreateUpdateCart(cartDto);
            return Ok(Result.Success(cartDt));
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, Result.Fail(ex.ToString()));
        }
    }

    [HttpPost("RemoveCart")]
    public async Task<IActionResult> RemoveCart([FromBody] int cartId)
    {
        try
        {
            bool isSuccess = await _cartService.RemoveFromCart(cartId);
            if (isSuccess) 
            {
                return Ok(Result.Success());
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError, Result.Fail());
            }
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, Result.Fail(ex.ToString()));
        }
    }

    [HttpPost("ApplyCoupon")]
    public async Task<IActionResult> ApplyCoupon([FromBody] CartDto cartDto)
    {
        try
        {
            bool isSuccess = await _cartService.ApplyCoupon(cartDto.CartHeader.UserId,cartDto.CartHeader.CouponCode);
            return isSuccess ? Ok(Result.Success()) : BadRequest(Result.Fail());
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, Result.Fail(ex.Message));

        }
    }

    [HttpPost("RemoveCoupon")]
    public async Task<IActionResult> RemoveCoupon([FromBody] string userId)
    {
        try
        {
            bool isSuccess = await _cartService.RemoveCoupon(userId);
            return isSuccess ? Ok(Result.Success()) : BadRequest(Result.Fail());
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, Result.Fail(ex.Message));

        }
    }

    [HttpPost("Checkout")]
    public async Task<IActionResult> Checkout(CheckoutHeaderDto checkoutHeader)
    {
        try
        {
            CartDto cartDto = await _cartService.GetCartByUserId(checkoutHeader.UserId);
            if (cartDto == null)
            {
                return BadRequest(Result.Fail());
            }

            checkoutHeader.CartDetails = cartDto.CartDetails;
            await _messageBus.PublishMessage(checkoutHeader, "checkordertopic");

            return Ok(Result.Success(cartDto));
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, Result.Fail(ex.Message));
        }
    }
}
