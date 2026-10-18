using System;
using System.Collections.Generic;

namespace EF_DatabaseFirstApproach.Models;

public partial class Payment
{
    public long PaymentId { get; set; }

    public long OrderId { get; set; }

    public decimal Amount { get; set; }

    public string CurrencyCode { get; set; } = null!;

    public int PaymentStatusId { get; set; }

    public int PaymentMethodId { get; set; }

    public string? PaymentGateway { get; set; }

    public int AttemptNumber { get; set; }

    public string? MerchantTransactionId { get; set; }

    public string? GatewayTransactionId { get; set; }

    public string? UpiVpa { get; set; }

    public string? CardLast4 { get; set; }

    public string? CardNetwork { get; set; }

    public string? BankReferenceNo { get; set; }

    public string? GatewayRequestJson { get; set; }

    public string? GatewayResponseJson { get; set; }

    public DateTime PaymentInitiatedAt { get; set; }

    public DateTime? PaymentCompletedAt { get; set; }

    public bool IsActive { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime CreatedDate { get; set; }

    public string? ModifiedBy { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public virtual ICollection<FailureLog> FailureLogs { get; set; } = new List<FailureLog>();

    public virtual Order Order { get; set; } = null!;

    public virtual PaymentMethodsMaster PaymentMethod { get; set; } = null!;

    public virtual PaymentStatusMaster PaymentStatus { get; set; } = null!;

    public virtual ICollection<Refund> Refunds { get; set; } = new List<Refund>();
}
