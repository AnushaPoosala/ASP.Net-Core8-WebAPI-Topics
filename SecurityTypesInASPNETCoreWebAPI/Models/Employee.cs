using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;


namespace SecurityTypesInASPNETCoreWebAPI.Models
{
    [Index(nameof(Email), Name="Index_Email", IsUnique=true)]
    public class Employee
    {
        [Key]
        public int EmpId { get; set; }

        [Required(ErrorMessage ="First name is required")]
        [StringLength(50,ErrorMessage ="First Name cannot  exceed 50 length chars")]
        public string FirstName { get; set; }


        [Required(ErrorMessage = "Last name is required")]
        [StringLength(50, ErrorMessage = "Last Name cannot  exceed 50 length chars")]
        public string LastName { get; set; }


        public string Description { get; set; }
        public string Department { get; set; }

        [Required(ErrorMessage = "Email id is required")]
        [StringLength(50, ErrorMessage = "First Name cannot  exceed 50 length chars")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
    }
}
