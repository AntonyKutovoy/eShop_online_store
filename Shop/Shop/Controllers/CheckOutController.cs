using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Shop.DataAccess;
using Shop.Models;
using Shop.Services;
using System;
using System.Collections.Generic;

namespace Shop.Controllers
{
    public class CheckOutController : Controller
    {
        private readonly OrderService orderService;
        private readonly ProductService productService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly CartService cartService;
        public CheckOutController(CartService cartService, OrderService orderService, ProductService productService, UserManager<ApplicationUser> userManager)
        {
            this.orderService = orderService;
            this.userManager = userManager;
            this.productService = productService;
            this.cartService = cartService;
        }

        public IActionResult Thanks()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateOrder(Dictionary<Guid, int> items)
        {
            var orderViewModel = new OrderViewModel();
            ProductViewModel product;
            foreach (var item in items)
            {
                product = productService.GetProduct(item.Key);
                orderViewModel.OrderItems.Add(new OrderItemViewModel { Product = product, Amount = item.Value, Id = Guid.NewGuid() });
            }
            return View(orderViewModel);
        }

        [HttpPost]
        public IActionResult SaveOrder(Dictionary<Guid, int> items, OrderViewModel orderViewModel)
        {
            ProductViewModel product;
            foreach (var item in items)
            {
                product = productService.GetProduct(item.Key);
                orderService.AddProductToOrder(userManager.GetUserId(User), product, item.Value);
            }
            orderService.AddInformation(userManager.GetUserId(User), orderViewModel);
            cartService.DeleteCart(userManager.GetUserId(User));
            return RedirectToAction("Thanks");
        }
    }
}
