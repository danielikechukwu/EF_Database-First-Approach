using System;
using System.Collections.Generic;

namespace EF_DatabaseFirstApproach.Models;

public partial class DeliveryPartner
{
    public long PartnerId { get; set; }

    public string PartnerName { get; set; } = null!;

    public string? TrackingUrl { get; set; }

    public string? DisplayName { get; set; }

    public bool IsActive { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime CreatedDate { get; set; }

    public string? ModifiedBy { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public virtual ICollection<Shipment> Shipments { get; set; } = new List<Shipment>();
}
