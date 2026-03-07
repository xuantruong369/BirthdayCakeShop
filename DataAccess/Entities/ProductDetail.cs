using System;
using System.Collections.Generic;

namespace DataAccess.Entities;

public partial class ProductDetail
{
    public string ProductDetailId { get; set; } = null!;

    public string? ProductId { get; set; }

    public string? CakeSize { get; set; }

    public string? Flavor { get; set; }

    public decimal? Price { get; set; }

    public decimal? Discount { get; set; }

    public int? Stock { get; set; }

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

    public virtual Product? Product { get; set; }
}
