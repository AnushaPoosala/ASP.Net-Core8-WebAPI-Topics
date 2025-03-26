using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace AutoMapperUsageInWebAPI.Models
{
    public class OrderItem
    {
        [Key]
        public int OrderItemId { get; set; }
        public int ProductId { get; set; }
        [ForeignKey("ProductId")]
        public Product Product { get; set; }

        [Required]
        public int Quantity {  get; set; }
        [Required]
        [Column(TypeName ="decimal(18,2")]
        public  decimal ProductPrice { get; set; }
        [Required]
        public int OrderId {  get; set; }
        [ForeignKey("OrderId")]
        public Order Order { get; set; }

    }
}
