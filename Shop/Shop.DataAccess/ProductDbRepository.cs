using Microsoft.EntityFrameworkCore;
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
            return shopContext.Products.AsNoTracking().ToList();
        }

        public void Create (Product product)
        {
            shopContext.Products.Add(product);
            shopContext.SaveChanges();
        }
    }
}