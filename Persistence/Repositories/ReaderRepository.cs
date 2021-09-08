using LibraryWebApp.Domain.Models;
using LibraryWebApp.Domain.Repositories;
using LibraryWebApp.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LibraryWebApp.Persistence.Repositories
{
    public class ReaderRepository : RepositoryBase<Reader>, IReaderRepository
    {

        public ReaderRepository(AppDbContext context) : base(context) { }

        public async Task<IEnumerable<Reader>> ListAsync()
        {
            return await FindAll().ToListAsync();
        }

        public void AddReader(Reader reader)
        {
            Create(reader);
        }
        public void UpdateReader(Reader reader)
        {
            Update(reader);
        }
        public void DeleteReader(Reader reader)
        {
            Delete(reader);
        }
        public async Task<Reader> GetReaderByIdAsync(long id)
        {
            return await FindByCondition(b => b.ReaderId == id).FirstOrDefaultAsync();
        }

        public async Task<Reader> GetReaderByIdentityIdAsync(string id)
        {
            return await FindByCondition(b => b.UserId == id).FirstOrDefaultAsync();
        }

        /*public async Task<Reader> GetReaderBookingsAsync(long id)
        {
            return await FindByCondition(b => b.R == id && b.Status != "Delivered").FirstOrDefaultAsync();
        }*/
    }
}