using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Shop.DataAccess;
using Shop.Models;
using Shop.Services;
using System.Collections.Generic;
using System.Linq;

namespace Shop.Controllers
{
    public class HomeController : Controller
    {
        private const int productsCountPerPage = 9;
        private readonly ProductService productService;

        public HomeController(ProductService productService)
        {
            this.productService = productService;
        }
        public IActionResult Index(int page = 1)
        {
            var products = productService.GetAllProducts();
            var productsOnCurrentPage = CreatePagination(products, page);
            ViewData["methodForPagination"] = "Index";
            return View(productsOnCurrentPage);
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
            ViewData["methodForPagination"] = "Search/" + id;
            return View("Index", productsOnCurrentPage);
        }
    }
}
