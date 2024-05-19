/*using Microsoft.EntityFrameworkCore;
using Pomponet.Model;

namespace Pomponet.Context
{
    public class InventoriesDbContext : DbContext
    {
        public InventoriesDbContext(DbContextOptions<InventoriesDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Inventories>()
              .HasKey(Inventories => Inventories.Id_Inventory);
        }
        public DbSet<Inventories> Inventory { get; set; }
    }
}*/
