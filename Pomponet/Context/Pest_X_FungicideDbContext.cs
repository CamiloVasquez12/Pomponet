/*using Microsoft.EntityFrameworkCore;
using Pomponet.Model;

namespace Pomponet.Context
{
    public class Pest_X_FungicideDbContext : DbContext
    {
        public Pest_X_FungicideDbContext(DbContextOptions<Pest_X_FungicideDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Pest_X_Fungicide>()
              .HasKey(Pest_X_Fungicide => Pest_X_Fungicide.Id_Pest_X_Fungicide);
        }
        public DbSet<Pest_X_Fungicide> Pest_X_Fungicid { get; set; }
    }
}*/
