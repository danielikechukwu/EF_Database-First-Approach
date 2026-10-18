using System;
using System.Collections.Generic;

namespace EF_DatabaseFirstApproach.Models;

public partial class CustomerRole
{
    public long CustomerId { get; set; }

    public long RoleId { get; set; }

    public bool IsActive { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime CreatedDate { get; set; }

    public string? ModifiedBy { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public virtual Customer Customer { get; set; } = null!;

    public virtual Role Role { get; set; } = null!;
}
