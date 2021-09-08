using LibraryWebApp.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LibraryWebApp.Domain.Repositories
{
    public interface ILibrarianRepository
    {
        Task<IEnumerable<Librarian>> ListAsync();
        void AddLibrarian(Librarian librarian);
        void UpdateLibrarian(Librarian librarian);
        void DeleteLibrarian(Librarian librarian);
        Task<Librarian> GetLibrarianByIdAsync(long id);
    }
}
