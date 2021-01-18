using Microsoft.AspNetCore.Mvc;
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
        private Guid userId = new Guid("0f8fad5b-d9cb-469f-a165-70867728950e");//временная переменная для проверок
        public CheckOutController(CartService cartService)
        {
            this.cartService = cartService;
        }

        public IActionResult Index()
        {
            ViewData["productInCartCount"] = cartService.GetCurrentCart(userId).AllAmount;
            return View(cartService.GetCurrentCart(userId));
        }

        public IActionResult GetNewCart()
        {
            cartService.DeleteCart(userId);
            return RedirectToAction("Index");
        }

    }
}
