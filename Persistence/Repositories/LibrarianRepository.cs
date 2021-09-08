using LibraryWebApp.Domain.Models;
using LibraryWebApp.Domain.Repositories;
using LibraryWebApp.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LibraryWebApp.Persistence.Repositories
{
    public class LibrarianRepository : RepositoryBase<Librarian>, ILibrarianRepository
    {

        public LibrarianRepository(AppDbContext context) : base(context) { }

        public async Task<IEnumerable<Librarian>> ListAsync()
        {
            return await FindAll().ToListAsync();
        }

        public void AddLibrarian(Librarian librarian)
        {
            Create(librarian);
        }
        public void UpdateLibrarian(Librarian librarian)
        {
            Update(librarian);
        }
        public void DeleteLibrarian(Librarian librarian)
        {
            Delete(librarian);
        }
        public async Task<Librarian> GetLibrarianByIdAsync(long id)
        {
            return await FindByCondition(b => b.LibrarianId == id).FirstOrDefaultAsync();
        }

        /*public async Task<Librarian> GetLibrarianBookingsAsync(long id)
        {
            return await FindByCondition(b => b.R == id && b.Status != "Delivered").FirstOrDefaultAsync();
        }*/
    }
}