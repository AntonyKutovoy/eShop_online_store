using Microsoft.AspNetCore.Mvc;
using Shop.Models;
using Shop.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Controllers
{
    public class CartController : Controller
    {
        private readonly IProductRepository repository;
        private readonly Cart cart;
        public CartController(IProductRepository repository, Cart cart)
        {
            this.repository = repository;
            this.cart = cart;
        }
        public ViewResult Index()
        {
            return View();
        }
    }
}
