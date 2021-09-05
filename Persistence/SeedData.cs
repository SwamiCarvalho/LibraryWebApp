using LibraryWebApp.Persistence.Contexts;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryWebApp.Persistence
{
    public static class SeedData
    {
        public static void Initialize(UserManager<IdentityUser> userManager)
        {
            // Look for any Roles.
            if (userManager.Users.Any())
            {
                return;
            }

            var user = new IdentityUser { UserName = "Admin", Email = "admin@admin.pt" };

            IdentityResult result = userManager.CreateAsync(user, "admin").Result;

            if (result.Succeeded)
            {
                // Seed AspNetUserRoles Table
                userManager.AddToRoleAsync(user, "ADMIN").Wait();
            }
        }
    }
}
