using System.Linq;

namespace Shop.DataAccess
{
    public static class ShopContextSeeder
    {
        public static void Seed(ShopContext context)
        {
            if (!context.Products.Any())
            {
                context.Products.AddRange(SampleData.GetDefaultProducts());
                context.SaveChanges();
            }
        }
    }
}