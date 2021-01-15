using Microsoft.AspNetCore.Mvc;
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

        public IActionResult Index(Guid id)
        {
            var product = productService.GetProduct(id);
            var cart = cartService.AddProductToCart(product, userId);
            return View(cart);
        }



    }
}
