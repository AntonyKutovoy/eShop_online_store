using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Shop.DataAccess;
using Shop.Models;
using Shop.Services;
using System;

namespace Shop.Controllers
{
    public class AccountController : Controller
    {
        private readonly CartService cartService;
        private readonly ProductService productService;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private Guid userId = new Guid("0f8fad5b-d9cb-469f-a165-70867728950e");//временная переменная для проверок

        public AccountController(CartService cartService, ProductService productService)
        {
            this.cartService = cartService;
            this.productService = productService;
        }

        public IActionResult Login()
        {
            ViewData["cartProductsCount"] = cartService.GetCurrentCart(userId).AllAmount;
            return View(cartService.GetCurrentCart(userId));
        }

        public IActionResult Register(RegisterViewModel data, string returnUrl = null)
        {
            ViewData["cartProductsCount"] = cartService.GetCurrentCart(userId).AllAmount;
            returnUrl = returnUrl ?? Url.Content("~/");
            if (ModelState.IsValid)
            {
                var result = _signInManager.PasswordSignInAsync(data.Email, data.Password, data.RememberMe, lockoutOnFailure: true).Result;
                if (result.Succeeded)
                {
                    return LocalRedirect(returnUrl);
                }
            }
            return RedirectToAction("Login");
        }
    }
}
