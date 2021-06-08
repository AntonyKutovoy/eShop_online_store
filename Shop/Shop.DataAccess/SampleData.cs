using Shop.DataAccess.Models;
using System.Collections.Generic;

namespace Shop.DataAccess
{
    public class SampleData
    {
        public static List<Product> GetDefaultProducts()
        {
            var products = new List<Product>();
            for (int i = 1; i < 30; i++)
            {
                products.Add(new Product
                {
                    Name = "Product" + " " + i,
                    Price = i * 10000,
                    Description = "Lorem ipsum dolor sit amet, consectetur adipisicing elit. Amet numquam aspernatur!",
                    ImagePath = "/images/products/700x400.gif"
                });
            }
            return products;
        }
    }
}
