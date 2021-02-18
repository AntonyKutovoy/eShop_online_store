using Microsoft.EntityFrameworkCore;
using Shop.DataAccess.Models;

namespace Shop.DataAccess
{
    public class ShopContext : DbContext
    {
        public ShopContext(DbContextOptions<ShopContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
        public DbSet<Product> Products { get; set; }
    }
}
