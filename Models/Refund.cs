using System;
using System.Collections.Generic;

namespace EF_DatabaseFirstApproach.Models;

public partial class Refund
{
    public long RefundId { get; set; }

    public long PaymentId { get; set; }

    public long OrderId { get; set; }

    public decimal RefundAmount { get; set; }

    public string CurrencyCode { get; set; } = null!;

    public int RefundStatusId { get; set; }

    public int AttemptNumber { get; set; }

    public string? RefundTransactionId { get; set; }

    public string? GatewayReferenceId { get; set; }

    public string? RefundReason { get; set; }

    public string? InitiatedBy { get; set; }

    public bool IsAutoRefund { get; set; }

    public string? GatewayRequestJson { get; set; }

    public string? GatewayResponseJson { get; set; }

    public DateTime RefundInitiatedAt { get; set; }

    public DateTime? RefundCompletedAt { get; set; }

    public bool IsActive { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime CreatedDate { get; set; }

    public string? ModifiedBy { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public virtual Order Order { get; set; } = null!;

    public virtual Payment Payment { get; set; } = null!;

    public virtual RefundStatusMaster RefundStatus { get; set; } = null!;
}
