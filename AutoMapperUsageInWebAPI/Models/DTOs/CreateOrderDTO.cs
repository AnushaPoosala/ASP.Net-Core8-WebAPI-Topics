using System.ComponentModel.DataAnnotations;

namespace AutoMapperUsageInWebAPI.Models.DTOs
{
    public class CreateOrderDTO
    {
        [Required]
         public int CustomerId {  get; set; }
        [Required]
        public List<CreateOrderItemDTO> OrderItems { get; set; }
    }
}
