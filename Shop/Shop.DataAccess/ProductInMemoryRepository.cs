using Shop.DataAccess.Models;
using System.Collections.Generic;
using System.Linq;

namespace Shop.DataAccess
{
    public class ProductInMemoryRepository : IProductRepository
    {
        public Product Get(int id)
        {
            var allProducts = GetAll();
            return allProducts.FirstOrDefault(p => p.Id == id);
        }

        public List<Product> GetAll()
        {
            return SampleData.GetDefaultProducts();
        }
    }
}