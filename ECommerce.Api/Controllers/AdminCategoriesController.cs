using ECommerce.Business.DTOs;
using ECommerce.Business.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Api.Controllers
{
    [Authorize(Roles = "ADMIN")]
    [ApiController]
    [Route("api/admin/categories")]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        // CREATE
        [HttpPost("Category")]
        public async Task<IActionResult> CreateCategory([FromBody] CategoryCreateDto dto)
        {
            var result = await _categoryService.CreateCategoryAsync(dto);
            return CreatedAtAction(nameof(GetAllCategories), result);
        }

        // UPDATE
        [HttpPut("Category")]
        public async Task<IActionResult> UpdateCategory([FromBody] CategoryUpdateDto dto)
        {
            var success = await _categoryService.UpdateCategoryAsync(dto);
            return success ? Ok() : NotFound();
        }

        // DELETE
        [HttpDelete("Category/{id:long}")]
        public async Task<IActionResult> DeleteCategory(long id)
        {
            var success = await _categoryService.DeleteCategoryAsync(id);
            return Ok("Category deleted successfully");
        }

        // GET ALL
        [HttpGet]
        public async Task<IActionResult> GetAllCategories()
        {
            var categories = await _categoryService.GetAllAsync();
            if(categories is  null)
                return NoContent();
            return Ok(categories);
        }
    }

}
