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

        public IActionResult GetProduct(Product product)
        {
            var products = repository.GetAll();
            var selectedProduct = products.FirstOrDefault(i => i.Id == product.Id);
            selectedProduct = product;
            return View();
        }


    }
}
