using LibraryWebApp.Domain.Services.Communication;
using LibraryWebApp.Resources;
using System.Threading.Tasks;

namespace LibraryWebApp.Services
{
    public interface ILibrarianService
    {
        Task<LibrarianResponse> GetAllLibrariansAsync();
        Task<LibrarianResponse> GetLibrarianByIdAsync(long id);
        Task<LibrarianResponse> SaveLibrarianAsync(CreateUserResource librarian);
        //Task<LibrarianResponse> UpdateLibrarianAsync(long id, UpdateLibrarianResource librarian);
        Task<LibrarianResponse> DeleteLibrarianAsync(long id);
    }
}
