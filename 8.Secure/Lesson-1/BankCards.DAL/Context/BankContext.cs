using BankCards.Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BankCards.DAL.Context;

public class BankContext : IdentityDbContext<AppUser>
{
    public DbSet<Card> Cards { get; set; }

    public BankContext(DbContextOptions<BankContext> options)
        : base(options)
    {
        // Database.EnsureCreated();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Card>(entity =>
        {
            entity.Property(p => p.Id).ValueGeneratedOnAdd();

            entity.Property(p => p.Number).IsRequired().HasMaxLength(16);

            entity.Property(p => p.Type).IsRequired();

            entity.Property(p => p.Owner).IsRequired().HasMaxLength(100); ;

            //entity.Property(p => p.Created).IsRequired().HasColumnType("datetime");

            //entity.Property(p => p.ValidThru).IsRequired().HasColumnType("datetime");

            entity.Property(p => p.Created).IsRequired();

            entity.Property(p => p.ValidThru).IsRequired();
        });
        base.OnModelCreating(modelBuilder);
    }
}
