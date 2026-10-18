using System;
using System.Collections.Generic;

namespace EF_DatabaseFirstApproach.Models;

public partial class Product
{
    public long ProductId { get; set; }

    public long? BrandId { get; set; }

    public string ProductName { get; set; } = null!;

    public string Sku { get; set; } = null!;

    public string? Description { get; set; }

    public bool IsDiscountActive { get; set; }

    public bool IsTaxIncluded { get; set; }

    public decimal Mrp { get; set; }

    public decimal BasePrice { get; set; }

    public decimal? DiscountPercent { get; set; }

    public decimal? FlatDiscount { get; set; }

    public decimal? DiscountAmount { get; set; }

    public decimal? FinalPrice { get; set; }

    public decimal? TaxPercent { get; set; }

    public decimal? TaxAmount { get; set; }

    public decimal? PayablePrice { get; set; }

    public int StockQuantity { get; set; }

    public bool IsActive { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime CreatedDate { get; set; }

    public string? ModifiedBy { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public virtual Brand? Brand { get; set; }

    public virtual ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();

    public virtual ICollection<FailureLog> FailureLogs { get; set; } = new List<FailureLog>();

    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    public virtual ICollection<ProductAttribute> ProductAttributes { get; set; } = new List<ProductAttribute>();

    public virtual ICollection<ProductCategory> ProductCategories { get; set; } = new List<ProductCategory>();

    public virtual ICollection<ProductImage> ProductImages { get; set; } = new List<ProductImage>();

    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();

    public virtual ICollection<StockTransaction> StockTransactions { get; set; } = new List<StockTransaction>();
}
