using System.ComponentModel.DataAnnotations;

namespace SecurityTypesInASPNETCoreWebAPI.Models.DTO
{
    public class EmpRegistrationDTO
    {
        [Required(ErrorMessage = "First name is required")]
        [StringLength(50, ErrorMessage = "First Name cannot exceed 50 chars")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "First name should only contains the alpha letters")]
        public string FirstName { get; set; }


        [Required(ErrorMessage = "Last name is required")]
        [StringLength(50, ErrorMessage = "Last Name cannot exceed 50 chars")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "First name should only contains the alpha letters")]
        public string LastName { get; set; }
        public string Department { get; set; }
        public string Description { get; set; }


        [Required(ErrorMessage = "Email is required")]
        [StringLength(50, ErrorMessage = "Email cannot exceed 50 chars")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }


        [Required(ErrorMessage = "Password is required")]
        [StringLength(10, ErrorMessage = "Password cannot exceed 10 chars and min 6", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
