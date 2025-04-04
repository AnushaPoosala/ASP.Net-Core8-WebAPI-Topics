using DemoServerAppForHMAC.Models;
using Microsoft.EntityFrameworkCore;

namespace DemoServerAppForHMAC.Context
{
    public class ServerAppDbContext : DbContext
    {
        public ServerAppDbContext(DbContextOptions<ServerAppDbContext> options) : base(options)
        {

        }
        public DbSet<Models.User> Users { get; set; }
        public DbSet<Models.ClientInfo> ClientInfos { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Models.User>().HasData(
                new Models.User
                {
                    Id = 1,
                    UserName = "Rama",
                    UserRole = "Software Engineer",
                    Salary = 10000
                },
                new Models.User
                {
                    Id = 2,
                    UserName = "Krishna",
                    UserRole = "Manager",
                    Salary = 500000
                }
            );

            modelBuilder.Entity<Models.ClientInfo>().HasData(
                new Models.ClientInfo
                {
                    Id = 1,
                    ClientId = "WebApp",
                    ClientName = "XYZ Company",
                    ClientSecretKey = "XYZCompany123",
                    ClientSecretSalt = "Test"
                    //ClientSecretKey = SamplePasswordHasher.CreatePasswordHash("WebAppXYZCompany", out byte[] passwordHash, out byte[] passwordSalt),
                    //ClientSecretSalt = passwordSalt
                    //Due to limitataion of EF Core uding of Dynamic we must go with static values. So we are using above lines of code

                },
                new Models.ClientInfo
                {
                    Id = 2,
                    ClientId = "MobileApp",
                    ClientName = "ABC Company",
                    ClientSecretKey = "XYZCompany123",
                    ClientSecretSalt = "Test"
                
                }
                ,
                new Models.ClientInfo
                {
                    Id = 3,
                    ClientId = "DeskTop",
                    ClientName = "123 Company",
                    ClientSecretKey = "XYZCompany123",
                    ClientSecretSalt = "Test"
                }
            );
        }
    }
}
