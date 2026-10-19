using System.ComponentModel.DataAnnotations;

namespace EF_DatabaseFirstApproach.DTOs
{
    public class OrderCreateDTO
    {
        [Required(ErrorMessage = "CustomerId is required.")]
        public long CustomerId { get; set; }

        [Required(ErrorMessage = "ShippingAddressId is required.")]
        public long ShippingAddressId { get; set; }

        [Required(ErrorMessage = "BillingAddressId is required.")]
        public long BillingAddressId { get; set; }

        [Required(ErrorMessage = "OrderItems are required.")]
        public List<OrderItemCreateDTO> OrderItems { get; set; } = new();
    }
}
