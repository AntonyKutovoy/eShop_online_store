using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly OrderService orderService;

        public AdminController(OrderService orderService)
        {
            this.orderService = orderService;
        }

        public IActionResult GetOrders()
        {
            return View(orderService.GetAll());
        }

        public IActionResult GetOrder(Guid id)
        {
            return View(orderService.GetOrder(id));
        }

        public IActionResult ChangeStatus(string status, Guid id)
        {
            orderService.ChangeStatus(status, id);
            return RedirectToAction("GetOrder", new { id = id });
        }
    }
}
