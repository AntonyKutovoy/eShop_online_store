using Shop.DataAccess.Models;
using System.Collections.Generic;

namespace Shop.DataAccess
{
    public class SampleData
    {
        public static List<Product> GetDefaultProducts()
        {
            var products = new List<Product>();
            for (int i = 1; i < 100; i++)
            {
                products.Add(new Product
                {
                    Name = "Product" + " " + i,
                    Price = i * 10000,
                    Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.",
                    ImagePath = "/images/products/400x300.png"
                });
            }
            return products;
        }
    }
}