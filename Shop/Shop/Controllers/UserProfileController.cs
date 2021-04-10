using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Shop.DataAccess;
using Shop.Extensions;
using Shop.Models;
using Shop.Services;
using System.Threading.Tasks;

namespace Shop.Controllers
{
    [Authorize]
    public class UserProfileController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly OrderService orderService;
        public UserProfileController(UserManager<ApplicationUser> userManager, OrderService orderService)
        {
            this.userManager = userManager;
            this.orderService = orderService;
        }

        public async Task<IActionResult> Index()
        {
            var user = await userManager.FindByIdAsync(userManager.GetUserId(User));
            var model = new UserViewModel { Id = user.Id, Email = user.Email, Name = user.FirstName, Surname = user.Surname, PhoneNumber = user.PhoneNumber };
            return View(model);
        }

        public async Task<IActionResult> Edit(string id)
        {
            var user = await userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            var model = new UserViewModel { Id = user.Id, Email = user.Email, Name = user.FirstName, Surname = user.Surname, PhoneNumber = user.PhoneNumber };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByIdAsync(model.Id);
                if (user != null)
                {
                    user.Email = model.Email;
                    user.UserName = model.Email;
                    user.FirstName = model.Name;
                    user.Surname = model.Surname;
                    user.PhoneNumber = model.PhoneNumber;
                    var result = await userManager.UpdateAsync(user);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        result.AddErrorsTo(ModelState);
                    }
                }
            }
            return View(model);
        }

        public async Task<IActionResult> ChangePassword(string id)
        {
            var user = await userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            var model = new UserViewModel { Id = user.Id, Email = user.Email, Name = user.FirstName };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(UserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByIdAsync(model.Id);
                if (user != null)
                {
                    var result = await userManager.ChangePasswordAsync(user, model.Password, model.NewPassword);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        result.AddErrorsTo(ModelState);
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Пользователь не найден");
                }
            }
            return View(model);
        }
        public async Task<IActionResult> GetAllOrders()
        {
            var user = await userManager.FindByIdAsync(userManager.GetUserId(User));
            var userViewModel = new UserViewModel { Id = user.Id, Email = user.Email, Name = user.FirstName, Surname = user.Surname, PhoneNumber = user.PhoneNumber };
            var userOrdersViewModel = new UserOrdersViewModel()
            {
                Orders = orderService.GetAllByUserId(userManager.GetUserId(User)),
                User = userViewModel
            };
            return View(userOrdersViewModel);
        }
    }
}
