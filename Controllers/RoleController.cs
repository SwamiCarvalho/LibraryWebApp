using LibraryWebApp.Domain.Models;
using LibraryWebApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace LibraryWebApp.Controllers
{
    [Authorize]
    public class RoleController : Controller
    {
        private readonly IUsersRolesService _usersRolesService;
        private readonly IGeneralService _generalService;

        public RoleController(IUsersRolesService usersRolesService, IGeneralService generalService)
        {
            _usersRolesService = usersRolesService;
            _generalService = generalService;
    }

        public async Task<IActionResult> Index()
        {
            var result = _usersRolesService.GetAllRoles();

            if (!result.Success)
            {
                ViewData["Feedback"] = result.Message;
                View("Error", new ErrorViewModel());
            }

            var myRoles = await _generalService.GetUserRoles(this.User);
            if (myRoles.Success)
            {
                ViewBag.MyRole = myRoles.RoleString;
            }
            else
            {
                ViewBag.MyRole = null;
                ViewBag.Feedback = myRoles.Message;
            }
                

            return View(result.RolesList);
        }

        public IActionResult Create()
        {
            return View(new IdentityRole());
        }

        [HttpPost]
        public async Task<IActionResult> Create(IdentityRole role)
        {
            var result = await _usersRolesService.SaveRoleAsync(role);
            if (!result.Success)
            {
                ViewData["Feedback"] = result.Message;
                View("Error", new ErrorViewModel());
            }
            return RedirectToAction("Index");
        }

        [Route("Role/Delete")]
        public async Task<IActionResult> Delete()
        {
            var roleName = Request.Form["RoleName"];
            var result = await _usersRolesService.GetRoleByName(roleName);
            if(!result.Success)
            {
                ViewData["Feedback"] = result.Message;
                View("Error", new ErrorViewModel());
            }

            var deleteResult = await _usersRolesService.DeleteRoleAsync(result.Role);
            if (!deleteResult.Success)
            {
                ViewData["Feedback"] = deleteResult.Message;
                View("Error", new ErrorViewModel());
            }
            return RedirectToAction("Index");
        }


    }
}