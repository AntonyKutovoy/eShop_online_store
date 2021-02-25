using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Shop.DataAccess;
using Shop.Services;
using System;

namespace Shop.Controllers
{
    public class ProductController : Controller
    {
        private readonly ProductService productService;
        private readonly CartService cartService;
        private readonly UserManager<ApplicationUser> _userManager;

        public ProductController(ProductService productService, CartService cartService, UserManager<ApplicationUser> userManager)
        {
            this.productService = productService;
            this.cartService = cartService;
            _userManager = userManager;
        }

        public IActionResult Index(Guid id)
        {
            var product = productService.GetProduct(id);
            ViewData["cartProductsCount"] = cartService.GetCurrentCart(_userManager.GetUserId(User)).AllAmount;
            return View(product);
        }
    }
}
