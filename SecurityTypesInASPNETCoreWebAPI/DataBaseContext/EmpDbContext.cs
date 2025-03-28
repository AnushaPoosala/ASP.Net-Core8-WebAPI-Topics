using Microsoft.EntityFrameworkCore;
using SecurityTypesInASPNETCoreWebAPI.Models;

namespace SecurityTypesInASPNETCoreWebAPI.DataBaseContext
{
    public class EmpDbContext : DbContext
    {
        public EmpDbContext(DbContextOptions<EmpDbContext> options)  :base(options)
        {
            
        }

        public DbSet<Employee> Employees { get; set; }
    }
}
