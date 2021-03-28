using Shop.DataAccess;
using Shop.DataAccess.Models;
using Shop.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Shop.Services
{
    public class CartService
    {
        private readonly ICartRepository cartRepository;

        public CartService(ICartRepository cartRepository)
        {
            this.cartRepository = cartRepository;
        }

        public CartViewModel AddProductToCart(ProductViewModel productViewModel, string userId)
        {
            var existingCart = cartRepository.TryGetByUserId(userId);
            var product = productViewModel.ToProduct();

            Cart cart;
            if (existingCart == null)
            {
                cart = cartRepository.Create(userId, product);
            }
            else
            {
                cart = cartRepository.AddProduct(existingCart.Id, product);
            }

            var cartViewModel = new CartViewModel()
            {
                Id = cart.Id,
                Items = cart.CartItems.ToCartItemsViewModel()
            };
            return cartViewModel;
        }

        public CartViewModel GetCurrentCart(string userId)
        {
            var existingCart = cartRepository.TryGetByUserId(userId);
            if (existingCart != null)
            {
                return new CartViewModel()
                {
                    Id = existingCart.Id,
                    Items = existingCart.CartItems.ToCartItemsViewModel()
                };
            }

            return new CartViewModel()
            {
                Items = new List<CartItemViewModel>()
            };
        }

        public void DeleteItem(string userId, Guid cartItemId)
        {
            var existingCart = cartRepository.TryGetByUserId(userId);
            var cartItem = existingCart.CartItems.FirstOrDefault(x => x.Id == cartItemId);
            cartRepository.DeleteItem(existingCart, cartItem.Product);
        }

        public void UpdateAmount(string userId, Guid cartItemId, int amount)
        {
            var existingCart = cartRepository.TryGetByUserId(userId);
            if (existingCart != null)
            {
                var cartItem = existingCart.CartItems.FirstOrDefault(x => x.Id == cartItemId);
                cartItem.Amount = amount;
            }
            cartRepository.Update(existingCart);
        }

        public void DeleteCart(string userId)
        {
            var existingCart = cartRepository.TryGetByUserId(userId);
            if (existingCart != null)
            {
                cartRepository.DeleteCart(userId);
            }
        }
    }
}