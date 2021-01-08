using Microsoft.AspNetCore.Mvc;
using Shop.DataAccess;

namespace Shop.Controllers
{
    public class CartController : Controller
    {
        private readonly IProductRepository repository;
        public CartController(IProductRepository repository)
        {
            this.repository = repository;
        }
        public ViewResult Index()
        {
            return View();
        }
    }
}
