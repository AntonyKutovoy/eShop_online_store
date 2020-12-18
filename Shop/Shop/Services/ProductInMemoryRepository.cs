using Shop.Models;
using System.Collections.Generic;

namespace Shop.Services
{
    public class ProductInMemoryRepository : IProductRepository
    {
        public List<Product> GetAll()
        {
            return SampleData.GetDefaultProducts();
        }
    }
}