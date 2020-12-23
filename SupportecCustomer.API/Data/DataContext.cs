using Microsoft.EntityFrameworkCore;
using SupportecCustomer.API.Models;

namespace SupportecCustomer.API.Data
{
    public class DataContext : DbContext
    {

        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<Value> Values { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Photo> Photos { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<ProductItem> ProductItem { get; set; }
        public DbSet<Company> Company { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductItem>()
                .HasKey(x => new { x.ProductId, x.ItemId });

            modelBuilder.Entity<ProductItem>()
                .HasOne(pi => pi.Product)
                .WithMany(p => p.ProductItems)
                .HasForeignKey(pi => pi.ProductId);

            modelBuilder.Entity<ProductItem>()
                .HasOne(pi => pi.Item)
                .WithMany(a => a.ProductItems)
                .HasForeignKey(pi => pi.ItemId);
        }
    }
}