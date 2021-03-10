
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop.Models;
using Shop.Services;
using System;

namespace Shop.Controllers
{
    public class ProductController : Controller
    {
        private readonly ProductService productService;

        public ProductController(ProductService productService)
        {
            this.productService = productService;
        }

        public IActionResult Index(Guid id)
        {
            var product = productService.GetProduct(id);
            return View(product);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Create() => View();

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult Create(ProductViewModel productViewModel)
        {
            productService.Create(productViewModel);
            return View();
        }
    }
}
