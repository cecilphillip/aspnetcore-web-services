using Microsoft.EntityFrameworkCore;
using ProductsApiDemo.Models;

namespace ProductsApiServiceDemo.Data
{
    public class DemoApiDbContext: DbContext
    {
        public DemoApiDbContext(DbContextOptions<DemoApiDbContext> options): base(options)
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
