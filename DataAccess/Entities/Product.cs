using System;
using System.Collections.Generic;

namespace DataAccess.Entities;

public partial class Product
{
    public string ProductId { get; set; } = null!;

    public string ProductName { get; set; } = null!;

    public string? CategoryId { get; set; }

    public string? Description { get; set; }

    public string? Thumbnail { get; set; }

    public decimal? MinPrice { get; set; }

    public decimal? MaxPrice { get; set; }

    public virtual CakeCategory? Category { get; set; }

    public virtual ICollection<ProductDetail> ProductDetails { get; set; } = new List<ProductDetail>();

    public virtual ICollection<ProductImage> ProductImages { get; set; } = new List<ProductImage>();
}
