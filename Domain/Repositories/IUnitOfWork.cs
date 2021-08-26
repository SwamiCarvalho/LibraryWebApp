using System.Threading.Tasks;

namespace LibraryWebApp.Domain.Repositories
{
    public interface IUnitOfWork
    {
        Task CompleteAsync();
    }
}