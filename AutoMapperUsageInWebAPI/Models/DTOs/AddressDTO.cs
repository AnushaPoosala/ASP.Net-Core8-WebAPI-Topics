using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AutoMapperUsageInWebAPI.Models.DTOs
{
    public class AddressDTO
    {
        public string Street { get; set; }
      
        public string City { get; set; }
       
        public string State { get; set; }
       
        public string ZipCode { get; set; }
    }
}
