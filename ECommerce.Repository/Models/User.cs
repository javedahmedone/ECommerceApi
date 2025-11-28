using System;
using System.Collections.Generic;

namespace ECommerce.Repository.Models;

public partial class User
{
    public long Id { get; set; }

    public string Name { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Passwordhash { get; set; } = null!;

    public int Role { get; set; }

    public DateTime CreatedDate { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual UserRole RoleNavigation { get; set; } = null!;
}
