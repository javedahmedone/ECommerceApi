using ECommerce.Business.DTOs;
using ECommerce.Repository.Models;

namespace ECommerce.Business.Mapper
{
    public static class DTOsResponseMapper
    {
        public static RegisterResponseDto RegsisterToDto(User user)
        {
            return new RegisterResponseDto
            {
                UserId = user.Id,
                Role = user.Role,
                Email = user.Email,
            };
        }

        public static AuthResponseDto LoginToDto(User user, string token)
        {
            return new AuthResponseDto
            {
                UserId = user.Id,
                Role = user.Role,
                Email = user.Email,
                Token = token
            };
        }
        public static CategoryResponseDto CategoryToDto(Category cat)
        {
            return new CategoryResponseDto
            {
                Id = cat.Id,
                Name = cat.Name,
                Description = cat.Description
            };
        }
        public static IEnumerable<CategoryResponseDto> AllCategoryToDto(IEnumerable<Category> list) {
            return list.Select(c => new CategoryResponseDto
            {
                Id = c.Id,
                Name = c.Name,
                Description = c.Description,
                CreatedDate = c.CreatedDate,
                UpdatedDate = c.UpdatedDate
            }).ToList();
        }

        public static ProductResponseDto ProductDBModelToDTO(Product product)
        {
            return new ProductResponseDto
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                StockQuantity = product.StockQuantity,
                IsActive = product.IsActive,
                CategoryName = product.Category?.Name,
                CategoryId = product.Category.Id
            };
        }

        public static IEnumerable<ProductResponseDto> ProductDBModelToDTO(IEnumerable<Product> products)
        {
            return products.Select(ProductDBModelToDTO);
        }

        public static void ProductDBModelToDTO( ref Product product, ref ProductUpdateDto dto)
        {
            product.Name = dto.Name;
            product.Price = dto.Price;
            product.Description = dto.Description;
            product.StockQuantity = dto.StockQuantity;
            product.CategoryId = dto.CategoryId;
            product.IsActive = dto.isActive;
        }
        public static OrderResponseDto OderDBModelToDTO(Order order)
        {
            if (order == null) return null;

            return new OrderResponseDto
            {
                Id = order.Id,
                OrderDate = order.OrderDate,
                TotalAmount = order.TotalAmount,
            };
        }
    }

    public static class DtoRequestMapper
    {
        public static Category CategoryDtoToModel(CategoryCreateDto dto)
        {
            return new Category
            {
                Name = dto.Name,
                Description = dto.Description,
                CreatedDate = DateTime.UtcNow
            };
        }
        public static Category CategoryUpdateDtoToModel(CategoryUpdateDto dto)
        {
            return new Category {
                Name = dto.Name,
                Description = dto.Description
            };
        }
       

        public static User UserDTOToDBModel(RegisterRequestDto request, string hashed)
        {
            return new User
            {
                Name = request.Name,
                Email = request.Email,
                Passwordhash = hashed,
                Role = request.Role,
                CreatedDate = DateTime.UtcNow
            };
        }

        public static Product ProductDtoToModel(this ProductCreateDto dto)
        {
            return new Product
            {
                Name = dto.Name,
                Description = dto.Description,
                Price = dto.Price,
                StockQuantity = dto.StockQuantity,
                CategoryId = dto.CategoryId,
                IsActive = true,
                CreatedDate = DateTime.UtcNow
            };
        }

        public static Order OderDtoToModel(OrderCreateDto dto, Product product, long customerId)
        {
            return new Order
            {
                Id = Guid.NewGuid(),
                CustomerId = customerId,
                OrderDate = DateTime.UtcNow,
                ProductId = dto.ProductId,
                Quantity = dto.Quantity,
                Status = (int)OrderStateEnum.PENDING,
                OrderPrice = (double?)product.Price,
                TotalAmount = product.Price * dto.Quantity
            };
        }


    }
 }
