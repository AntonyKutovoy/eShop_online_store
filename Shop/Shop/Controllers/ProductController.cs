using Microsoft.AspNetCore.Mvc;
using Shop.DataAccess;

namespace Shop.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductRepository repository;

        public ProductController(IProductRepository repository)
        {
            this.repository = repository;
        }

        public IActionResult Index(int id)
        {
            var product = repository.Get(id);
            return View(product);
        }
    }
}
