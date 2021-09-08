using LibraryWebApp.Domain.Services.Communication;
using LibraryWebApp.Resources;
using System.Threading.Tasks;

namespace LibraryWebApp.Services
{
    public interface IReaderService
    {
        Task<ReaderResponse> GetAllReadersAsync();
        Task<ReaderResponse> GetReaderByIdAsync(long id);
        Task<ReaderResponse> GetReaderByIdentityIdAsync(string id);
        Task<ReaderResponse> SaveReaderAsync(CreateUserResource reader);
        Task<ReaderResponse> UpdateReaderAsync(long id, UpdateReaderResource reader);
        Task<ReaderResponse> DeleteReaderAsync(long id);
    }
}
