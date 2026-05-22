using Microsoft.EntityFrameworkCore;
using Purchase.Api.Domain.Entities;

namespace Purchase.Api.Infrastructure.Persistence
{
    public class PurchaseDbContext
    : DbContext
    {
        public PurchaseDbContext(
            DbContextOptions<
                PurchaseDbContext>
                options)
            : base(options)
        {
        }

        public DbSet<PurchaseCab>
            PurchaseCab
            => Set<PurchaseCab>();

        public DbSet<PurchaseDet>
            PurchaseDet
            => Set<PurchaseDet>();

        public DbSet<MovementCab>
            MovementCab
            => Set<MovementCab>();

        public DbSet<MovementDet>
            MovementDet
            => Set<MovementDet>();

        protected override void OnModelCreating(
     ModelBuilder modelBuilder)
        {
            // CompraCab
            modelBuilder.Entity<PurchaseCab>()
                .ToTable("CompraCab");

            modelBuilder.Entity<PurchaseCab>()
                .HasKey(x => x.Id_CompraCab);

            modelBuilder.Entity<PurchaseCab>()
                .Property(x => x.SubTotal)
                .HasPrecision(10, 2);

            modelBuilder.Entity<PurchaseCab>()
                .Property(x => x.Igv)
                .HasPrecision(10, 2);

            modelBuilder.Entity<PurchaseCab>()
                .Property(x => x.Total)
                .HasPrecision(10, 2);

            // CompraDet
            modelBuilder.Entity<PurchaseDet>()
                .ToTable("CompraDet");

            modelBuilder.Entity<PurchaseDet>()
                .HasKey(x => x.Id_CompraDet);

            modelBuilder.Entity<PurchaseDet>()
                .Property(x => x.Precio)
                .HasPrecision(18, 2);

            modelBuilder.Entity<PurchaseDet>()
                .Property(x => x.Sub_Total)
                .HasPrecision(18, 2);

            modelBuilder.Entity<PurchaseDet>()
                .Property(x => x.Igv)
                .HasPrecision(18, 2);

            modelBuilder.Entity<PurchaseDet>()
                .Property(x => x.Total)
                .HasPrecision(18, 2);

            modelBuilder.Entity<PurchaseDet>()
                .HasOne(x => x.PurchaseCab)
                .WithMany(x => x.Details)
                .HasForeignKey(x => x.Id_CompraCab);

            // MovementCab
            modelBuilder.Entity<MovementCab>()
                .ToTable("MovimientoCab");

            modelBuilder.Entity<MovementCab>()
                .HasKey(x => x.Id_MovimientoCab);

            // MovementDet
            modelBuilder.Entity<MovementDet>()
                .ToTable("MovimientoDet");

            modelBuilder.Entity<MovementDet>()
                .HasKey(x => x.Id_MovimientoDet);

            modelBuilder.Entity<MovementDet>()
                .HasOne(x => x.MovementCab)
                .WithMany(x => x.Details)
                .HasForeignKey(x => x.Id_movimientocab);

            base.OnModelCreating(modelBuilder);
        }
    }
}