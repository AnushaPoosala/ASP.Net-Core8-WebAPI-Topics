using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoServerAppForHMAC.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(60)]
        public string UserName { get; set; }

        [Required]
        [MaxLength(60)]
        public string UserRole { get; set; }

        [Column(TypeName="decimal(18,2)")]
        public decimal Salary { get; set; }
    }
}
