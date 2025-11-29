using System;
using System.Collections.Generic;

namespace ECommerce.Repository.Models;

public partial class Product
{
    public long Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public decimal Price { get; set; }

    public long CategoryId { get; set; }

    public long StockQuantity { get; set; }

    public bool IsActive { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public long? CreatedBy { get; set; }

    public virtual Category Category { get; set; } = null!;

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
