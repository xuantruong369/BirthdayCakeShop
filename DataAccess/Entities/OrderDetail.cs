using System;
using System.Collections.Generic;

namespace DataAccess.Entities;

public partial class OrderDetail
{
    public int Id { get; set; }

    public string? OrderId { get; set; }

    public string? ProductDetailId { get; set; }

    public int? Quantity { get; set; }

    public decimal? UnitPrice { get; set; }

    public virtual Order? Order { get; set; }

    public virtual ProductDetail? ProductDetail { get; set; }
}
