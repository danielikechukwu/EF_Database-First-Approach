using System;
using System.Collections.Generic;

namespace EF_DatabaseFirstApproach.Models;

public partial class FailureLog
{
    public long Id { get; set; }

    public long? OrderId { get; set; }

    public long? CustomerId { get; set; }

    public long? ProductId { get; set; }

    public long? PaymentId { get; set; }

    public string MethodName { get; set; } = null!;

    public string ClassName { get; set; } = null!;

    public string ErrorMessage { get; set; } = null!;

    public string StackTrace { get; set; } = null!;

    public string? RequestPayloadJson { get; set; }

    public string Severity { get; set; } = null!;

    public string? CorrelationId { get; set; }

    public string Environment { get; set; } = null!;

    public DateTime LoggedAt { get; set; }

    public virtual Customer? Customer { get; set; }

    public virtual Order? Order { get; set; }

    public virtual Payment? Payment { get; set; }

    public virtual Product? Product { get; set; }
}
