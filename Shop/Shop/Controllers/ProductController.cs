using Microsoft.AspNetCore.Mvc;
using Shop.Services;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Controllers
{
    public class ProductController : Controller
    {
        public ProductController()
        { 

        }

        //private const int productsCountPerPage = 8;
        //private readonly IProductRepository repository;

        //public HomeController(IProductRepository repository)
        //{
        //    this.repository = repository;
        //}
        //public IActionResult Index(int page = 1)
        //{
        //    var products = repository.GetAll();
        //    var productsOnCurrentPage = products.Skip((page - 1) * productsCountPerPage).Take(productsCountPerPage).ToList();

        //    var countPages = products.Count / productsCountPerPage;
        //    if (products.Count % productsCountPerPage > 0)
        //    {
        //        countPages++;
        //    }
        //    ViewData["countPages"] = countPages;
        //    return View(productsOnCurrentPage);
        //}
    }
}
