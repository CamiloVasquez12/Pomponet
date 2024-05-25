using Microsoft.EntityFrameworkCore;
using PomponetWebsite.Models;
using System;

namespace PomponetWebsite.Context
{
    public class CropsDbContext : DbContext
    {
        public CropsDbContext(DbContextOptions<CropsDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Crops>().HasKey(c => c.Id_Crop);
            modelBuilder.Entity<Achievements>().HasKey(a => a.Id_Achievement);
            modelBuilder.Entity<AplicationTools>().HasKey(a => a.Id_AplicationTool);
            modelBuilder.Entity<Epps>().HasKey(e => e.Id_Epp);
            modelBuilder.Entity<Fungicide_X_Pompon_Part>().HasKey(fxpp => fxpp.Id_Fungicide_X_Pompon_Part);
            modelBuilder.Entity<Inventories>().HasKey(i => i.Id_Inventory);
            modelBuilder.Entity<Money>().HasKey(m => m.Id_Money);
            modelBuilder.Entity<Pest_X_Fungicide>().HasKey(pxf => pxf.Id_Pest_X_Fungicide);
            modelBuilder.Entity<Player_Achievements>().HasKey(pa => pa.Id_Player_Achievement);
            modelBuilder.Entity<People>().HasKey(p => p.Id_Person);
            modelBuilder.Entity<Players>().HasKey(pl => pl.Id_Player);
            modelBuilder.Entity<Pests>().HasKey(pe => pe.Id_Pest);
            modelBuilder.Entity<Pompon_Parts>().HasKey(pp => pp.Id_Pompon_Part);
            modelBuilder.Entity<Sensors>().HasKey(s => s.Id_Sensor);
            modelBuilder.Entity<Types_Fungicides>().HasKey(tf => tf.Id_Type_Fungicide);

            modelBuilder.Entity<Pompon_Parts>()
                .HasMany(p => p.Fungicide_X_Pompon_Parts)
                .WithOne(f => f.Pompon_Parts)
                .HasForeignKey(f => f.Id_Pompon_Part);

            modelBuilder.Entity<Fungicides>()
                .HasMany(f => f.Fungicide_X_Pompon_Parts)
                .WithOne(p => p.Fungicides)
                .HasForeignKey(f => f.Id_Fungicide);
            modelBuilder.Entity<Crops>()
               .HasMany(c => c.Fungicides)
               .WithOne(f => f.Crops)
               .HasForeignKey(f => f.Id_crop);
            modelBuilder.Entity<People>()
              .HasMany(p => p.Inventories)
              .WithOne(f => f.People)
              .HasForeignKey(f => f.Id_Person);
            modelBuilder.Entity<AplicationTools>()
              .HasMany(a => a.Inventories)
              .WithOne(f => f.Aplication_Tools)
              .HasForeignKey(f => f.Id_Tool);
            modelBuilder.Entity<Epps>()
              .HasMany(e => e.Inventories)
              .WithOne(f => f.Epps)
              .HasForeignKey(f => f.Id_Epp);
            modelBuilder.Entity<Players>()
              .HasMany(p => p.Money)
              .WithOne(f => f.Players)
              .HasForeignKey(f => f.Id_Player);
            modelBuilder.Entity<Pests>()
              .HasMany(p => p.Pest_X_Fungicide)
              .WithOne(f => f.Pests)
              .HasForeignKey(f => f.Id_Pest);
            modelBuilder.Entity<Fungicides>()
                .HasMany(f => f.Pest_X_Fungicide)
                .WithOne(p => p.Fungicides)
                .HasForeignKey(f => f.Id_Fungicide);
            modelBuilder.Entity<Achievements>()
              .HasMany(a => a.Player_Achievements)
              .WithOne(f => f.Achievements)
              .HasForeignKey(f => f.Id_Achievement);
            modelBuilder.Entity<Players>()
              .HasMany(p => p.Player_Achievements)
              .WithOne(f => f.Players)
              .HasForeignKey(f => f.Id_Player);
            modelBuilder.Entity<People>()
              .HasMany(p => p.Players)
              .WithOne(f => f.People)
              .HasForeignKey(f => f.Id_Person);
            modelBuilder.Entity<Crops>()
               .HasMany(c => c.Sensors)
               .WithOne(f => f.Crops)
               .HasForeignKey(f => f.Id_crop);
            modelBuilder.Entity<Fungicides>()
                .HasMany(f => f.Types_Fungicides)
                .WithOne(p => p.Fungicides)
                .HasForeignKey(f => f.Id_Funicides);
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

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.LogTo(Console.WriteLine, new[] { DbLoggerCategory.Database.Command.Name }, LogLevel.Information)
                          .EnableSensitiveDataLogging();
        }
    }
}
