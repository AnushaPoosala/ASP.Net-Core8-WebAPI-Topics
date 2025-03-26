using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AutoMapperUsageInWebAPI.Models
{
    public class Address
    {
        [Key]
        public int AddressId { get; set; }
        [Required, MaxLength(150)]
        public string Street { get; set; }
        [Required, MaxLength(50)]
        public string City { get; set; }
        [Required, MaxLength(50)]
        public string State { get; set; }
        [Required, MaxLength(10)]
        public string ZipCode { get; set; }

        public string Latitude { get; set; }

        public int CustomerId { get; set; }
        [ForeignKey("CustomerId")]
        public Customer Customer { get; set; }

    }
}
