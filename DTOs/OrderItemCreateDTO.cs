using System.ComponentModel.DataAnnotations;

namespace EF_DatabaseFirstApproach.DTOs
{
    public class OrderItemCreateDTO
    {
        [Required(ErrorMessage = "ProductId is required.")]
        public long ProductId { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1.")]
        public int Quantity { get; set; }
    }
}
