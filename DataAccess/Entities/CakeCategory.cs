using System;
using System.Collections.Generic;

namespace DataAccess.Entities;

public partial class CakeCategory
{
    public string CategoryId { get; set; } = null!;

    public string CategoryName { get; set; } = null!;

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
