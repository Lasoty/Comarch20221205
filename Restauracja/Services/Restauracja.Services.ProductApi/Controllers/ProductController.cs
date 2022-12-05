using Microsoft.AspNetCore.Mvc;
using Restauracja.Services.ProductApi.Model;
using Restauracja.Services.ProductApi.Model.Dto;
using Restauracja.Services.ProductApi.Repositories;

namespace Restauracja.Services.ProductApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IRepository<Product, ProductDto> _productRepository;

        public ProductController(IRepository<Product, ProductDto> productRepository)
        {
            _productRepository = productRepository;
        }

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

        [HttpPost("{id}")]
        public async Task<IActionResult> Add([FromBody] ProductDto value)
        {
            try
            {
                var product = await _productRepository.Add(value);
                return Ok(Result.Success(product));
            }
            catch (Exception ex)
            {
                return BadRequest(Result.Fail(ex.Message));
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ProductDto value)
        {
            try
            {
                if (!await _productRepository.Exists(id))
                    return BadRequest(Result.Fail($"Product does not exists."));


                var product = await _productRepository.Update(value);
                return Ok(Result.Success(product));
            }
            catch (Exception ex)
            {
                return BadRequest(Result.Fail(ex.Message));
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                if (!await _productRepository.Exists(id))
                    return BadRequest(Result.Fail($"Product does not exists."));


                if(!await _productRepository.Delete(id))
                    return BadRequest(Result.Fail($"An error occured while deleting the product."));


                return Ok(Result.Success());
            }
            catch (Exception ex)
            {
                return BadRequest(Result.Fail(ex.Message));
            }
        }
    }
}
