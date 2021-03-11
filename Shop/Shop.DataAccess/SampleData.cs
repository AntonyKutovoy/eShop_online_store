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
                    Name = "Test" + i,
                    Price = i * 10000,
                    Description = "Test1" + i,
                    ImagePath = "/images/products/Test.png"
                });
            }
            return products;
        }
    }
}
