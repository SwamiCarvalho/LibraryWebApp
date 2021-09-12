using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace LibraryWebApp.Domain.Services.Communication
{
    public class RoleResponse : BaseResponse
    {
        public IdentityRole Role { get; private set; }
        public List<IdentityRole> RolesList { get; private set; }
        public string RoleString { get; private set; }
        public bool RoleFound { get; private set; }

        private RoleResponse(bool success, string message, IdentityRole role, List<IdentityRole> rolesList, string roleString, bool roleFound) : base(success, message)
        {
            Role = role;
            RolesList = rolesList;
            RoleString = roleString;
        }

        /// <summary>
        /// Creates a success response.
        /// </summary>
        /// <param name="role">Saved category.</param>
        /// <returns>Response.</returns>
        public RoleResponse(IdentityRole role) : this(true, string.Empty, role, null, null, true)
        { }

        /// <summary>
        /// Creates am error response.
        /// </summary>
        /// <param name="message">Error message.</param>
        /// <returns>Response.</returns>
        public RoleResponse(string message) : this(false, message, null, null, null, false)
        { }

        public RoleResponse(List<IdentityRole> rolesList) : this(true, string.Empty, null, rolesList, null, true)
        { }
        public RoleResponse(string roleString, bool roleFound) : this(true, string.Empty, null, null, roleString, true)
        { }
    }
}