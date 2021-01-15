using Shop.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Shop.DataAccess
{
    public class ProductInMemoryRepository : IProductRepository
    {
        private List<Product> products;
        public List<Product> GetAll()
        {
            if (products == null)
                products = SampleData.GetDefaultProducts();

            return products;
        }

        public Product Get(Guid id)
        {
            var allProducts = GetAll();
            return allProducts.FirstOrDefault(p => p.Id == id);
        }
    }
}