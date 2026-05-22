using Microsoft.EntityFrameworkCore;

namespace Product.Api.Infrastructure.Persistence;

public class ProductDbContext : DbContext
{
    public ProductDbContext(
       DbContextOptions<ProductDbContext>
       options)
       : base(options)
    {
    }

    public DbSet<Domain.Entities.Product> Products
        => Set<Domain.Entities.Product>();

    protected override void OnModelCreating(
        ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Domain.Entities.Product>()
            .ToTable("Productos");

        modelBuilder.Entity<Domain.Entities.Product>()
            .HasKey(x => x.Id_producto);

        modelBuilder.Entity<Domain.Entities.Product>()
       .Property(x => x.Costo)
       .HasPrecision(10, 2);

        modelBuilder.Entity<Domain.Entities.Product>()
        .Property(x => x.PrecioVenta)
        .HasPrecision(10, 2);

        base.OnModelCreating(modelBuilder);
    }
}
