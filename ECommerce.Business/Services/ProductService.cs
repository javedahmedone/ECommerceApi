using ECommerce.Business.DTOs;
using ECommerce.Business.ExceptionHandlers;
using ECommerce.Business.Interfaces;
using ECommerce.Business.Mapper;
using ECommerce.Repository.Interfaces;

namespace ECommerce.Business.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepo;
        private readonly ICategoryRepository _catRepo;

        public ProductService(IProductRepository repo, ICategoryRepository catRepo)
        {
            _productRepo = repo;
            _catRepo = catRepo;
        }

        public async Task<ProductResponseDto> CreateProductAsync(ProductCreateDto dto)
        {
            var category = await _catRepo.GetByIdAsync(dto.CategoryId);
            if (category == null) throw new Exception("Invalid category");

            //var duplicate = await _catRepo.GetByIdAsync(dto.Name, dto.CategoryId);
            //if (duplicate != null) throw new Exception("Product already exists in this category");
            var product = DtoRequestMapper.ProductDtoToModel(dto);           

            await _productRepo.AddAsync(product);
            return DTOsResponseMapper.ProductDBModelToDTO(product);
        }

        public async Task<bool> UpdateProductAsync(ProductUpdateDto dto)
        {
            var product = await _productRepo.GetByIdAsync(dto.Id);
            if (product == null) throw new AppBadRequestException("Product not found");
            DTOsResponseMapper.ProductDBModelToDTO(ref product,ref dto);
            return await _productRepo.UpdateAsync(product);

        }

        public async Task<bool> DeleteProductAsync(long id)
        {
            return await _productRepo.SoftDeleteAsync(id);
        }

        public async Task<IEnumerable<ProductResponseDto>> GetAllAsync(int pageNumber, int pageSize, bool? isActive, string? searchText, int? categoryId)
        {
            if(pageNumber < 0 || pageSize < 0)
               throw new AppBadRequestException("page number of page size is invalid ");
            if(categoryId is not null && categoryId <= 0 )
                throw new AppBadRequestException("categroyId is invalid , please enter valid category Id");
            if(pageNumber == 0 & pageSize == 0)
            {
                pageSize = 10;
                pageNumber = 1;
            }
            var productList = await _productRepo.GetAllAsync(pageNumber, pageSize,isActive, searchText, categoryId);
            if (productList == null || !productList.Any())
                return Enumerable.Empty<ProductResponseDto>();
            return DTOsResponseMapper.ProductDBModelToDTO(productList).ToList();
        }

        public async Task<ProductResponseDto> GetByIdAsync(int id)
        {
            var p = await _productRepo.GetByIdAsync(id);
            if (p == null) throw new Exception("Product not found");

            return new ProductResponseDto
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                Price = p.Price,
                CategoryName = p.Category.Name,
                StockQuantity = p.StockQuantity,
                IsActive = p.IsActive
            };
        }

      
    }

}
