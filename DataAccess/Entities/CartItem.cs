using System;
using System.Collections.Generic;

namespace DataAccess.Entities;

public partial class CartItem
{
    public int CartItemId { get; set; }

    public int? CartId { get; set; }

    public int? ProductDetailId { get; set; }

    public int? Quantity { get; set; }

    public string? CakeText { get; set; }

    public virtual Cart? Cart { get; set; }

    public virtual ProductDetail? ProductDetail { get; set; }
}
