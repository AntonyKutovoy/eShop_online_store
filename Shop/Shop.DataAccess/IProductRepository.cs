using Shop.DataAccess.Models;
using System;
using System.Collections.Generic;

namespace Shop.DataAccess
{
    public interface IProductRepository
    {
        List<Product> GetAll();
        Product Get(Guid id);
        void Create(Product product);
    }
}