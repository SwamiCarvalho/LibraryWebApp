using LibraryWebApp.Domain.Services.Communication;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LibraryWebApp.Services
{
    public interface IUsersRolesService
    {
        // Roles
        RoleResponse GetAllRoles();
        Task<RoleResponse> GetUserRolesAsync(string userId);
        Task<RoleResponse> CheckRoleExistence(string roleName);
        Task<RoleResponse> GetRoleById(string roleId);
        Task<RoleResponse> GetRoleByName(string roleName);
        Task<RoleResponse> SaveRoleAsync(IdentityRole role);
        Task<RoleResponse> UpdateRoleAsync(IdentityRole role);
        Task<RoleResponse> DeleteRoleAsync(IdentityRole role);

        // Users
        Task<IList<IdentityUser>> GetUsersByRole(string role);
        Task<IdentityUser> GetUserById(System.Security.Claims.ClaimsPrincipal claims);
    }
}
