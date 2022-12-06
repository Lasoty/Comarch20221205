using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restauracja.Common.Model;
using Restauracja.Common.Model.Repositories;
using Restauracja.Services.ProductApi.Model;
using Restauracja.Services.ProductApi.Model.Dto;

namespace Restauracja.Services.ProductApi.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class ProductsController : ControllerBase
{
    private readonly IRepository<Product, ProductDto> _productRepository;

    public ProductsController(IRepository<Product, ProductDto> productRepository)
    {
        _productRepository = productRepository;
    }

    // GET: api/Products
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        try
        {
            return Ok(Result.Success(await _productRepository.GetAll()));
        }
        catch (Exception ex)
        {
            return BadRequest(Result.Fail(ex.Message));
        }
    }

    // GET api/Products/5
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        try
        {
            return Ok(Result.Success(await _productRepository.GetById(id)));
        }
        catch (Exception ex)
        {
            return BadRequest(Result.Fail(ex.Message));
        }
    }

    // POST api/Products
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] ProductDto value)
    {
        try
        {
            ProductDto product = await _productRepository.Add(value);
            return Ok(Result.Success(product));
        }
        catch (Exception ex)
        {
            return BadRequest(Result.Fail(ex.Message));
        }
    }

    // PUT api/Products/5
    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, [FromBody] ProductDto value)
    {
        try
        {
            if (!await _productRepository.Exists(id))
                return BadRequest(Result.Fail($"Product with id {id} does not exists."));

            if (!await _productRepository.Update(value))
                return BadRequest(Result.Fail($"An error occurred while updating the product."));

            return Ok(Result.Success());
        }
        catch (Exception ex)
        {
            return BadRequest(Result.Fail(ex.Message));
        }
    }

    // DELETE api/Products/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            if (!await _productRepository.Exists(id))
                return BadRequest(Result.Fail($"Product with id {id} does not exists."));

            if (!await _productRepository.Delete(id))
                return BadRequest(Result.Fail($"An error occurred while deleting the product."));

            return Ok(Result.Success());
        }
        catch (Exception ex)
        {
            return BadRequest(Result.Fail(ex.Message));
        }
    }
}
