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
        private readonly UserManager<ApplicationUser> _userManager;

        public CartController(CartService cartService, ProductService productService, UserManager<ApplicationUser> userManager)
        {
            this.cartService = cartService;
            this.productService = productService;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            return View(cartService.GetCurrentCart(_userManager.GetUserId(User)));
        }

        public IActionResult Add(Guid id)
        {
            var product = productService.GetProduct(id);
            cartService.AddProductToCart(product, _userManager.GetUserId(User));
            return RedirectToAction("Index");
        }

        public IActionResult Update(Dictionary<Guid, int> items)
        {
            foreach (var item in items)
            {
                cartService.UpdateAmount(_userManager.GetUserId(User), item.Key, item.Value);
            }
            
            return RedirectToAction("Index");
        }

        public IActionResult Delete(Guid itemId)
        {
            cartService.Delete(_userManager.GetUserId(User), itemId);
            return RedirectToAction("Index");
        }

    }
}
