using Microsoft.AspNetCore.Mvc;
using Shop.DataAccess;
using Shop.Services;

namespace Shop.Controllers
{
    public class CartController : Controller
    {
        private readonly CartService cartService;

        public CartController(CartService cartService)
        {
            this.cartService = cartService;
        }
        public ViewResult Index()
        {
            return View();
        }
    }
}
