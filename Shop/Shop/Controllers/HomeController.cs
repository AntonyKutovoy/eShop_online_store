using Microsoft.AspNetCore.Mvc;
using Shop.Services;

namespace Shop.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProductRepository repository;

        public HomeController(IProductRepository repository)
        {
            this.repository = repository;
        }
        public IActionResult Index()
        {
            return View(repository.GetAll());
        }
    }
}
