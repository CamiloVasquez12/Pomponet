/*using Microsoft.EntityFrameworkCore;
using Pomponet.Model;

namespace Pomponet.Context
{
    public class Types_FungicidesDbContext : DbContext
    {
        public Types_FungicidesDbContext(DbContextOptions<Types_FungicidesDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Types_Fungicides>()
              .HasKey(Types_Fungicides => Types_Fungicides.Id_Type_Fungicide);
        }
        public DbSet<Types_Fungicides> Type_Fungicide { get; set; }
    }
}*/
