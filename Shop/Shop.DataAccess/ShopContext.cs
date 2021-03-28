using Microsoft.EntityFrameworkCore;
using Shop.DataAccess.Models;

namespace Shop.DataAccess
{
    public class ShopContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<Order> Orders { get; set; }
        public ShopContext(DbContextOptions<ShopContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<CartItem>()
            .HasKey(t => new { t.ProductId, t.CartId });

            builder.Entity<CartItem>()
                .HasOne(sc => sc.Product)
                .WithMany(s => s.CartItems)
                .HasForeignKey(sc => sc.ProductId);

            builder.Entity<CartItem>()
                .HasOne(sc => sc.Cart)
                .WithMany(c => c.CartItems)
                .HasForeignKey(sc => sc.CartId);

            builder.Entity<OrderItem>()
            .HasKey(t => new { t.ProductId, t.OrderId });

            builder.Entity<OrderItem>()
                .HasOne(oi => oi.Product)
                .WithMany(p => p.OrderItems)
                .HasForeignKey(oi => oi.ProductId);

            builder.Entity<OrderItem>()
                .HasOne(oi => oi.Order)
                .WithMany(o => o.OrderItems)
                .HasForeignKey(oi => oi.OrderId);
        }
    }
}
