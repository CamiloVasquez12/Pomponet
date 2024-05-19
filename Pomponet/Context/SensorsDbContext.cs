/*using Microsoft.EntityFrameworkCore;
using Pomponet.Model;

namespace Pomponet.Context
{
    public class SensorsDbContext : DbContext
    {
        public SensorsDbContext(DbContextOptions<SensorsDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Sensors>()
              .HasKey(Sensors => Sensors.Id_Sensor);
        }
        public DbSet<Sensors> Sensor { get; set; }
    }
}*/