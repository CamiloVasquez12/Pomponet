/*using Microsoft.EntityFrameworkCore;
using Pomponet.Model;

namespace Pomponet.Context
{
    public class PestsDbContext : DbContext
    {
        public PestsDbContext(DbContextOptions<PestsDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Pests>()
              .HasKey(Pests => Pests.Id_Pest);
        }
        public DbSet<Pests> Pest { get; set; }
    }
}*/
