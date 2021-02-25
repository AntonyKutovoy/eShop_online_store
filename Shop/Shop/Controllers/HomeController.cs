using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Shop.DataAccess;
using Shop.Models;
using Shop.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Shop.Controllers
{
    public class HomeController : Controller
    {
        private const int productsCountPerPage = 9;
        private readonly ProductService productService;
        private readonly CartService cartService;
        private readonly UserManager<ApplicationUser> _userManager;

        public HomeController(ProductService productService, CartService cartService, UserManager<ApplicationUser> userManager)
        {
            this.productService = productService;
            this.cartService = cartService;
            _userManager = userManager;
        }
        public IActionResult Index(int page = 1)
        {
            var products = productService.GetAllProducts();
            var productsOnCurrentPage = CreatePagination(products, page);
            CreateCartProductsCount();
            ViewData["methodForPagination"] = "Index";
            return View(productsOnCurrentPage);
        }

        private void CreateCartProductsCount()
        {
            ViewData["cartProductsCount"] = cartService.GetCurrentCart(_userManager.GetUserId(User)).AllAmount;
        }

        private List<ProductViewModel> CreatePagination(List<ProductViewModel> products, int page)
        {
            var productsOnCurrentPage = products.Skip((page - 1) * productsCountPerPage).Take(productsCountPerPage).ToList();
            var countPages = products.Count / productsCountPerPage;
            if (products.Count % productsCountPerPage > 0)
            {
                countPages++;
            }
            ViewData["countPages"] = countPages;
            return productsOnCurrentPage;
        }

        public IActionResult Search(string id, int page = 1)
        {
            var products = productService.GetAllProducts();
            var searchProducts = new List<ProductViewModel>();
            if (!string.IsNullOrEmpty(id))
            {
                searchProducts = products.Where(x => x.Name.Contains(id)).ToList();
            }
            var productsOnCurrentPage = CreatePagination(searchProducts, page);
            CreateCartProductsCount();
            ViewData["methodForPagination"] = "Search/" + id;
            return View("Index", productsOnCurrentPage);
        }
    }
}
