using ECommerce.Business.DTOs;
namespace ECommerce.Business.Interfaces
{
    public interface ICategoryService
    {
        Task<CategoryResponseDto> CreateCategoryAsync(CategoryCreateDto dto);
        Task<bool> UpdateCategoryAsync(CategoryUpdateDto dto);
        Task<bool> DeleteCategoryAsync(long id);
        Task<IEnumerable<CategoryResponseDto>> GetAllAsync();
    }

}
