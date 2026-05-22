using Auth.Api.Domain.Entities;
using Microsoft.EntityFrameworkCore;


namespace Auth.Api.Infrastructure.Persistence;

public class AuthDbContext : DbContext
{
    public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options)
    {
        
    }

    public DbSet<User> Users => Set<User>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .ToTable("Users");

        modelBuilder.Entity<User>()
            .HasKey(u => u.Id);

        modelBuilder.Entity<User>().HasData(
        new User
        {
            Id = 1,
            UserName = "admin",            
            PasswordHash = "$2a$11$mvh9A30LE9G8ARzPky/ic.DeLIxNoSKwAodh.40cnC2iWmvFHIzrW",
            Role = "Admin",
            CreatedAt = new DateTime(2026, 5, 22, 0, 0, 0, DateTimeKind.Utc)
        }
    );

        base.OnModelCreating(modelBuilder);
    }

}
