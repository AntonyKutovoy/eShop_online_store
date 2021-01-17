using Microsoft.AspNetCore.Mvc;
using Shop.DataAccess;
using Shop.Services;
using System;
using System.Linq;

namespace Shop.Controllers
{
    public class HomeController : Controller
    {
        private const int productsCountPerPage = 9;
        private readonly ProductService productService;
        private readonly CartService cartService;
        private Guid userId = new Guid("0f8fad5b-d9cb-469f-a165-70867728950e");//временная переменная для проверок

        public HomeController(ProductService productService, CartService cartService)
        {
            this.productService = productService;
            this.cartService = cartService;
        }
        public IActionResult Index(int page = 1)
        {
            var products = productService.GetAllProducts();
            var productsOnCurrentPage = products.Skip((page - 1) * productsCountPerPage).Take(productsCountPerPage).ToList();

            var countPages = products.Count / productsCountPerPage;
            if (products.Count % productsCountPerPage > 0)
            {
                countPages++;
            }
            ViewData["countPages"] = countPages;
            ViewData["productInCartCount"] = cartService.GetCurrentCart(userId).AllAmount;
            return View(productsOnCurrentPage);
        }
    }
}
