using System.Linq;
using Shop.Models;

namespace Shop.Models
{
    //класс для инициализации базы данных
    public class SampleData
    {
        public static void Initialize(MobileContext context)
        {
            if (!context.Products.Any())
            {
                context.Products.AddRange(
                    new Product
                    {
                        Name = "NVIDIA GeForce RTX 3070",
                        Price = 70000,
                        Description = "Здесь будет описание RTX 3070."
                    },
                    new Product
                    {
                        Name = "NVIDIA GeForce RTX 3080",
                        Price = 100000,
                        Description = "Здесь будет описание RTX 3080."
                    },
                    new Product
                    {
                        Name = "NVIDIA GeForce RTX 3090",
                        Price = 155000,
                        Description = "Здесь будет описание RTX 3090."
                    }, 
                    new Product
                    {
                        Name = "NVIDIA GeForce RTX 3060TI",
                        Price = 55000,
                        Description = "Здесь будет описание RTX 3060TI."
                    },
                    new Product
                    {
                        Name = "NVIDIA GeForce RTX 3050",
                        Price = 45000,
                        Description = "Здесь будет описание RTX 3050."
                    }
                );
                context.SaveChanges();
            }
        }
    }
}
