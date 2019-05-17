using Microsoft.EntityFrameworkCore;
using ProductsODataApiDemo.Models;

namespace ProductsODataApiDemo.Data
{
    public class DemoODataApiDbContext: DbContext
    {
        public DemoODataApiDbContext(DbContextOptions<DemoODataApiDbContext> options): base(options)
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
