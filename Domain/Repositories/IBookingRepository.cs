using LibraryWebApp.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LibraryWebApp.Domain.Repositories
{
    public interface IBookingRepository
    {
        Task<IEnumerable<Booking>> ListAsync();
        Task<IEnumerable<Booking>> ListReaderBookingsAsync(long readerId);
        Task<Booking> GetBookingByIdAsync(long id);
        void AddBooking(Booking booking);
        void UpdateBooking(Booking booking);
        void DeleteBooking(Booking booking);
        Task<Booking> GetBookingByBookIdAsync(long id);
    }
}
