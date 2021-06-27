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
        private readonly CartService cartService;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly UserManager<ApplicationUser> userManager;

        public Cart(CartService basketService, SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
        {
            cartService = basketService;
            this.signInManager = signInManager;
            this.userManager = userManager;
        }

        public async Task<IViewComponentResult> InvokeAsync(string userName)
        {
            var itemsCount = GetBasketViewModel().AllAmount;
            return View(itemsCount);
        }

        private CartViewModel GetBasketViewModel()
        {
            if (signInManager.IsSignedIn(HttpContext.User))
            {
                var userId = userManager.GetUserId(HttpContext.User);
                return cartService.GetCurrentCart(userId);
            }
            string anonymousId = GetBasketIdFromCookie();
            if (anonymousId == null)
                return new CartViewModel();
            return cartService.GetCurrentCart(anonymousId);
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
