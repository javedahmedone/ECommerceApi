using ECommerce.Repository.Models;

namespace ECommerce.Repository.Interfaces
{
    public interface IProductRepository
    {
        Task<Product?> GetByIdAsync(long id);
        Task<IEnumerable<Product>> GetAllAsync(int pageNumber, int pageSize, bool? isActive,  string? searchText, int? categoryId);
        Task<Product> AddAsync(Product product);
        Task<bool> UpdateAsync(Product product);
        Task<bool> SoftDeleteAsync(long id);
        Task<bool> ReduceStockAsync(long productId, int quantity);
    }
}
