using ECommerce.Repository.Interfaces;
using ECommerce.Repository.Models;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Repository.Repositories
{
    internal class OrderRepository : IOrderRepository
    {
        private readonly AppDbContext _ctx;

        public OrderRepository(AppDbContext ctx)
        {
            _ctx = ctx;
        }


        public async Task<List<Order>> GetAllAsync()
        {
            return null;
            //return await _ctx.Orders
            //    .Include(o => o.Items)
            //    .ToListAsync();
        }

        public async Task<Order> AddAsync(Order order)
        {
            _ctx.Orders.Add(order);
            await _ctx.SaveChangesAsync();
            return order;
        }

        public async Task<bool> UpdateAsync(Order order)
        {
            _ctx.Orders.Update(order);
            return await _ctx.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _ctx.Orders.FindAsync(id);
            if (entity == null) return false;

            _ctx.Orders.Remove(entity);
            return await _ctx.SaveChangesAsync() > 0;
        }

        public async Task<IEnumerable<Order>?> GetOrdersByCustomerIdAsync(long id)
        {
           var result =  await _ctx.Orders.Include(x=> x.Customer).Where(x =>x.CustomerId == id).AsQueryable().ToListAsync();
            return result;
        }

        public async Task<Order> GetOrderByOrderId(Guid orderId)
        {
            return await _ctx.Orders
                 .FirstOrDefaultAsync(o => o.Id == orderId);
        }
    }
}
