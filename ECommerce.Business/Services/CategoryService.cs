using ECommerce.Business.DTOs;
using ECommerce.Business.ExceptionHandlers;
using ECommerce.Business.Interfaces;
using ECommerce.Business.Mapper;
using ECommerce.Repository.Interfaces;

namespace ECommerce.Business.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _repo;

        public CategoryService(ICategoryRepository repo)
        {
            _repo = repo;
        }

        public async Task<CategoryResponseDto> CreateCategoryAsync(CategoryCreateDto dto)
        {
            var cat =  DtoRequestMapper.CategoryDtoToModel(dto);
            var result = await _repo.AddAsync(cat);
            return DTOsResponseMapper.CategoryToDto(result);

        }

        public async Task<bool> UpdateCategoryAsync(CategoryUpdateDto dto)
        {
            var cat = await _repo.GetByIdAsync(dto.Id);
            if (cat == null) throw new AppBadRequestException("Category not found or Invalid category");

            cat =  DtoRequestMapper.CategoryUpdateDtoToModel(dto);
            return await _repo.UpdateAsync(cat);
        }

        public async Task<bool> DeleteCategoryAsync(long id)
        {
            if(id <= 0 )       throw new AppBadRequestException("Category id cannot be negative");
            var result = await _repo.GetByIdAsync(id);
            if (result is null)
                throw new AppBadRequestException("Category not found or Invalid category");
            return await _repo.DeleteAsync(id);
        }

        public async Task<IEnumerable<CategoryResponseDto>> GetAllAsync()
        {
            var list = await _repo.GetAllAsync();
            if(list is null)
                return Enumerable.Empty<CategoryResponseDto>();
            return DTOsResponseMapper.AllCategoryToDto(list);           
           
        }
    }

}
