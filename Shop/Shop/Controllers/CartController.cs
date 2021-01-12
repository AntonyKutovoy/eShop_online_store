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

        public CartController(CartService cartService, ProductService productService)
        {
            this.cartService = cartService;
            this.productService = productService;
        }
        public IActionResult index(int id)
        {
            var product = productService.GetProduct(id);
            Guid userId = Guid.NewGuid();
            var cart = cartService.AddProductToCart(product, userId);
            return View(cart);
        }


    }
}
