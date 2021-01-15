using Microsoft.AspNetCore.Mvc;
using Shop.Services;
using System;
using System.Collections.Generic;

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

        public IActionResult Index()
        {
            return View(cartService.GetCurrentCart(userId));
        }

        public IActionResult Add(Guid id)
        {
            var product = productService.GetProduct(id);
            cartService.AddProductToCart(product, userId);
            return RedirectToAction("Index");
        }

        public IActionResult Delete(Guid itemId)
        {
            cartService.Delete(userId, itemId);
            return RedirectToAction("Index");
        }
        public IActionResult Update(Dictionary<Guid, int> items)
        {
            foreach (var item in items)
            {
                cartService.UpdateAmount(userId, item.Key, item.Value);
            }
            
            return RedirectToAction("Index");
        }

    }
}
