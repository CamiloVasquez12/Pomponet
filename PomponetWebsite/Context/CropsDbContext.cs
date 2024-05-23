using Microsoft.EntityFrameworkCore;
using PomponetWebsite.Models;

namespace PomponetWebsite.Context
{
    public class CropsDbContext : DbContext
    {
        public CropsDbContext(DbContextOptions<CropsDbContext> options) : base(options) 
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Crops>()
                .HasKey(Crops => Crops.Id_Crop);
            modelBuilder.Entity<Achievements>()
              .HasKey(Achievements => Achievements.Id_Achievement);
            modelBuilder.Entity<AplicationTools>()
              .HasKey(AplicationTools => AplicationTools.Id_AplicationTool);
            modelBuilder.Entity<Epps>()
             .HasKey(Epps => Epps.Id_Epp);
            modelBuilder.Entity<Fungicide_X_Pompon_Part>()
              .HasKey(Fungicide_X_Pompon_Part => Fungicide_X_Pompon_Part.Id_Fungicide_X_Pompon_Part);
            modelBuilder.Entity<Fungicides>()
               .HasKey(Fungicides => Fungicides.Id_Fungicide);
            modelBuilder.Entity<Inventories>()
              .HasKey(Inventories => Inventories.Id_Inventory);
            modelBuilder.Entity<Money>()
              .HasKey(Money => Money.Id_Money);
            modelBuilder.Entity<Pest_X_Fungicide>()
              .HasKey(Pest_X_Fungicide => Pest_X_Fungicide.Id_Pest_X_Fungicide);
            modelBuilder.Entity<Player_Achievements>()
               .HasKey(Player_Achievements => Player_Achievements.Id_Player_Achievement);
            modelBuilder.Entity<People>()
             .HasKey(People => People.Id_Person);
            modelBuilder.Entity<Players>()
               .HasKey(Players => Players.Id_Player);
            modelBuilder.Entity<Pests>()
              .HasKey(Pests => Pests.Id_Pest);
            modelBuilder.Entity<Pompon_Parts>()
               .HasKey(Pompon_Parts => Pompon_Parts.Id_Pompon_Part);
            modelBuilder.Entity<Sensors>()
                .HasKey(Sensors => Sensors.Id_Sensor);
            modelBuilder.Entity<Types_Fungicides>()
               .HasKey(Types_Fungicides => Types_Fungicides.Id_Type_Fungicide);
        }
        public DbSet<Crops> Crop { get; set; }
        public DbSet<Achievements> Achievements { get; set; }
        public DbSet<AplicationTools> AplicationTools { get; set; }
        public DbSet<Epps> Epps { get; set; }
        public DbSet<Fungicide_X_Pompon_Part> Fungicide_X_Pompon_Part { get; set; }
        public DbSet<Fungicides> Fungicides { get; set; }
        public DbSet<Inventories> Inventories { get; set; }
        public DbSet<Money> Money { get; set; }
        public DbSet<Pest_X_Fungicide> Pest_X_Fungicide { get; set; }
        public DbSet<Pests> Pests { get; set; }
        public DbSet<Pompon_Parts> Pompon_Parts { get; set; }
        public DbSet<Sensors> Sensors { get; set; }
        public DbSet<Types_Fungicides> Types_Fungicides { get; set; }
        public DbSet<Player_Achievements> Player_Achievements { get; set; }
        public DbSet<Players> Players { get; set; }
        public DbSet<People> People { get; set; }


    }
}
