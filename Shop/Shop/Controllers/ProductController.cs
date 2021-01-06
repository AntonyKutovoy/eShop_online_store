using Microsoft.AspNetCore.Mvc;
using Shop.Models;
using Shop.Services;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
