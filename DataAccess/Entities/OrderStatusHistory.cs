using System;
using System.Collections.Generic;

namespace DataAccess.Entities;

public partial class OrderStatusHistory
{
    public int HistoryId { get; set; }

    public int OrderId { get; set; }

    public string Status { get; set; } = null!;

    public DateTime? UpdateDate { get; set; }

    public string? Note { get; set; }

    public virtual Order Order { get; set; } = null!;
}
