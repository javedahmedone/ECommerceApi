using System;
using System.Collections.Generic;

namespace ECommerce.Repository.Models;

public partial class Category
{
    public long Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public long? CreatedBy { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
