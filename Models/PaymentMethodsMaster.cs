using System;
using System.Collections.Generic;

namespace EF_DatabaseFirstApproach.Models;

public partial class PaymentMethodsMaster
{
    public int PaymentMethodId { get; set; }

    public string MethodCode { get; set; } = null!;

    public string DisplayName { get; set; } = null!;

    public string? Description { get; set; }

    public bool IsActive { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime CreatedDate { get; set; }

    public string? UpdatedBy { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();
}
