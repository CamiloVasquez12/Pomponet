/*using Microsoft.EntityFrameworkCore;
using Pomponet.Model;

namespace Pomponet.Context
{
    public class Pompon_PartsDbContext : DbContext
    {
        public Pompon_PartsDbContext(DbContextOptions<Pompon_PartsDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Pompon_Parts>()
              .HasKey(Pompon_Parts => Pompon_Parts.Id_Pompon_Part);
        }
        public DbSet<Pompon_Parts> Pompon_Part { get; set; }
    }
}
*/