using System;
using System.Collections.Generic;

namespace ECommerce.Repository.Models;

public partial class Order
{
    public long CustomerId { get; set; }

    public DateTime OrderDate { get; set; }

    public decimal TotalAmount { get; set; }

    public int Status { get; set; }

    public Guid Id { get; set; }

    public long? ProductId { get; set; }

    public double? OrderPrice { get; set; }

    public int? Quantity { get; set; }

    public virtual User Customer { get; set; } = null!;

    public virtual Product? Product { get; set; }

    public virtual OrderStatus StatusNavigation { get; set; } = null!;
}
