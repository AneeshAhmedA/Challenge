using Challenge.Entity;
using Microsoft.EntityFrameworkCore;

namespace Challenge.Database
{
    public class MyDbContext : DbContext
    {
        private readonly IConfiguration _configuration; 

        public MyDbContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }

        public DbSet<Item> Items { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_configuration.GetConnectionString("MyConnection"));
        }
    }
}
