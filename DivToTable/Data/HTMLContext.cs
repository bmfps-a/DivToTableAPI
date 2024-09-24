using DivToTable.Data.Map;
using DivToTable.Model;
using Microsoft.EntityFrameworkCore;

namespace DivToTable.Data
{
    public class HTMLContext : DbContext
    {
        public HTMLContext(DbContextOptions<HTMLContext> options)
            : base(options)
        {
            
        }
        public DbSet<HTMLModel> DivtoTable { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new HTMLMap());

            base.OnModelCreating(modelBuilder);
        }

    }
}
