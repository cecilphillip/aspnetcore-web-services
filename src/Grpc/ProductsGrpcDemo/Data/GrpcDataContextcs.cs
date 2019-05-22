using Microsoft.EntityFrameworkCore;
using Products;

namespace ProductsGrpcDemo.Data
{
    public class GrpcDataContext: DbContext
    {
        public GrpcDataContext(DbContextOptions<GrpcDataContext> options): base(options)
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