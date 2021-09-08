using LibraryWebApp.Domain.Services.Communication;
using System.Threading.Tasks;

namespace LibraryWebApp.Services
{
    public interface IGeneralService
    {
        // Roles
        Task<ReaderResponse> GetReader(System.Security.Claims.ClaimsPrincipal claim);
        Task<RoleResponse> GetUserRoles(System.Security.Claims.ClaimsPrincipal claim);
    }
}
