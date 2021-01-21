using Microsoft.AspNetCore.Mvc;
using Shop.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Controllers
{
    public class LoginController : Controller
    {
        private readonly CartService cartService;
        private readonly ProductService productService;
        private Guid userId = new Guid("0f8fad5b-d9cb-469f-a165-70867728950e");//временная переменная для проверок

        public LoginController(CartService cartService, ProductService productService)
        {
            this.cartService = cartService;
            this.productService = productService;
        }

        public IActionResult Index()
        {
            ViewData["cartProductsCount"] = cartService.GetCurrentCart(userId).AllAmount;
            return View(cartService.GetCurrentCart(userId));
        }
    }
}
