using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Business.DTOs
{
    public class CategoryCreateDto
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
    }

    public class CategoryUpdateDto : CategoryCreateDto
    {
        public int Id { get; set; }
    }

    public class CategoryResponseDto
    {
        public long Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }

}
