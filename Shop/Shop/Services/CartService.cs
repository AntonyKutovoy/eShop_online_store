using Shop.DataAccess;
using Shop.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Shop.Models
{
    public class CartService
    {
        private readonly ICartRepository cartRepository;

        public CartService(ICartRepository cartRepository)
        {
            this.cartRepository = cartRepository;
        }

        public CartViewModel AddProductToCart(ProductViewModel productViewModel, Guid userId)
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
                Items = cart.Items.ToCartItemsViewModel()
            };
            return cartViewModel;
        }

        public CartViewModel GetCurrentCart(Guid userId)
        {
            var existingCart = cartRepository.TryGetByUserId(userId);
            if (existingCart != null)
            {
                return new CartViewModel()
                {
                    Id = existingCart.Id,
                    Items = existingCart.Items.ToCartItemsViewModel()
                };
            }

            return new CartViewModel()
            {
                Items = new List<CartItemViewModel>()
            };
        }

        public CartViewModel Delete(Guid userId, int cartItemId)
        {
            var existingCart = cartRepository.TryGetByUserId(userId);
            if (existingCart != null)
            {
                existingCart.Items.RemoveAll(x => x.Id == cartItemId);
            }

            return GetCurrentCart(userId);
        }

        public CartViewModel UpdateAmount(Guid userId, int cartItemId, int amount)
        {
            if (amount == 0)
            {
                Delete(userId, cartItemId);
                return GetCurrentCart(userId);
            }

            var existingCart = cartRepository.TryGetByUserId(userId);
            if (existingCart != null)
            {
                var cartItem = existingCart.Items.FirstOrDefault(x => x.Id == cartItemId);
                cartItem.Amount = amount;
            }
            existingCart = cartRepository.Update(existingCart);
            return new CartViewModel()
            {
                Id = existingCart.Id,
                Items = existingCart.Items.ToCartItemsViewModel()
            };
        }
    }