using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Shop.DataAccess;
using Shop.Services;
using System;
using System.Collections.Generic;

namespace Shop.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private readonly CartService cartService;
        private readonly ProductService productService;
        private readonly UserManager<ApplicationUser> userManager;

        public CartController(CartService cartService, ProductService productService, UserManager<ApplicationUser> userManager)
        {
            this.cartService = cartService;
            this.productService = productService;
            this.userManager = userManager;
        }

        public IActionResult Index()
        {
            return View(cartService.GetCurrentCart(userManager.GetUserId(User)));
        }

        public IActionResult Add(Guid id)
        {
            var product = productService.GetProduct(id);
            cartService.AddProductToCart(product, userManager.GetUserId(User));
            return RedirectToAction("Index");
        }

        public IActionResult Update(Dictionary<Guid, int> items)
        {
            foreach (var item in items)
            {
                cartService.UpdateAmount(userManager.GetUserId(User), item.Key, item.Value);
            }
            
            return RedirectToAction("Index");
        }

        public IActionResult DeleteItem(Guid itemId)
        {
            cartService.DeleteItem(userManager.GetUserId(User), itemId);
            return RedirectToAction("Index");
        }

    }
}
