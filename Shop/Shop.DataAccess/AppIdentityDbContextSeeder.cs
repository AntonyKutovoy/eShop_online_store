using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace Shop.DataAccess
{
    public class AppIdentityDbContextSeeder
    {
        public static async Task SeedAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {

            var defaultUser = new ApplicationUser { UserName = "demouser@microsoft.com", Email = "demouser@microsoft.com" };
            if ((await userManager.FindByEmailAsync("demouser@microsoft.com")) == null)
            {
                await userManager.CreateAsync(defaultUser, "Pass@word1");
                var roleName = "Admin";
                await roleManager.CreateAsync(new IdentityRole(roleName));
                await userManager.AddToRoleAsync(defaultUser, roleName);
            }
        }
    }
}
