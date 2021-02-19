using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Shop.Extensions
{
    public static class IdentityResultExtensions
    {
        public static void AddErrorsTo(this IdentityResult result, ModelStateDictionary modelState)
        {
            foreach (var error in result.Errors)
            {
                modelState.AddModelError(string.Empty, error.Description);
            }
        }
    }
}
