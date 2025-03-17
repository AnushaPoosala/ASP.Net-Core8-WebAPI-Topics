using AutoMapperUsageInWebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace AutoMapperUsageInWebAPI.Data
{
    public class ProductDbContext  : DbContext
    {
        public ProductDbContext(DbContextOptions<ProductDbContext> options) : base(options)
        {

        }

        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    Id = 1,
                    Name = "Laptop" ,
                    Price=1000,
                    SerialNumber="ELE-Dell-Laptop-2025-001",
                    IsActive=true,
                    Brand="Dell",
                    CategoryId=1,
                    CreatedDateTime= new DateTime(2025,3,17,0,0,0,DateTimeKind.Utc),
                    Description="Dell Laptop",
                    SupplierName="MainITSupplier",
                    SupplierPrice=900,
                    StockQuantity=100,
                } ,
                new Product 
                {
                Id = 2,
                    Name = "Mobile" ,
                    Price = 500,
                    SerialNumber = "Apple-2025-001",
                    IsActive = true,
                    Brand = "Apple",
                    CategoryId = 1,
                    CreatedDateTime = new DateTime(2025, 3, 16, 0, 0, 0, DateTimeKind.Utc),
                    Description = "IPhone",
                    SupplierName = "MainITSupplier",
                    SupplierPrice = 300,
                    StockQuantity = 100,
                }

                );       

        }
    }
}
