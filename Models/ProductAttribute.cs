using System;
using System.Collections.Generic;

namespace EF_DatabaseFirstApproach.Models;

public partial class ProductAttribute
{
    public long ProductAttributeId { get; set; }

    public long ProductId { get; set; }

    public string AttributeName { get; set; } = null!;

    public string AttributeValue { get; set; } = null!;

    public bool IsActive { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime CreatedDate { get; set; }

    public string? ModifiedBy { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public virtual Product Product { get; set; } = null!;
}
