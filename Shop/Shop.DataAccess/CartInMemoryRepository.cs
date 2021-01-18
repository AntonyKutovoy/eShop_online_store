using Shop.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Shop.DataAccess
{
    public class CartInMemoryRepository : ICartRepository
    {
        private List<Cart> carts = new List<Cart>();
        public Cart AddProduct(Guid id, Product product)
        {
            var cart = carts.FirstOrDefault(x => x.Id == id);
            var existingSameProduct = cart.Items.FirstOrDefault(x => x.Product.Id == product.Id);
            if (existingSameProduct != null)
            {
                existingSameProduct.Amount += 1;
            }
            else
            {
                cart.Items.Add(new CartItem
                {
                    Id = Guid.NewGuid(),
                    Amount = 1,
                    Product = product,
                    CartId = id,
                });
            }
            return cart;
        }

        public Cart Create(Guid userId, Product product)
        {
            var cartId = Guid.NewGuid();
            var cartItem = new CartItem
            {
                Id = Guid.NewGuid(),
                Amount = 1,
                Product = product,
                CartId = cartId
            };
            var cart = new Cart
            {
                Id = cartId,
                UserId = userId,
                Items = new List<CartItem>() { cartItem }
            };
            carts.Add(cart);
            return cart;
        }

        public void Delete(Guid userId)
        {
            carts.Remove(TryGetByUserId(userId));
        }

        public Cart TryGetByUserId(Guid userId)
        {
            var cart = carts.FirstOrDefault(x => x.UserId == userId);
            return cart;
        }

        public Cart Update(Cart existingCart)
        {
            var cart = carts.FirstOrDefault(x => x.Id == existingCart.Id);
            cart = existingCart;
            return cart;
        }
    }
}