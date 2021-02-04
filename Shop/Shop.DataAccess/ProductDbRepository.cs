using Shop.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Shop.DataAccess
{
    public class ProductDbRepository : IProductRepository
    {
        private readonly ShopContext shopContext;
        public ProductDbRepository(ShopContext shopContext)
        {
            this.shopContext = shopContext;
        }
        public Product Get(Guid id)
        {
            var allProducts = GetAll();
            return allProducts.FirstOrDefault(p => p.Id == id);
        }

        public List<Product> GetAll()
        {
            IQueryable<Product> productIQuer = shopContext.Products;
            var products = productIQuer.ToList();
            return products;
        }
    }
}