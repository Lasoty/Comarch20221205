using Microsoft.AspNetCore.Mvc;
using Restauracja.Common.Model;
using Restauracja.Web.Models;
using Restauracja.Web.Services.Interfaces;
using System.Diagnostics;

namespace Restauracja.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProductService _productService;
        private readonly ILogger<HomeController> _logger;

        public HomeController(IProductService productService, ILogger<HomeController> logger)
        {
            this._productService = productService;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<ProductDto> list = new List<ProductDto>();
            var result = await _productService.GetAllProductsAsync<Result<IEnumerable<ProductDto>>>();
            if (result != null && result.IsSuccess)
            {
                list = result.Data;
            }
            return View(list);
        }

        public async Task<IActionResult> Details(int productId)
        {
            ProductDto model = new();
            var result = await _productService.GetProductByIdAsync<Result<ProductDto>>(productId);
            if (result != null && result.IsSuccess)
            {
                model = result.Data;
            }
            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}