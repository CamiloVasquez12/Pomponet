/*using Microsoft.EntityFrameworkCore;
using Pomponet.Model;

namespace Pomponet.Context
{
    public class FungicidesDbContext : DbContext
    {
        public FungicidesDbContext(DbContextOptions<FungicidesDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Fungicides>()
              .HasKey(Fungicides => Fungicides.Id_Fungicide);
        }
        public DbSet<Fungicides> Fungicide { get; set; }
    }
}*/
