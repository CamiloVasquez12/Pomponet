/*using Microsoft.EntityFrameworkCore;
using Pomponet.Model;

namespace Pomponet.Context
{
    public class AplicationToolsDbContext : DbContext
    {
        public AplicationToolsDbContext(DbContextOptions<AplicationToolsDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AplicationTools>()
              .HasKey(AplicationTools => AplicationTools.Id_AplicationTool);
        }
        public DbSet<AplicationTools> AplicationTool { get; set; }
    }
}
*/