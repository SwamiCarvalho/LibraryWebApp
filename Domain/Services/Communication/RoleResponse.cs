using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace LibraryWebApp.Domain.Services.Communication
{
    public class RoleResponse : BaseResponse
    {
        public IdentityRole Role { get; private set; }
        public List<IdentityRole> RolesList { get; private set; }
        public IList<string> RolesIList { get; private set; }

        private RoleResponse(bool success, string message, IdentityRole role, List<IdentityRole> rolesList, IList<string> rolesIList) : base(success, message)
        {
            Role = role;
            RolesList = rolesList;
            RolesIList = rolesIList;
        }

        /// <summary>
        /// Creates a success response.
        /// </summary>
        /// <param name="role">Saved category.</param>
        /// <returns>Response.</returns>
        public RoleResponse(IdentityRole role) : this(true, string.Empty, role, null, null)
        { }

        /// <summary>
        /// Creates am error response.
        /// </summary>
        /// <param name="message">Error message.</param>
        /// <returns>Response.</returns>
        public RoleResponse(string message) : this(false, message, null, null, null)
        { }

        public RoleResponse(List<IdentityRole> rolesList) : this(true, string.Empty, null, rolesList, null)
        { }
        public RoleResponse(IList<string> rolesIList) : this(true, string.Empty, null, null, rolesIList)
        { }
    }
}