using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Shop.DataAccess;
using Shop.Models;
using Microsoft.AspNetCore.Authorization;

namespace CustomIdentityApp.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UsersController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        public UsersController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public IActionResult Index() => View(_userManager.Users.ToList());

        public IActionResult Create() => View();

        [HttpPost]
        public IActionResult Create(CreateUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { Email = model.Email, UserName = model.Email };
                var result = _userManager.CreateAsync(user, model.Password).Result;
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            return View(model);
        }

        public IActionResult Edit(string id)
        {
            var user = _userManager.FindByIdAsync(id).Result;
            if (user == null)
            {
                return NotFound();
            }
            var model = new EditUserViewModel { Id = user.Id, Email = user.Email };
            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(EditUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = _userManager.FindByIdAsync(model.Id).Result;
                if (user != null)
                {
                    user.Email = model.Email;
                    user.UserName = model.Email;

                    var result = _userManager.UpdateAsync(user).Result;
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError(string.Empty, error.Description);
                        }
                    }
                }
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult Delete(string id)
        {
            var user = _userManager.FindByIdAsync(id).Result;
            if (user != null)
            {
                var result = _userManager.DeleteAsync(user).Result;
            }
            return RedirectToAction("Index");
        }

        public IActionResult ChangePassword(string id)
        {
            var user =  _userManager.FindByIdAsync(id).Result;
            if (user == null)
            {
                return NotFound();
            }
            var model = new ChangePasswordViewModel { Id = user.Id, Email = user.Email };
            return View(model);
        }

        [HttpPost]
        public IActionResult ChangePassword(ChangePasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = _userManager.FindByIdAsync(model.Id).Result;
                if (user != null)
                {
                    var _passwordValidator =
                        HttpContext.RequestServices.GetService(typeof(IPasswordValidator<ApplicationUser>)) as IPasswordValidator<ApplicationUser>;
                    var _passwordHasher =
                        HttpContext.RequestServices.GetService(typeof(IPasswordHasher<ApplicationUser>)) as IPasswordHasher<ApplicationUser>;

                    var result =
                        _passwordValidator.ValidateAsync(_userManager, user, model.NewPassword).Result;
                    if (result.Succeeded)
                    {
                        user.PasswordHash = _passwordHasher.HashPassword(user, model.NewPassword);
                        _userManager.UpdateAsync(user);
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError(string.Empty, error.Description);
                        }
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Пользователь не найден");
                }
            }
            return View(model);
        }
    }
}
