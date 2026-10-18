using System;
using System.Collections.Generic;

namespace EF_DatabaseFirstApproach.Models;

public partial class Review
{
    public long ReviewId { get; set; }

    public long ProductId { get; set; }

    public long CustomerId { get; set; }

    public int Rating { get; set; }

    public string? ReviewText { get; set; }

    public bool IsActive { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime CreatedDate { get; set; }

    public string? ModifiedBy { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public virtual Customer Customer { get; set; } = null!;

    public virtual Product Product { get; set; } = null!;
}
