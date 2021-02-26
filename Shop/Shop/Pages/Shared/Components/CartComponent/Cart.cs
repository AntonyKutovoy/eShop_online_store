using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Shop.DataAccess;
using Shop.Models;
using Shop.Services;
using System.Threading.Tasks;

namespace Shop.Pages.Shared.Components.BasketComponent
{
    public class Cart : ViewComponent
    {
        private readonly CartService _cartService;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public Cart(CartService basketService, SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
        {
            _cartService = basketService;
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public async Task<IViewComponentResult> InvokeAsync(string userName)
        {
            var itemsCount = GetBasketViewModel().AllAmount;
            return View(itemsCount);
        }

        private CartViewModel GetBasketViewModel()
        {
            if (_signInManager.IsSignedIn(HttpContext.User))
            {
                var userId = _userManager.GetUserId(HttpContext.User);
                return _cartService.GetCurrentCart(userId);
            }
            string anonymousId = GetBasketIdFromCookie();
            if (anonymousId == null)
                return new CartViewModel();
            return _cartService.GetCurrentCart(anonymousId);
        }

        private string GetBasketIdFromCookie()
        {
            if (Request.Cookies.ContainsKey(Constants.BASKET_COOKIENAME))
            {
                return Request.Cookies[Constants.BASKET_COOKIENAME];
            }
            return null;
        }
    }
}
