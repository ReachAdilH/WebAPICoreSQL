using Microsoft.EntityFrameworkCore;
using WebAPICoreSQL.Model;

namespace WebAPICoreSQL.Data
{
    public class AppDbContext : DbContext
    {
       // public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }


        public DbSet<Product> Product { get; set; }
        public DbSet<Employee> Employee { get; set; }

    }
}
