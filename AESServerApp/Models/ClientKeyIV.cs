using System.ComponentModel.DataAnnotations;

namespace AESServerApp.Models
{
    //REpresents the IV(Initialization VEctor) used in AES encryption
    public class ClientKeyIV
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string ClientId { get; set; }

        [Required]
        public string Key { get; set; }

        [Required]
        public string IV { get; set; }
    }
}
