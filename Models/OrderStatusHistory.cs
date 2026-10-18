using System;
using System.Collections.Generic;

namespace EF_DatabaseFirstApproach.Models;

public partial class OrderStatusHistory
{
    public long OrderStatusHistoryId { get; set; }

    public long OrderId { get; set; }

    public int? OldStatusId { get; set; }

    public int NewStatusId { get; set; }

    public DateTime ChangedAt { get; set; }

    public string? Remarks { get; set; }

    public bool IsActive { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime CreatedDate { get; set; }

    public string? ModifiedBy { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public virtual OrderStatusMaster NewStatus { get; set; } = null!;

    public virtual OrderStatusMaster? OldStatus { get; set; }

    public virtual Order Order { get; set; } = null!;
}
