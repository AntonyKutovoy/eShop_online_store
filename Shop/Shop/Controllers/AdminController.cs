using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Shop.DataAccess;
using Shop.Extensions;
using Shop.Models;
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
        private readonly ProductService productService;
        private readonly OrderService orderService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public AdminController(ProductService productService, OrderService orderService, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            this.productService = productService;
            this.orderService = orderService;
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        public IActionResult GetOrders() => View(orderService.GetAll());

        public IActionResult GetOrder(Guid id) 
        {
            return View(orderService.GetOrder(id));
        }

        public IActionResult ChangeOrderStatus(string status, Guid id)
        {
            orderService.ChangeStatus(status, id);
            return RedirectToAction("GetOrder", new { id = id });
        }

        public IActionResult GetUsers() => View(userManager.Users.ToList());

        public IActionResult CreateUser() => View();

        [HttpPost]
        public async Task<IActionResult> CreateUser(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { Email = model.Email, UserName = model.Email };
                var result = await userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    return RedirectToAction("GetUsers");
                }
                else
                {
                    result.AddErrorsTo(ModelState);
                }
            }
            return View(model);
        }

        public async Task<IActionResult> GetUser(string id)
        {
            var user = await userManager.FindByIdAsync(id);
            var model = new UserViewModel { Id = user.Id, Email = user.Email, Name = user.FirstName, Surname = user.Surname, PhoneNumber = user.PhoneNumber };
            return View(model);
        }

        public async Task<IActionResult> ChangeUserPassword(string id)
        {
            var user = await userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            var model = new UserViewModel { Id = user.Id, Email = user.Email };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ChangeUserPassword(UserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByIdAsync(model.Id);
                if (user != null)
                {
                    var result = await userManager.ChangePasswordAsync(user, model.Password, model.NewPassword);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("GetUser", new { id = model.Id });
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

        public async Task<IActionResult> EditUser(string id)
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
        public async Task<IActionResult> EditUser(UserViewModel model)
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
                        return RedirectToAction("GetUser", new { id = model.Id });
                    }
                    else
                    {
                        result.AddErrorsTo(ModelState);
                    }
                }
            }
            return View(model);
        }

        public async Task<ActionResult> DeleteUser(string id)
        {
            var user = await userManager.FindByIdAsync(id);
            if (user != null)
            {
                var result = await userManager.DeleteAsync(user);
                if (!result.Succeeded)
                {
                    result.AddErrorsTo(ModelState);
                }
            }
            return RedirectToAction("GetUsers");
        }

        public IActionResult GetRoles() => View(roleManager.Roles.ToList());

        public async Task<ActionResult> DeleteRole(string id)
        {
            var role = await roleManager.FindByIdAsync(id);
            if (role != null)
            {
                var result = await roleManager.DeleteAsync(role);
                if (!result.Succeeded)
                {
                    result.AddErrorsTo(ModelState);
                }
            }
            return RedirectToAction("GetRoles");
        }

        public IActionResult CreateRole() => View();

        [HttpPost]
        public async Task<ActionResult> CreateRole(string name)
        {
            if (!string.IsNullOrEmpty(name))
            {
                var result = await roleManager.CreateAsync(new IdentityRole(name));
                if (result.Succeeded)
                {
                    return RedirectToAction("GetRoles");
                }
                else
                {
                    result.AddErrorsTo(ModelState);
                }
            }
            return View();
        }

        public async Task<ActionResult> EditUserRights(string id)
        {
            var user = await userManager.FindByIdAsync(id);
            if (user != null)
            {
                var userRoles = await userManager.GetRolesAsync(user);
                var allRoles = roleManager.Roles.ToList();
                var model = new ChangeRoleViewModel
                {
                    UserId = user.Id,
                    UserEmail = user.Email,
                    UserRoles = userRoles,
                    AllRoles = allRoles
                };
                return View(model);
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<ActionResult> EditUserRights(string id, List<string> roles)
        {
            var user = await userManager.FindByIdAsync(id);
            if (user != null)
            {
                var userRoles = await userManager.GetRolesAsync(user);
                var addedRoles = roles.Except(userRoles);
                var removedRoles = userRoles.Except(roles);
                var result = await userManager.AddToRolesAsync(user, addedRoles);
                if (!result.Succeeded)
                {
                    result.AddErrorsTo(ModelState);
                    return RedirectToAction("GetUser", new { id = id });
                }
                result = await userManager.RemoveFromRolesAsync(user, removedRoles);
                if (!result.Succeeded)
                {
                    result.AddErrorsTo(ModelState);
                }
                return RedirectToAction("GetUser", new { id = id });
            }
            return NotFound();
        }

        public IActionResult AddProduct() => View();

        [HttpPost]
        public IActionResult AddProduct(ProductViewModel productViewModel)
        {
            productService.Create(productViewModel);
            return RedirectToAction("AddProduct");
        }
    }
}
