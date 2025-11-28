using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Business.DTOs
{
    public class ProductCreateDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
        public int StockQuantity { get; set; }
    }

    public class ProductUpdateDto : ProductCreateDto
    {
        public int Id { get; set; }
        public bool isActive { get; set; }
    }

    public class ProductResponseDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string CategoryName { get; set; }
        public long CategoryId { get; set; }
        public long StockQuantity { get; set; }
        public bool IsActive { get; set; }
    }

}
