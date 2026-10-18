using System;
using System.Collections.Generic;

namespace EF_DatabaseFirstApproach.Models;

public partial class StockTransaction
{
    public long StockTransactionId { get; set; }

    public long ProductId { get; set; }

    public int StockTransactionTypeId { get; set; }

    public int QuantityChange { get; set; }

    public long? ReferenceId { get; set; }

    public string? ReferenceType { get; set; }

    public int OldStock { get; set; }

    public int NewStock { get; set; }

    public string? Remarks { get; set; }

    public DateTime TransactionDate { get; set; }

    public bool IsActive { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime CreatedDate { get; set; }

    public string? ModifiedBy { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public virtual Product Product { get; set; } = null!;

    public virtual StockTransactionMaster StockTransactionType { get; set; } = null!;
}
