using System.ComponentModel.DataAnnotations;

namespace AutoMapperUsageInWebAPI.Models
{
    public class Customer
    {
        [Key]
        public int CustomerId { get; set; }
        [Required, MaxLength(50)]
        public string FirstName { get; set; }
        [Required, MaxLength(50)]
        public string LastName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        public Address Address { get; set; }

        public List<Order> Orders { get; set; }
    }
}
