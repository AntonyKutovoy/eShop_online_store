using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Controllers
{
    public class AboutController : Controller
    {
        public IActionResult Start()
        {
            return View("Index");
        }
    }
}
