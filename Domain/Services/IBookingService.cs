using LibraryWebApp.Domain.Services.Communication;
using LibraryWebApp.Resources;
using System.Threading.Tasks;

namespace LibraryWebApp.Services
{
    public interface IBookingService
    {
        Task<BookingResponse> GetAllBookingsAsync();
        Task<BookingResponse> GetUserBookingsAsync(long id);
        Task<BookingResponse> GetBookingByIdAsync(long id);
        Task<BookingResponse> SaveBookingAsync(CreateBookingResource booking);
        Task<BookingResponse> UpdateBookingAsync(long id, UpdateBookingResource booking);
        Task<BookingResponse> DeleteBookingAsync(long id);
    }
}
