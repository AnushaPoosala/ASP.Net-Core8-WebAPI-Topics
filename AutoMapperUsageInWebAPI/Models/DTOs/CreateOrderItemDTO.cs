using System.ComponentModel.DataAnnotations;

namespace AutoMapperUsageInWebAPI.Models.DTOs
{
    public class CreateOrderItemDTO
    {
        [Required]
        public int ProductId { get; set; }
        [Required]
        public int Quantity { get; set; }
    }
}
