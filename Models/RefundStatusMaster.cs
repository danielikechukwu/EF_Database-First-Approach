using System;
using System.Collections.Generic;

namespace EF_DatabaseFirstApproach.Models;

public partial class RefundStatusMaster
{
    public int RefundStatusId { get; set; }

    public string StatusName { get; set; } = null!;

    public string? Description { get; set; }

    public string DisplayName { get; set; } = null!;

    public bool IsActive { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime CreatedDate { get; set; }

    public string? ModifiedBy { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public virtual ICollection<Refund> Refunds { get; set; } = new List<Refund>();
}
