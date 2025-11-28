using ECommerce.Repository.Models;

namespace ECommerce.Repository.Interfaces
{
    public interface IOrderRepository
    {
        Task<IEnumerable<Order>?> GetOrdersByCustomerIdAsync(long id);
        Task<Order> GetOrderByOrderId(Guid orderId);

        Task<List<Order>> GetAllAsync();
        Task<Order> AddAsync(Order order);
        Task<bool> UpdateAsync(Order order);
        Task<bool> DeleteAsync(int id);
    }
}
