using LibraryWebApp.Domain.Models;
using LibraryWebApp.Resources;
using System.Collections.Generic;

namespace LibraryWebApp.Domain.Services.Communication
{
    public class BookingResponse : BaseResponse
    {
        public BookingResource Booking { get; private set; }
        public IEnumerable<BookingResource> Bookings { get; private set; }

        private BookingResponse(bool success, string message, BookingResource booking, IEnumerable<BookingResource> bookings) : base(success, message)
        {
            Booking = booking;
            Bookings = bookings;
        }

        /// <summary>
        /// Creates a success response.
        /// </summary>
        /// <param name="booking">Saved category.</param>
        /// <returns>Response.</returns>
        public BookingResponse(BookingResource booking) : this(true, string.Empty, booking, null)
        { }

        /// <summary>
        /// Creates am error response.
        /// </summary>
        /// <param name="message">Error message.</param>
        /// <returns>Response.</returns>
        public BookingResponse(string message) : this(false, message, null, null)
        { }

        public BookingResponse(IEnumerable<BookingResource> bookings) : this(true, string.Empty, null, bookings)
        { }
    }
}