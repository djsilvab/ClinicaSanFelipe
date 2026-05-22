using Microsoft.EntityFrameworkCore;
using Sales.Api.Domain.Entities;

namespace Sales.Api.Infrastructure.Persistence
{
    public class SalesDbContext :DbContext
    {
        public SalesDbContext( DbContextOptions<SalesDbContext> options)
            : base(options)
        {
            
        }

        public DbSet<SaleCab> SaleCab => Set<SaleCab>();

        public DbSet<SaleDet> SaleDet => Set<SaleDet>();

        public DbSet<MovementCab> MovementCab => Set<MovementCab>();

        public DbSet<MovementDet> MovementDet => Set<MovementDet>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SaleCab>().ToTable("VentaCab");

            modelBuilder.Entity<SaleCab>().HasKey(x => x.Id_VentaCab);

            modelBuilder.Entity<SaleCab>().Property(x => x.SubTotal).HasPrecision(18, 2);

            modelBuilder.Entity<SaleCab>().Property(x => x.Igv).HasPrecision(18, 2);

            modelBuilder.Entity<SaleCab>().Property(x => x.Total).HasPrecision(18, 2);

            modelBuilder.Entity<SaleDet>().ToTable("VentaDet");

            modelBuilder.Entity<SaleDet>().HasKey(x => x.Id_VentaDet);

            modelBuilder.Entity<SaleDet>().Property(x => x.Precio).HasPrecision(18, 2);

            modelBuilder.Entity<SaleDet>().Property(x => x.Sub_Total).HasPrecision(18, 2);

            modelBuilder.Entity<SaleDet>().Property(x => x.Igv).HasPrecision(18, 2);

            modelBuilder.Entity<SaleDet>().Property(x => x.Total).HasPrecision(18, 2);

            modelBuilder.Entity<SaleDet>().HasOne(x => x.SaleCab).WithMany(x => x.Details).HasForeignKey(x => x.Id_VentaCab);

            modelBuilder.Entity<MovementCab>().ToTable("MovimientoCab");

            modelBuilder.Entity<MovementCab>().HasKey(x => x.Id_MovimientoCab);

            modelBuilder.Entity<MovementDet>().ToTable("MovimientoDet");

            modelBuilder.Entity<MovementDet>().HasKey(x => x.Id_MovimientoDet);

            modelBuilder.Entity<MovementDet>().HasOne(x => x.MovementCab).WithMany(x => x.Details).HasForeignKey(x => x.Id_movimientocab);

            base.OnModelCreating(modelBuilder);
        }
    }
}