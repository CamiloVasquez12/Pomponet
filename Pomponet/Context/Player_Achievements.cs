/*using Microsoft.EntityFrameworkCore;
using Pomponet.Model;

namespace Pomponet.Context
{
    public class Player_AchievementsDbContext : DbContext
    {
        public Player_AchievementsDbContext(DbContextOptions<Player_AchievementsDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Player_Achievements>()
              .HasKey(Player_Achievements => Player_Achievements.Id_Player_Achievement);
        }
        public DbSet<Player_Achievements> Player_Achievement { get; set; }
    }
}*/
