using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Business.DTOs
{


    public class OrderCreateDto
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }

    public class OrderResponseDto
    {
        public Guid Id { get; set; }
        public decimal TotalAmount { get; set; }
        public string Status { get; set; }
        public DateTime OrderDate { get; set; }
        public long OrderBy { get; set; }
        public string CustomerEmailAddress { get; set; }
    }

    public class OrderItemDetailDto
    {
        public string ProductName { get; set; }
        public long? Quantity { get; set; }
        public double? OrderPrice { get; set; }
    }

}
