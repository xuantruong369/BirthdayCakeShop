using System;
using System.Collections.Generic;

namespace DataAccess.Entities;

public partial class ProductImage
{
    public int ImageId { get; set; }

    public string? ProductId { get; set; }

    public string? ImageFile { get; set; }

    public int? DisplayOrder { get; set; }

    public virtual Product? Product { get; set; }
}
