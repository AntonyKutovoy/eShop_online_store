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
        //private readonly CartService cartService;
        //private readonly ProductService productService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        private Guid userId = new Guid("0f8fad5b-d9cb-469f-a165-70867728950e");//временная переменная для проверок

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            //this.cartService = cartService;
            //this.productService = productService;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Login(string returnUrl = null)
        {
            return View(new LoginViewModel { ReturnUrl = returnUrl });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false).Result;
                if (result.Succeeded)
                {
                    // проверяем, принадлежит ли URL приложению
                    if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
                    {
                        return Redirect(model.ReturnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Неправильный логин и (или) пароль");
                }
            }
            return View(model);
        }

        //public IActionResult Register(RegisterViewModel data, string returnUrl = null)
        //{
        //    //ViewData["cartProductsCount"] = cartService.GetCurrentCart(userId).AllAmount;
        //    returnUrl = returnUrl ?? Url.Content("~/");
        //    if (ModelState.IsValid)
        //    {
        //        var result = _signInManager.PasswordSignInAsync(data.Email, data.Password, data.RememberMe, lockoutOnFailure: true).Result;
        //        if (result.Succeeded)
        //        {
        //            return LocalRedirect(returnUrl);
        //        }
        //    }
        //    return RedirectToAction("Login");
        //}

        [HttpGet]
        public IActionResult Register()
        {
            //ViewData["cartProductsCount"] = cartService.GetCurrentCart(userId).AllAmount;
            //return View(cartService.GetCurrentCart(userId));
            return View();
        }
        

        [HttpPost]
        public IActionResult Register(RegisterViewModel model)
        {
            //ViewData["cartProductsCount"] = cartService.GetCurrentCart(userId).AllAmount;
            if (ModelState.IsValid)
            {
                ApplicationUser aplicationUser = new ApplicationUser { Email = model.Email, UserName = model.Email };
                var result = _userManager.CreateAsync(aplicationUser, model.Password).Result;
                if (result.Succeeded)
                {
                    _signInManager.SignInAsync(aplicationUser, false);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            return RedirectToAction("Register");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Logout()
        {
            // удаляем аутентификационные куки
            _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
