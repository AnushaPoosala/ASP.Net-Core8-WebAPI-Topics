using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AutoMapperUsageInWebAPI.Models.DTOs
{
    public class OrderDTO
    {
        
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        
        public decimal Amount { get; set; }
        public string CustomerName { get; set; }
        public string CustomerEmail { get; set; } 
        public string CustomerPhoneNumber { get; set; }

        public AddressDTO Address { get; set; }
        public List<OrderItemDTO> OrderItems { get; set; }

    }
}
