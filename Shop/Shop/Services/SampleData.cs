using Shop.Models;
using System.Collections.Generic;

namespace Shop.Services
{
    public class SampleData
    {
        public static List<Product> GetDefaultProducts()
        {
            var products = new List<Product> {
                    new Product
                    {
                        Id = 1,
                        Name = "NVIDIA GeForce RTX 3070",
                        Price = 70000,
                        Description = "Здесь будет описание RTX 3070."
                    },
                    new Product
                    {
                        Id = 2,
                        Name = "NVIDIA GeForce RTX 3080",
                        Price = 100000,
                        Description = "Здесь будет описание RTX 3080."
                    },
                    new Product
                    {
                        Id = 3,
                        Name = "NVIDIA GeForce RTX 3090",
                        Price = 155000,
                        Description = "Здесь будет описание RTX 3090"
                    },
                    new Product
                    {
                        Id = 4,
                        Name = "NVIDIA GeForce RTX 3060TI",
                        Price = 55000,
                        Description = "Здесь будет описание RTX 3060TI."
                    },
                    new Product
                    {
                        Id = 5,
                        Name = "NVIDIA GeForce RTX 3050",
                        Price = 45000,
                        Description = "Здесь будет описание RTX 3050."
                    }
            };

            for (int i = 6; i < 30; i++)
            {
                products.Add(new Product
                {
                    Id = i,
                    Name = "NVIDIA GeForce " + i,
                    Price = i * 10000,
                    Description = "Здесь будет описание RTX." + i
                });
            }
            return products;
        }
    }
}
