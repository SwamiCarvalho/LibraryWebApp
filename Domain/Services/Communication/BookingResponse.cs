using LibraryWebApp.Domain.Models;
using System.Collections.Generic;

namespace LibraryWebApp.Domain.Services.Communication
{
    public class BookingResponse : BaseResponse
    {
        public Booking Booking { get; private set; }
        public IEnumerable<Booking> Bookings { get; private set; }

        private BookingResponse(bool success, string message, Booking booking, IEnumerable<Booking> bookings) : base(success, message)
        {
            Booking = booking;
        }

        /// <summary>
        /// Creates a success response.
        /// </summary>
        /// <param name="booking">Saved category.</param>
        /// <returns>Response.</returns>
        public BookingResponse(Booking booking) : this(true, string.Empty, booking, null)
        { }

        /// <summary>
        /// Creates am error response.
        /// </summary>
        /// <param name="message">Error message.</param>
        /// <returns>Response.</returns>
        public BookingResponse(string message) : this(false, message, null, null)
        { }

        public BookingResponse(IEnumerable<Booking> bookings) : this(true, string.Empty, null, bookings)
        { }
    }
}