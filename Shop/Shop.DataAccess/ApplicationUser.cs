using Microsoft.AspNetCore.Identity;

namespace Shop.DataAccess
{
    public class ApplicationUser : IdentityUser
    {
        public string Surname { get; set; }
    }
}
