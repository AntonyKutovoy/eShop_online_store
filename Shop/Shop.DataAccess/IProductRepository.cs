using Shop.DataAccess.Models;
using System.Collections.Generic;

namespace Shop.DataAccess
{
    public interface IProductRepository
    {
        List<Product> GetAll();
        Product Get(int id);
    }
}