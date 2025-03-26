using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AutoMapperUsageInWebAPI.Models.DTOs
{
    public class OrderItemDTO
    {

        public string ProductName { get; set; }
        
       
        public decimal ProductPrice { get; set; }
    
        public int Quantity { get; set; }
    
        public decimal TotalPrice { get; set; }
    }
}
