using System.Threading.Tasks;
using LibraryWebApp.Domain.Services.Communication;

namespace LibraryWebApp.Services
{
    public class GeneralService : IGeneralService
    {

        public ILibrarianService _librarianService;
        public IReaderService _readerService;
        public IUsersRolesService _usersRolesService;


        public GeneralService(ILibrarianService librarianService, IReaderService readerService, IUsersRolesService usersRolesService)
        {
            _librarianService = librarianService;
            _readerService = readerService;
            _usersRolesService = usersRolesService;
        }
        public async Task<ReaderResponse> GetReader(System.Security.Claims.ClaimsPrincipal claim)
        {
            var identity = await _usersRolesService.GetUserByClaim(claim);

            if (identity != null)
            {
                var result = await _readerService.GetReaderByIdentityIdAsync(identity.Id);

                if (!result.Success)
                    return new ReaderResponse("An error occurred when trying to retrieve the user data from the authenticated user.");

                return new ReaderResponse(result.Reader);
            }
            return new ReaderResponse("You are not logged in, so we can't retrieve your info.");
        }


        public async Task<RoleResponse> GetUserRoles(System.Security.Claims.ClaimsPrincipal claim)
        {
            var identity = await _usersRolesService.GetUserByClaim(claim);

            if(identity != null)
            {
                var result = await _usersRolesService.GetUserRolesAsync(identity.Id);

                if (!result.Success)
                    return new RoleResponse("An error occurred when trying to retrieve the roles from the authenticated user.");

                return new RoleResponse(result.RolesIList);
            }
            return new RoleResponse("You are not logged in, so we can't see your role.");

        }
        
    }
}