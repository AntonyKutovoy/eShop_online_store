using Shop.DataAccess.Models;
using System;

namespace Shop.DataAccess
{
    public interface ICartRepository
    {
        Cart TryGetByUserId(string userId);
        Cart Create(string userId, Product product);//можно ли вместо продукта передавать его id?
        Cart AddProduct(Guid Guid, Product product);//можно ли вместо продукта передавать его id?
        void Update(Cart existingCart);//можно ли вместо корзины передавать ее id?
        void DeleteItem(Cart existingCart, Product product);//можно ли вместо продукта и корзины передавать их id?
        void DeleteCart(string userId);
    }
}