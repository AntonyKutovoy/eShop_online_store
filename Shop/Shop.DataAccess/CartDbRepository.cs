using Microsoft.EntityFrameworkCore;
using Shop.DataAccess.Models;
using System;
using System.Linq;

namespace Shop.DataAccess
{
    public class CartDbRerpository : ICartRepository
    {
        private readonly ShopContext shopContext;

        public CartDbRerpository(ShopContext shopContext)
        {
            this.shopContext = shopContext;
        }

        public Cart AddProduct(Guid id, Product product)
        {
            var cart = shopContext.Carts.FirstOrDefault(x => x.Id == id);
            var existingSameProduct = cart.CartItems.FirstOrDefault(x => x.ProductId == product.Id);
            if (existingSameProduct != null)
            {
                existingSameProduct.Amount += 1;
            }
            else
            {
                cart.CartItems.Add(new CartItem { Cart = cart, Product = product, Amount = 1, Id = Guid.NewGuid() });
            }
            shopContext.SaveChanges();
            return cart;
        }

        public Cart Create(string userId, Product product)
        {
            var cart = new Cart { UserId = userId };
            shopContext.Carts.Add(cart);
            cart.CartItems.Add(new CartItem { Cart = cart, Product = product, Amount = 1, Id = Guid.NewGuid() });//натсроить чтобы id генерировалась автоматически
            shopContext.SaveChanges();
            return cart;
        }

        public void DeleteCart(string userId)
        {
            shopContext.Carts.Remove(TryGetByUserId(userId));
            shopContext.SaveChanges();
        }

        public Cart TryGetByUserId(string userId)
        {
            var cart = shopContext.Carts.Include(c => c.CartItems).ThenInclude(p => p.Product).FirstOrDefault(i => i.UserId == userId);
            return cart;
        }

        public void Update(Cart existingCart)
        {
            var cart = shopContext.Carts.FirstOrDefault(x => x.Id == existingCart.Id);
            cart = existingCart;
            shopContext.SaveChanges();
        }

        public void DeleteItem(Cart existingCart, Product product)
        {
            var cart = shopContext.Carts.FirstOrDefault(x => x.Id == existingCart.Id);
            var cartItem = cart.CartItems.FirstOrDefault(x => x.ProductId == product.Id);
            product.CartItems.Remove(cartItem);
            shopContext.SaveChanges();
        }
    }
}