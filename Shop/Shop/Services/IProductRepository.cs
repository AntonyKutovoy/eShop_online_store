using Shop.Models;
using System.Collections.Generic;

namespace Shop.Services
{
    public interface IProductRepository
    {
        List<Product> GetAll();
        Product Get(int id);
    }
}