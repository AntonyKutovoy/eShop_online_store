using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Shop.DataAccess;
using Shop.Models;
using Microsoft.AspNetCore.Authorization;
using Shop.Extensions;

namespace CustomIdentityApp.Controllers
{
    [Authorize(Roles = "Admin")]
    public class RolesController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public RolesController(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public IActionResult Index() => View(_roleManager.Roles.ToList());

        public IActionResult Create() => View();

        [HttpPost]
        public IActionResult Create(string name)
        {
            if (!string.IsNullOrEmpty(name))
            {
                var result = _roleManager.CreateAsync(new IdentityRole(name)).Result;
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    result.AddErrorsTo(ModelState);
                }
            }
            return View();
        }

        [HttpPost]
        public IActionResult Delete(string id)
        {
            var role = _roleManager.FindByIdAsync(id).Result;
            if (role != null)
            {
                var result = _roleManager.DeleteAsync(role).Result;
                if (!result.Succeeded)
                {
                    result.AddErrorsTo(ModelState);
                }
            }
            return RedirectToAction("Index");
        }

        public IActionResult UserList() => View(_userManager.Users.ToList());

        public IActionResult Edit(string userId)
        {
            // получаем пользователя
            var user = _userManager.FindByIdAsync(userId).Result;
            if (user != null)
            {
                // получем список ролей пользователя
                var userRoles = _userManager.GetRolesAsync(user).Result;
                var allRoles = _roleManager.Roles.ToList();
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
        public IActionResult Edit(string userId, List<string> roles)
        {
            // получаем пользователя
            var user = _userManager.FindByIdAsync(userId).Result;
            if (user != null)
            {
                // получем список ролей пользователя
                var userRoles = _userManager.GetRolesAsync(user).Result;

                // получаем список ролей, которые были добавлены
                var addedRoles = roles.Except(userRoles);
                // получаем роли, которые были удалены
                var removedRoles = userRoles.Except(roles);

                var result = _userManager.AddToRolesAsync(user, addedRoles).Result;
                if (!result.Succeeded)
                {
                    result.AddErrorsTo(ModelState);
                    return RedirectToAction("UserList");
                }
                result = _userManager.RemoveFromRolesAsync(user, removedRoles).Result;
                if (!result.Succeeded)
                {
                    result.AddErrorsTo(ModelState);
                }
                return RedirectToAction("UserList");
            }

            return NotFound();        
        }
    }
}
