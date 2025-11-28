
using ECommerce.Repository.Interfaces;
using ECommerce.Repository.Models;
using Microsoft.EntityFrameworkCore;


namespace ECommerce.Repository.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _ctx;

        public ProductRepository(AppDbContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<Product?> GetByIdAsync(int id)
        {
            return await _ctx.Products
                .Include(p => p.Category)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<Product>> GetAllAsync(int pageNumber, int pageSize, bool? isActive,string? searchText, int? categoryId)
        {
            var query = _ctx.Products
                .Include(p => p.Category)            
                .AsQueryable();

            if (categoryId.HasValue)
                query = query.Where(x => x.CategoryId == categoryId.Value);
            if(isActive.HasValue)
                query = query.Where(x => x.IsActive == isActive);

            if (!string.IsNullOrWhiteSpace(searchText))
            {
                searchText = searchText.ToLower();
                query = query.Where(p =>
                    p.Name.ToLower().Contains(searchText) ||
                    p.Description.ToLower().Contains(searchText)
                );
            }

            return await query
                .OrderBy(p => p.Id)                  
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

      
        public async Task<Product?> GetByNameAndCategoryAsync(string name, int categoryId)
        {
            return await _ctx.Products
                .IgnoreQueryFilters() // to check duplicates even if soft-deleted present
                .FirstOrDefaultAsync(p => p.Name == name && p.CategoryId == categoryId && p.IsActive);
        }

        public async Task<Product> AddAsync(Product product)
        {
            await _ctx.Products.AddAsync(product);
            await _ctx.SaveChangesAsync();
            return product;
        }

        public async Task<bool> UpdateAsync(Product product)
        {
            product.UpdatedDate = DateTime.UtcNow;
            _ctx.Products.Update(product);
            return await _ctx.SaveChangesAsync() > 0;
        }

        public async Task<bool> SoftDeleteAsync(long id)
        {
            var entity = await _ctx.Products.IgnoreQueryFilters().FirstOrDefaultAsync(p => p.Id == id);
            if (entity == null) return false;

            entity.IsActive = false;
            entity.UpdatedDate = DateTime.UtcNow;
            _ctx.Products.Update(entity);
            return await _ctx.SaveChangesAsync() > 0;
        }

       
        public async Task<bool> ReduceStockAsync(long productId, int quantity)
        {
            if (quantity <= 0) throw new ArgumentException("Quantity must be > 0", nameof(quantity));

            var product = await _ctx.Products.IgnoreQueryFilters().FirstOrDefaultAsync(p => p.Id == productId);
            if (product == null || !product.IsActive) return false;

            if (product.StockQuantity < quantity) return false;

            product.StockQuantity -= quantity;
            product.UpdatedDate = DateTime.UtcNow;
            _ctx.Products.Update(product);
            return await _ctx.SaveChangesAsync() > 0;
        }

        public async Task<bool> IncreaseStockAsync(int productId, int quantity)
        {
            if (quantity <= 0) throw new ArgumentException("Quantity must be > 0", nameof(quantity));

            var product = await _ctx.Products.IgnoreQueryFilters().FirstOrDefaultAsync(p => p.Id == productId);
            if (product == null) return false;

            product.StockQuantity += quantity;
            product.UpdatedDate = DateTime.UtcNow;
            _ctx.Products.Update(product);
            return await _ctx.SaveChangesAsync() > 0;
        }

        public async Task<Product?> GetByIdAsync(long id)
        {
            return await _ctx.Products
               .FirstOrDefaultAsync(x => x.Id == id);
        }
    }

}
