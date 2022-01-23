using BankCards.DAL.Configuration;
using BankCards.Domain;
using BankCards.Domain.Account;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BankCards.DAL.Context;

public class BankContext : IdentityDbContext<AppUser>
{
    public DbSet<Card> Cards { get; set; }

    public BankContext(DbContextOptions<BankContext> options)
        : base(options)
    {
        //Database.EnsureCreated();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new CardsConfiguration());
        //modelBuilder.ApplyConfiguration(new UsersConfiguration());
        base.OnModelCreating(modelBuilder);
    }
}
