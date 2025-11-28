using ECommerce.Repository.Interfaces;
using ECommerce.Repository.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Repository.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly AppDbContext _ctx;

        public CategoryRepository(AppDbContext ctx)
        { 
            _ctx = ctx;
        }

        public async Task<Category?> GetByIdAsync(long id)
        {
            return await _ctx.Categories
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            return await _ctx.Categories.ToListAsync();
        }

        public async Task<Category> AddAsync(Category category)
        {
            _ctx.Categories.Add(category);
            await _ctx.SaveChangesAsync();
            return category;
        }

        public async Task<bool> UpdateAsync(Category category)
        {
            _ctx.Categories.Update(category);
            return await _ctx.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteAsync(long id)
        {
            var entity = await _ctx.Categories.FindAsync(id);
            if (entity == null) return false;

            _ctx.Categories.Remove(entity);
            return await _ctx.SaveChangesAsync() > 0;
        }
    }
}
