using System;
using System.Collections.Generic;

namespace EF_DatabaseFirstApproach.Models;

public partial class Order
{
    public long OrderId { get; set; }

    public long CustomerId { get; set; }

    public int OrderStatusId { get; set; }

    public DateTime OrderDate { get; set; }

    public string ShippingAddressJson { get; set; } = null!;

    public string BillingAddressJson { get; set; } = null!;

    public decimal ItemsTotal { get; set; }

    public decimal OrderDiscountTotal { get; set; }

    public decimal OrderTaxTotal { get; set; }

    public decimal ShippingTotal { get; set; }

    public decimal PlatformFee { get; set; }

    public decimal GrandTotal { get; set; }

    public bool IsActive { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime CreatedDate { get; set; }

    public string? ModifiedBy { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public virtual Customer Customer { get; set; } = null!;

    public virtual ICollection<FailureLog> FailureLogs { get; set; } = new List<FailureLog>();

    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    public virtual OrderStatusMaster OrderStatus { get; set; } = null!;

    public virtual ICollection<OrderStatusHistory> OrderStatusHistories { get; set; } = new List<OrderStatusHistory>();

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();

    public virtual ICollection<Refund> Refunds { get; set; } = new List<Refund>();

    public virtual ICollection<Shipment> Shipments { get; set; } = new List<Shipment>();
}
