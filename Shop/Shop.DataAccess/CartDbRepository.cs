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

        public Cart AddProduct(Guid id, Product product)//метод работает
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

        public Cart Create(string userId, Product product)//метод работает
        {
            var cart = new Cart { UserId = userId };
            shopContext.Carts.Add(cart);
            cart.CartItems.Add(new CartItem { Cart = cart, Product = product, Amount = 1, Id = Guid.NewGuid() });
            shopContext.SaveChanges();
            return cart;
        }

        public void SaveForOrderPreparation(string userId)
        {
            shopContext.CartsForOrderPreparation.Add(TryGetByUserId(userId));
            shopContext.Carts.Remove(TryGetByUserId(userId));
            shopContext.SaveChanges();
        }

        public Cart TryGetByUserId(string userId)//метод работает
        {
            var cart = shopContext.Carts.Include(c => c.CartItems).ThenInclude(p => p.Product).FirstOrDefault(i => i.UserId == userId);
            return cart;
        }

        public Cart Update(Cart existingCart)//метод работает
        {
            var cart = shopContext.Carts.FirstOrDefault(x => x.Id == existingCart.Id);
            cart = existingCart;
            shopContext.SaveChanges();
            return cart;
        }

        public Cart Delete(Cart existingCart, Product product)//протестировать
        {
            var cart = shopContext.Carts.FirstOrDefault(x => x.Id == existingCart.Id);
            var cartItem = cart.CartItems.FirstOrDefault(x => x.ProductId == product.Id);
            product.CartItems.Remove(cartItem);
            shopContext.SaveChanges();
            return cart;
        }
        //// удаление продукта у корзины
        //Product product = shopContext.Products.Include(s => s.CartItems).FirstOrDefault(s => s.Name == "NVIDIA GeForce RTX 3070");
        //Cart Cart = shopContext.Carts.FirstOrDefault(c => c.UserId == userId);
        //if (product != null && cart != null)
        //{
        //    var cartItem = product.CartItems.FirstOrDefault(sc => sc.CartId == cart.Id);
        //    product.CartItems.Remove(cartItem);
        //    shopContext.SaveChanges();
        //}
    }
}