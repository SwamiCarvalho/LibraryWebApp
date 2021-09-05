using System.Threading.Tasks;
using System;
using LibraryWebApp.Domain.Services.Communication;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using LibraryWebApp.Domain.Repositories;

namespace LibraryWebApp.Services
{
    public class UsersRolesService : IUsersRolesService
    {
        UserManager<IdentityUser> _userManager;
        RoleManager<IdentityRole> _roleManager;
        public readonly IUnitOfWork _unitOfWork;

        public UsersRolesService(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _unitOfWork = unitOfWork;
        }
        public RoleResponse GetAllRoles()
        {
            var roles = _roleManager.Roles.ToList();

            if (!roles.Any())
                return new RoleResponse("The is no roles listed.");

            return new RoleResponse(roles);
        }

        public async Task<RoleResponse> GetUserRolesAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return new RoleResponse("You don't have any Role.");

            var userRoles = await _userManager.GetRolesAsync(user);

            return new RoleResponse(userRoles);
        }

        public async Task<RoleResponse> CheckRoleExistence(string roleName)
        {
            var role = await _roleManager.RoleExistsAsync(roleName);

            if (!role)
                return new RoleResponse("The Role does not exist.");

            return new RoleResponse("The Role exists.");
        }

        public async Task<RoleResponse> GetRoleById(string roleId)
        {
            var role = await _roleManager.FindByIdAsync(roleId);

            if (role == null)
                return new RoleResponse("The Role does not exist.");

            return new RoleResponse(role);
        }

        public async Task<RoleResponse> GetRoleByName(string roleName)
        {
            var role = await _roleManager.FindByNameAsync(roleName);

            if (role == null)
                return new RoleResponse("The Role does not exist.");

            return new RoleResponse(role);
        }

        public async Task<RoleResponse> SaveRoleAsync(IdentityRole role)
        {
            try
            {
                var newRole = await _roleManager.CreateAsync(role);

                return new RoleResponse(role);
            }
            catch (Exception ex)
            {
                return new RoleResponse($"An error occurred when saving the role: {ex.Message}");
            }
        }

        public async Task<RoleResponse> UpdateRoleAsync(IdentityRole role)
        {
            try
            {
                var existingRole = await _roleManager.UpdateAsync(role);

                if (!existingRole.Succeeded)
                    return new RoleResponse("Role not updated.");
                await _unitOfWork.CompleteAsync();

                return new RoleResponse(role);
            }
            catch (Exception ex)
            {
                return new RoleResponse($"An error occurred when updating the role: {ex.Message}");
            }
        }

        public async Task<RoleResponse> DeleteRoleAsync(IdentityRole role)
        {

            try
            {
                var existingRole = await _roleManager.DeleteAsync(role);

                if (!existingRole.Succeeded)
                    return new RoleResponse("Role not deleted.");

                await _unitOfWork.CompleteAsync();

                return new RoleResponse(role);
            }
            catch (Exception ex)
            {
                // Do some logging stuff
                return new RoleResponse($"An error occurred when deleting the role: {ex.Message}");
            }
        }

        public async Task<IList<IdentityUser>> GetUsersByRole(string role)
        {
            var users = await _userManager.GetUsersInRoleAsync(role);

            return users;
        }

        public async Task<IdentityUser> GetUserById(System.Security.Claims.ClaimsPrincipal claims)
        {
            var user = await _userManager.GetUserAsync(claims);
            return user;
        }
    }
}
