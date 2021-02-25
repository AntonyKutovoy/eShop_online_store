using Shop.DataAccess.Models;
using System;

namespace Shop.DataAccess
{
    public interface ICartRepository
    {
        Cart TryGetByUserId(string userId);
        Cart Create(string userId, Product product);
        Cart AddProduct(Guid Guid, Product product);
        Cart Update(Cart existingCart);
        void SaveForOrderPreparation(string userId);
    }
}