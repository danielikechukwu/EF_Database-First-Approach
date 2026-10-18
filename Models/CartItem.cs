using System;
using System.Collections.Generic;

namespace EF_DatabaseFirstApproach.Models;

public partial class CartItem
{
    public long CartItemId { get; set; }

    public long CartId { get; set; }

    public long ProductId { get; set; }

    public string ProductName { get; set; } = null!;

    public string Sku { get; set; } = null!;

    public string? BrandName { get; set; }

    public string? ProductAttributeJson { get; set; }

    public string? ThumbnailImageUrl { get; set; }

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

    public bool IsActive { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime CreatedDate { get; set; }

    public string? ModifiedBy { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public virtual ShoppingCart Cart { get; set; } = null!;

    public virtual Product Product { get; set; } = null!;
}
