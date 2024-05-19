/*using Microsoft.EntityFrameworkCore;
using Pomponet.Model;

namespace Pomponet.Context
{
    public class Fungicide_X_Pompon_PartDbContext : DbContext
    {
        public Fungicide_X_Pompon_PartDbContext(DbContextOptions<Fungicide_X_Pompon_PartDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Fungicide_X_Pompon_Part>()
              .HasKey(Fungicide_X_Pompon_Part => Fungicide_X_Pompon_Part.Id_Fungicide_X_Pompon_Part);
        }
        public DbSet<Fungicide_X_Pompon_Part> Fungicide_X_Pompon_Part { get; set; }
    }
}*/
