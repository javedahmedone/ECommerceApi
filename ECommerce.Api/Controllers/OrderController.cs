using ECommerce.Business.DTOs;
using ECommerce.Business.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost("PlaceOrder")]
        public async Task<IActionResult> PlaceOrder( [FromBody] OrderCreateDto orderRequest)
        {
            var result = await _orderService.PlaceOrderAsync(orderRequest);
            if (result == null)
                return BadRequest("Order could not be placed.");

            return Ok(result);
        }

        [HttpPut("CancelOrder/{orderId}")]
        public async Task<IActionResult> CancelOrder(Guid orderId)
        {
            var success = await _orderService.CancelOrderAsync(orderId);          

            return Ok(new { Message = "Order cancelled successfully." });
        }

        [HttpGet("UserId/{customerId}")]
        public async Task<IActionResult> GetOrdersByCustomerIdAsync(long? customerId)
        {
            var orders = await _orderService.GetOrdersByCustomerIdAsync(customerId);
            return Ok(orders);
        }

        [HttpPut("Status/{orderId}")]
        public async Task<IActionResult> UpdateStatus(Guid orderId, [FromQuery] int status)
        {
            var success = await _orderService.UpdateOrderStatusAsync(orderId, status);
            if (!success)
                return BadRequest("Unable to update status.");
            return Ok(new { Message = "Order status updated." });
        }
    }
}
