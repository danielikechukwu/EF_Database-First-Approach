using System;
using System.Collections.Generic;

namespace EF_DatabaseFirstApproach.Models;

public partial class Shipment
{
    public long ShipmentId { get; set; }

    public long OrderId { get; set; }

    public long PartnerId { get; set; }

    public string? TrackingNumber { get; set; }

    public DateTime? ShippedDate { get; set; }

    public DateTime? DeliveredDate { get; set; }

    public int ShipmentStatusId { get; set; }

    public bool IsActive { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime CreatedDate { get; set; }

    public string? ModifiedBy { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public virtual Order Order { get; set; } = null!;

    public virtual DeliveryPartner Partner { get; set; } = null!;

    public virtual ShipmentStatusMaster ShipmentStatus { get; set; } = null!;
}
