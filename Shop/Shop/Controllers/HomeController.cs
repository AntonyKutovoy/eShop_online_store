using Microsoft.AspNetCore.Mvc;
using Shop.DataAccess;
using Shop.Services;
using System.Linq;

namespace Shop.Controllers
{
    public class HomeController : Controller
    {
        private const int productsCountPerPage = 8;
        private readonly ProductService productService;

        public HomeController(ProductService productService)
        {
            this.productService = productService;
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
            return View(productsOnCurrentPage);
        }
    }
}
