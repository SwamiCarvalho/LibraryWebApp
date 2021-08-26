using LibraryWebApp.Domain.Models;
using LibraryWebApp.Domain.Services.Communication;
using System.Threading.Tasks;

namespace LibraryWebApp.Services
{
    public interface IBookingService
    {
        Task<BookingResponse> GetAllBookingsAsync();
        Task<BookingResponse> GetUserBookingsAsync(long id);
        Task<BookingResponse> GetBookingByIdAsync(long id);
        Task<BookingResponse> SaveBookingAsync(Booking booking);
        Task<BookingResponse> UpdateBookingAsync(long id, Booking booking);
        Task<BookingResponse> DeleteBookingAsync(long id);
    }
}
