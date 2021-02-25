using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Shop.DataAccess;
using Shop.Models;
using Shop.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Controllers
{
    public class CheckOutController : Controller
    {
        private readonly CartService cartService;
        private readonly UserManager<ApplicationUser> _userManager;
        public CheckOutController(CartService cartService, UserManager<ApplicationUser> userManager)
        {
            this.cartService = cartService;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            ViewData["cartProductsCount"] = cartService.GetCurrentCart(_userManager.GetUserId(User)).AllAmount;
            return View(cartService.GetCurrentCart(_userManager.GetUserId(User)));
        }

        public IActionResult Finish()
        {
            cartService.SaveOrder(_userManager.GetUserId(User));
            return RedirectToAction("Index");
        }

    }
}
