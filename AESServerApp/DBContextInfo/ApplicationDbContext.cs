using AESServerApp.Models;
using Microsoft.EntityFrameworkCore;

namespace AESServerApp.DBContextInfo
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<ClientKeyIV> ClientkeyIVs { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Configure unique index for ClientId in ClientkeyIV entity
            modelBuilder.Entity<ClientKeyIV>()
                .HasIndex(c => c.ClientId).IsUnique();

            modelBuilder.Entity<User>().HasData(
                new User { Id = 1, Name = "CodeAdmirer", Email = "ABC@gmail.com" },
                new User { Id = 2, Name = "abcdCodeAdmirer", Email = "ABCd@gmail.com" },
                new User { Id = 3, Name = "abcdeCodeAdmirer", Email = "ABCde@gmail.com" }
                );
        }
    }
}