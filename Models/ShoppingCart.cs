using System;
using System.Collections.Generic;

namespace EF_DatabaseFirstApproach.Models;

public partial class ShoppingCart
{
    public long CartId { get; set; }

    public long CustomerId { get; set; }

    public bool IsCurrent { get; set; }

    public bool IsActive { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime CreatedDate { get; set; }

    public string? ModifiedBy { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public virtual ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();

    public virtual Customer Customer { get; set; } = null!;
}
