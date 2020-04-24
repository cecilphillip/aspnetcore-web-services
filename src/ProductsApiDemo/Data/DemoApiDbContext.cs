using Microsoft.EntityFrameworkCore;
using ProductsApiDemo.Models;

namespace ProductsApiDemo.Data
{
    public class DemoApiDbContext : DbContext
    {
        public DemoApiDbContext(DbContextOptions<DemoApiDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
                .HasKey(p => p.Id);

            modelBuilder.Entity<Product>()
               .HasData(DataGenerator.GetProducts());
        }
    }
}
