using Shop.DataAccess.Models;
using System;

namespace Shop.DataAccess
{
    public interface ICartRepository
    {
        Cart TryGetByUserId(Guid userId);
        Cart Create(Guid userId, Product product);
        Cart AddProduct(int id, Product product);
        Cart Update(Cart existingCart);
    }
}