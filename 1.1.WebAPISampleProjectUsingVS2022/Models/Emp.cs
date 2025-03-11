using System.ComponentModel.DataAnnotations;

namespace WebAPISampleProjectUsingVS2022.Models
{
    public class Emp
    {
        public int EmpNo { get; set; }
        [Required]
        [StringLength(10)]
        public string EmpName { get; set; }
        public string job { get; set; }
        [Range(1000,9999)]
        public int Salary  { get; set; }
    }
}
