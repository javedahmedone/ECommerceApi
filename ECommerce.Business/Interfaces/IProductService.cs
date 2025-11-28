using ECommerce.Business.DTOs;

namespace ECommerce.Business.Interfaces
{
    public interface IProductService
    {
        Task<ProductResponseDto> CreateProductAsync(ProductCreateDto dto);
        Task<bool> UpdateProductAsync(ProductUpdateDto dto);
        Task<bool> DeleteProductAsync(long id);
        Task<IEnumerable<ProductResponseDto>> GetAllAsync(int pageNumber, int pageSize, bool? isActive, string? searchText, int? categoryId);

    }

}
