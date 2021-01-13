using Microsoft.AspNetCore.Mvc;
using Shop.DataAccess;
using Shop.Models;
using Shop.Services;
using System;

namespace Shop.Controllers
{
    public class CartController : Controller
    {
        private readonly CartService cartService;
        private readonly ProductService productService;
        private Guid userId = new Guid("0f8fad5b-d9cb-469f-a165-70867728950e");//временная переменная для проверок

        public CartController(CartService cartService, ProductService productService)
        {
            this.cartService = cartService;
            this.productService = productService;
        }
        public IActionResult index(int id)
        {
            var product = productService.GetProduct(id);
            var cart = cartService.GetCurrentCart(userId);
            if (cart.Items.Count > 0)
            {
                for (int i = 0; i < cart.Items.Count; i++)
                {
                    if (cart.Items[i].Product.Id == product.Id)
                    {
                        cart = cartService.UpdateAmount(userId, cart.Items[i].Id, cart.Items[i].Amount + 1);
                        return View(cart);
                    }
                }
            }
            cart = cartService.AddProductToCart(product, userId);
            return View(cart);
        }


    }
}
