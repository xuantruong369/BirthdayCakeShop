using System;
using System.Collections.Generic;

namespace DataAccess.Entities;

public partial class ProductDetail
{
    public int ProductDetailId { get; set; }

    public int ProductId { get; set; }

    public string? CakeSize { get; set; }

    public string? Flavor { get; set; }

    public decimal Price { get; set; }

    public decimal? Discount { get; set; }

    public int? Stock { get; set; }

    public virtual ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();

    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    public virtual Product Product { get; set; } = null!;
}
