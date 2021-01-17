using Microsoft.AspNetCore.Mvc;
using Shop.Services;
using System;

namespace Shop.Controllers
{
    public class ProductController : Controller
    {
        private readonly ProductService productService;
        private readonly CartService cartService;
        private Guid userId = new Guid("0f8fad5b-d9cb-469f-a165-70867728950e");//временная переменная для проверок

        public ProductController(ProductService productService, CartService cartService)
        {
            this.productService = productService;
            this.cartService = cartService;
        }

        public IActionResult Index(Guid id)
        {
            var product = productService.GetProduct(id);
            ViewData["productInCartCount"] = cartService.GetCurrentCart(userId).AllAmount;
            return View(product);
        }
    }
}
