/*using Microsoft.EntityFrameworkCore;
using Pomponet.Model;

namespace Pomponet.Context
{
    public class MoneyDbContext : DbContext
    {
        public MoneyDbContext(DbContextOptions<MoneyDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Money>()
              .HasKey(Money => Money.Id_Money);
        }
        public DbSet<Money> Money { get; set; }
    }
}*/
