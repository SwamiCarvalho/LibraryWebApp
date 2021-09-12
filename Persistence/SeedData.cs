using Microsoft.AspNetCore.Identity;
using System.Linq;

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
