using LibraryWebApp.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LibraryWebApp.Domain.Repositories
{
    public interface IReaderRepository
    {
        Task<IEnumerable<Reader>> ListAsync();
        void AddReader(Reader reader);
        void UpdateReader(Reader reader);
        void DeleteReader(Reader reader);
        Task<Reader> GetReaderByIdAsync(long id);
        Task<Reader> GetReaderByIdentityIdAsync(string id);
    }
}
