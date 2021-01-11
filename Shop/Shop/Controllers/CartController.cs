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

        public CartController(CartService cartService)
        {
            this.cartService = cartService;
        }
        public IActionResult Index(ProductViewModel productViewModel)
        {
            Guid userId = Guid.NewGuid();
            var cart = cartService.AddProductToCart(productViewModel, userId);
            return View(cart);
        }
    }
}
