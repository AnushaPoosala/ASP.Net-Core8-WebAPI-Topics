using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AutoMapperUsageInWebAPI.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Range(1, 1000)]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Price { get; set; }
        //SerailNumber is like Category-Brand-Model-Year-ProductId
        public string? SerialNumber { get; set; } //ELE-DELL-Laptop-2025-001
        public bool IsActive { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }
        public string Brand { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public DateTime UpdatedDateTime { get; set; }


        //Inrernal or confidential data fields be hidden from the response/customers
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? SupplierPrice { get; set; }

        public string? SupplierName { get; set; }
        [Range(0, int.MaxValue)]
        public int StockQuantity { get; set; }



    }
}
