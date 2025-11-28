using System;
using System.Collections.Generic;

namespace ECommerce.Repository.Models;

public partial class UserRole
{
    public int Id { get; set; }

    public string Role { get; set; } = null!;

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
