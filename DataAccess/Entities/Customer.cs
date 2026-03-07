using System;
using System.Collections.Generic;

namespace DataAccess.Entities;

public partial class Customer
{
    public string CustomerId { get; set; } = null!;

    public string? Username { get; set; }

    public string? CustomerName { get; set; }

    public DateOnly? BirthDate { get; set; }

    public string? Phone { get; set; }

    public string? Address { get; set; }

    public string? CustomerType { get; set; }

    public string? Avatar { get; set; }

    public string? Note { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual User? UsernameNavigation { get; set; }
}
