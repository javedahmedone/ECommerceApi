using ECommerce.Business.DTOs;

namespace ECommerce.Business.Interfaces
{
    public interface IOrderService
    {
        Task<OrderResponseDto> PlaceOrderAsync( OrderCreateDto dto);
        Task<bool> CancelOrderAsync(Guid orderId);
        Task<IEnumerable<OrderResponseDto>> GetOrdersByCustomerIdAsync(long? customerId);
        Task<bool> UpdateOrderStatusAsync(Guid orderId, int status);
    }

}
