using System.Threading.Tasks;
using LibraryWebApp.Domain.Repositories;
using LibraryWebApp.Persistence.Contexts;

namespace LibraryWebApp.Persistence.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }

        public async Task CompleteAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}