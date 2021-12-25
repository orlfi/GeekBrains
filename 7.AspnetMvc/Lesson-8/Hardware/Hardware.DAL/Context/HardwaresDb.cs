using Hardwares.Domain;
using Microsoft.EntityFrameworkCore;

namespace Hardwares.DAL.Context
{
    public class HardwaresDb: DbContext
    {
        public DbSet<Hardware> Hardwares { get; set; }

        public HardwaresDb(DbContextOptions<HardwaresDb> options): base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Hardware>()
                .Property(p => p.Cost)
                .HasColumnType("decimal(18,2)");
        }
    }
}
