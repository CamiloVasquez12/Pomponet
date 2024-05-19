/*using Microsoft.EntityFrameworkCore;
using Pomponet.Model;

namespace Pomponet.Context
{
    public class EppsDbContext : DbContext
    {
        public EppsDbContext(DbContextOptions<EppsDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Epps>()
              .HasKey(Epps => Epps.Id_Epp);
        }
        public DbSet<Epps> Epp { get; set; }
    }
}
*/