using System;
using System.Collections.Generic;

namespace EF_DatabaseFirstApproach.Models;

public partial class ProductCategory
{
    public long ProductId { get; set; }

    public long CategoryId { get; set; }

    public bool IsActive { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime CreatedDate { get; set; }

    public string? ModifiedBy { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public virtual Category Category { get; set; } = null!;

    public virtual Product Product { get; set; } = null!;
}
