using ECommerce.Business.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace ECommerce.Api.Controllers
{
    [ApiController]
    [Route("api/Products")]
    public class ProductsController : Controller
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProduct(int pageNumber, int pageSize, bool? isActive, string? searchText = null, int? categoryId = null)
        {
            var result = await _productService.GetAllAsync(pageNumber, pageSize, isActive, searchText, categoryId);
            if (result == null)
                return NotFound();
            return Ok(result);
        }
    }
}
