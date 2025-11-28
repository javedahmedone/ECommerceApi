using ECommerce.Business.DTOs;
using ECommerce.Business.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Api.Controllers
{
    [ApiController]
    [Authorize(Roles = "ADMIN")]
    [Route("api/admin")]
    public class AdminProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public AdminProductsController(IProductService productService)
        {
            _productService = productService;
        }


        [HttpPost("Product")]
        public async Task<IActionResult> Create(ProductCreateDto request)
        {
            var result = await _productService.CreateProductAsync(request);
            return Ok(result);
        }

        // PUT: /api/admin/products/{id}
        [HttpPut("Product")]
        public async Task<IActionResult> Update(ProductUpdateDto request)
        {
            var result = await _productService.UpdateProductAsync(request);
            if (result)
                return Ok("Record updated successfullly");
            return Ok(result);
        }

        // DELETE: soft delete → mark inactive
        [HttpDelete("Product/{id:long}")]
        public async Task<IActionResult> Delete(long id)
        {
            var result = await _productService.DeleteProductAsync(id);
            return Ok(result);
        }

       
    }
}
