using AutoMapperUsageInWebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace AutoMapperUsageInWebAPI.Data
{
    public class EComDBContext : DbContext
    {
        public EComDBContext(DbContextOptions<EComDBContext> options) : base(options)
        {
            
        }

        public DbSet<Customer> MyCustomers { get; set; }

        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }

        public DbSet<OrderItem> OrdersItem { get; set;}
        public DbSet<Address> Address { get; set; }


        //Migration Commands

        //Add-Migration InitialCreate_EComDBContext -Context EComDBContext
        //Update-Database -Context EComDBContext
        //Remove-Migration -Context EComDBContext
        //Update-Database -Migration:0 -Context EComDBContext
        //Script-Migration -Context EComDBContext
        //Script-Migration -From 0 -Context EComDBContext
        //Script-Migration -To InitialCreate_EComDBContext -Context EComDBContext
        //Script-Migration -From 0 -To InitialCreate_EComDBContext -Context EComDBContext
        //Script-Migration -From 0 -To InitialCreate_EComDBContext -Idempotent -Context EComDBContext
        //Script-Migration -From 0 -To InitialCreate_EComDBContext -Idempotent -OutputPath "C:\temp\script.sql" -Context EComDBContext

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>().HasData(
                new Customer { CustomerId = 1, FirstName = "Ram", LastName = "Shiva", PhoneNumber = "02365987", Email = "Ram@abc.com" },
                 new Customer { CustomerId = 2, FirstName = "Hanu", LastName = "Man", PhoneNumber = "78945620", Email = "Hanu@abc.com" }
            );

            modelBuilder.Entity<Product>().HasData(
                new Product { Id = 1, CreatedDateTime = new DateTime(2025, 3, 17, 0, 0, 0, DateTimeKind.Utc), Brand ="Dell",Name = "Dell Laptop", Price = 10, Description = "This is latest Gen Laptop" },
                new Product { Id = 2, CreatedDateTime = new DateTime(2025, 3, 18, 0, 0, 0, DateTimeKind.Utc), Brand ="HP",Name = "Oneplus mobile", Price = 20, Description = "This 5G smart phone" }
            );

            modelBuilder.Entity<Address>().HasData(
                new Address { AddressId = 1,Latitude="41.2.5", Street = "123 Main St", City = "Anytown", State = "NY", ZipCode = "12345", CustomerId = 1 },
                new Address { AddressId = 2, Latitude = "41.2.5", Street = "456 Elm St", City = "Othertown", State = "CA", ZipCode = "67890", CustomerId = 2 }
            );

            modelBuilder.Entity<Order>().HasData(
                new Order { OrderId = 1, OrderDate = new DateTime(2025, 3, 17, 0, 0, 0, DateTimeKind.Utc), Amount = 10, CustomerId = 1 },
                new Order { OrderId = 2, OrderDate = new DateTime(2025, 3, 17, 0, 0, 0, DateTimeKind.Utc), Amount = 20, CustomerId = 2 }
            );

            modelBuilder.Entity<OrderItem>().HasData(
                new OrderItem { OrderItemId = 1, OrderId = 1, ProductId = 1, Quantity = 1, ProductPrice = 10 },
                new OrderItem { OrderItemId = 2, OrderId = 2, ProductId = 2, Quantity = 2, ProductPrice = 20 }
            );

        }
    }
}
