using Microsoft.EntityFrameworkCore;

namespace Catalog.Entities
{
    public class ItemContext : DbContext
    {
        public ItemContext(DbContextOptions<ItemContext> options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Item>().Property(p => p.Price).HasColumnType("decimal(18,4)");
            //base.OnModelCreating(modelBuilder);
        }

        public DbSet<Item> Items { get; set; }
    }
}