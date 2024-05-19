/*using Microsoft.EntityFrameworkCore;
using Pomponet.Model;

namespace Pomponet.Context
{
    public class AchievementsDbContext : DbContext
    {
        public AchievementsDbContext(DbContextOptions<AchievementsDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Achievements>()
              .HasKey(Achievements => Achievements.Id_Achievement);
        }
        public DbSet<Achievements> Achievement { get; set; }
    }
}
*/