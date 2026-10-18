using System;
using System.Collections.Generic;

namespace EF_DatabaseFirstApproach.Models;

public partial class OrderItem
{
    public long OrderItemId { get; set; }

    public long OrderId { get; set; }

    public long ProductId { get; set; }

    public string ProductName { get; set; } = null!;

    public string Sku { get; set; } = null!;

    public string? BrandName { get; set; }

    public string? ProductAttributesJson { get; set; }

    public decimal Mrp { get; set; }

    public decimal SellingPrice { get; set; }

    public decimal DiscountPercent { get; set; }

    public decimal DiscountFlat { get; set; }

    public decimal DiscountTotal { get; set; }

    public decimal PriceAfterDiscount { get; set; }

    public decimal TaxPercent { get; set; }

    public decimal TaxAmount { get; set; }

    public decimal FinalPrice { get; set; }

    public int Quantity { get; set; }

    public decimal? LineTotal { get; set; }

    public bool IsActive { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime CreatedDate { get; set; }

    public string? ModifiedBy { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public virtual Order Order { get; set; } = null!;

    public virtual Product Product { get; set; } = null!;
}
