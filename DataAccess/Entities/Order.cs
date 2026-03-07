using System;
using System.Collections.Generic;

namespace DataAccess.Entities;

public partial class Order
{
    public string OrderId { get; set; } = null!;

    public DateOnly? OrderDate { get; set; }

    public string? CustomerId { get; set; }

    public decimal? TotalAmount { get; set; }

    public decimal? Discount { get; set; }

    public string? PaymentMethod { get; set; }

    public string? Note { get; set; }

    public virtual Customer? Customer { get; set; }

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
}
