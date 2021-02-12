using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace Shop.DataAccess
{
    public class AppIdentityDbContextSeeder
    {
        public static async Task SeedAsync(UserManager<ApplicationUser> userManager)
        {
            var defaultUser = new ApplicationUser { UserName = "demouser@microsoft.com", Email = "demouser@microsoft.com" };
            await userManager.CreateAsync(defaultUser, "Pass@word1");
            await userManager.AddToRoleAsync(defaultUser, "Admin");
        }
    }
}
