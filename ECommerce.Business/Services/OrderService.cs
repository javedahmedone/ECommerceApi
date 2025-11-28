using ECommerce.Business.DTOs;
using ECommerce.Business.ExceptionHandlers;
using ECommerce.Business.Interfaces;
using ECommerce.Business.Mapper;
using ECommerce.Repository.Interfaces;
using ECommerce.Repository.Models;


namespace ECommerce.Business.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepo;
        private readonly IProductRepository _productRepo;
        private readonly IUserRepository _userRepo;
        private readonly IAuthService _authService;

        public OrderService(IOrderRepository o, IProductRepository p, IUserRepository u, IAuthService authService)
        {
            _orderRepo = o;
            _productRepo = p;
            _userRepo = u;
            _authService = authService;
        }

        public async Task<OrderResponseDto> PlaceOrderAsync(OrderCreateDto orderRequest)
        {
            if (orderRequest is null)
                throw new AppBadRequestException("Order request is empty");
            if(orderRequest.Quantity <= 0 )
                throw new AppBadRequestException("Quantity must be valid value");
            var customerId = _authService.GetUserId();
            var product = await _productRepo.GetByIdAsync(orderRequest.ProductId);
            if (product == null || !product.IsActive) 
                throw new AppBadRequestException($"Product {orderRequest.ProductId} not found");
            if (!await _productRepo.ReduceStockAsync(orderRequest.ProductId, orderRequest.Quantity))
                throw new AppBadRequestException($"Not enough stock for {product.Name}");
            var order = DtoRequestMapper.OderDtoToModel(orderRequest, product, customerId);           
            await _orderRepo.AddAsync(order);
            return DTOsResponseMapper.OderDBModelToDTO(order);            
        }

        public async Task<bool> CancelOrderAsync(Guid orderId)
        {
            string role = _authService.GetRole();
            long userId = _authService.GetUserId();

            var order = await _orderRepo.GetOrderByOrderId(orderId);
            if (order is null) throw new AppBadRequestException("Order not found");

            if (order.CustomerId != userId &&  role != RoleEnum.ADMIN.ToString()) throw new AppBadRequestException("Only Admin can view other user's orders");


            if (order.Status != (int)OrderStateEnum.PENDING) throw new AppBadRequestException("Only pending orders can be cancelled");

            order.Status = (int)OrderStateEnum.CANCELLED;
           
            return await _orderRepo.UpdateAsync(order);
        }

        public async Task<IEnumerable<OrderResponseDto>> GetOrdersByCustomerIdAsync(long? customerId)
        {
            string role = _authService.GetRole();
            long userId = _authService.GetUserId();
            if (customerId.HasValue && role != RoleEnum.ADMIN.ToString() && userId != customerId)
                throw new AppBadRequestException("Only Admin can view other user's orders");
            else if(role  == RoleEnum.CUSTOMER.ToString())
                customerId = userId;
            var orders = await _orderRepo.GetOrdersByCustomerIdAsync(customerId!.Value);

            return orders.Select(o => new OrderResponseDto
            {
                Id = o.Id,
                TotalAmount = o.TotalAmount,
                OrderDate = o.OrderDate,
                Status = Enum.GetName(typeof(OrderStateEnum), o.Status),
                CustomerEmailAddress = o.Customer.Email,
                OrderBy = o.Customer.Id
                
            });
        }


        public async Task<bool> UpdateOrderStatusAsync(Guid orderId, int status)
        {
            string role = _authService.GetRole();
            if (role != RoleEnum.ADMIN.ToString()) throw new AppBadRequestException("Only Admin can update  user's orders");
            var order = await _orderRepo.GetOrderByOrderId(orderId);
            if (order is null) throw new AppBadRequestException("Order not found");
            if (order.Status != (int)OrderStateEnum.PENDING) throw new AppBadRequestException("Only pending orders can be modified");
            order.Status = status;
            return await _orderRepo.UpdateAsync(order);
        }
    }

}
