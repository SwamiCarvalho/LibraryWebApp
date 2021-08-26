using System.Threading.Tasks;
using System;
using LibraryWebApp.Domain.Repositories;
using LibraryWebApp.Domain.Models;
using LibraryWebApp.Domain.Services.Communication;


namespace LibraryWebApp.Services
{
    public class BookingService : IBookingService
    {

        public IBookingRepository _bookingRepository;
        public readonly IUnitOfWork _unitOfWork;

        public BookingService(IBookingRepository bookingRepository, IUnitOfWork unitOfWork)
        {
            _bookingRepository = bookingRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<BookingResponse> GetAllBookingsAsync()
        {
            var bookings = await _bookingRepository.ListAsync();

            if (bookings == null)
                return new BookingResponse("There is no bookings.");

            return new BookingResponse(bookings);
        }

        public async Task<BookingResponse> GetUserBookingsAsync(long id)
        {
            var userBookings = await _bookingRepository.ListUserBookingsAsync(id);

            if (userBookings == null)
                return new BookingResponse("There is no bookings.");

            return new BookingResponse(userBookings);
        }

        public async Task<BookingResponse> GetBookingByIdAsync(long id)
        {
            var booking = await _bookingRepository.GetBookingByIdAsync(id);

            if (booking == null)
                return new BookingResponse("Booking not found.");

            return new BookingResponse(booking);
        }

        public async Task<BookingResponse> SaveBookingAsync(Booking booking)
        {
            try
            {
                _bookingRepository.AddBooking(booking);
                await _unitOfWork.CompleteAsync();

                return new BookingResponse(booking);
            }
            catch (Exception ex)
            {
                return new BookingResponse($"An error occurred when saving the booking: {ex.Message}");
            }
        }

        public async Task<BookingResponse> UpdateBookingAsync(long id, Booking booking)
        {
            var existingBooking = await _bookingRepository.GetBookingByIdAsync(id);

            if (existingBooking == null)
                return new BookingResponse("Booking not found.");

            existingBooking.Status = booking.Status;

            try
            {
                _bookingRepository.UpdateBooking(existingBooking);
                await _unitOfWork.CompleteAsync();

                return new BookingResponse(existingBooking);
            }
            catch (Exception ex)
            {
                return new BookingResponse($"An error occurred when updating the booking: {ex.Message}");
            }
        }

        public async Task<BookingResponse> DeleteBookingAsync(long id)
        {
            var existingBooking = await _bookingRepository.GetBookingByIdAsync(id);

            if (existingBooking == null)
                return new BookingResponse("Booking not found.");

            try
            {
                _bookingRepository.DeleteBooking(existingBooking);
                await _unitOfWork.CompleteAsync();

                return new BookingResponse(existingBooking);
            }
            catch (Exception ex)
            {
                // Do some logging stuff
                return new BookingResponse($"An error occurred when deleting the booking: {ex.Message}");
            }
        }

        /*public async Task<BookResponse> GetBookingBooks(long id)
        {
            var booking = await _bookingRepository.GetBookingByIdAsync(id);

            if (booking == null)
                return new BookResponse("Booking not found.");

            var bookingBooks = booking.Books;

            return new BookResponse(bookingBooks);
        }
    }*/
    }
}