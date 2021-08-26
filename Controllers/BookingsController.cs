using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System;
using LibraryWebApp.Domain.Models;
using LibraryWebApp.Resources;
using AutoMapper;
using LibraryWebApp.Services;
using System.Net.Http;
using LibraryAPI.Resources;

namespace LibraryWebApp.Controllers
{
    public class BookingsController : Controller
    {
        private readonly IBookingService _bookingService;
        private readonly IMapper _mapper;

        // HttpClient is intended to be instantiated once per application, rather than per-use.
        private readonly HttpClient _client = new HttpClient();

        const string baseurl = "https://localhost:44351/api/";

        public BookingsController(IBookingService bookingService, IMapper mapper, HttpClient client)
        {
            _bookingService = bookingService;
            _mapper = mapper;
            _client = client;
        }

        // GET: Profile
        [Route("Bookings")]
        public async Task<ActionResult<IEnumerable<BookingResource>>> Index()
        {
            //TODO: depending on the user 
            ViewData["Title"] = "My Bookings";

            // Get all Bookings
            var result = await _bookingService.GetAllBookingsAsync();
            //Check if it retrieved anything
            if (!result.Success)
            {
                ViewData["Feedback"] = result.Message;
                View("Error", new ErrorViewModel());
            }

            if (result.Bookings == null)
                ViewData["Feedback"] = "You dont have any Bookings yet, \r\n feel free to book a book from us ;D";

            var bookingsResource = _mapper.Map<IEnumerable<Booking>, IEnumerable<BookingResource>>(result.Bookings);

            //returning the bookings list to view controller
            return View(bookingsResource);

        }

        // GET: Profile/Edit/5
        public async Task<IActionResult> Edit([FromRoute] long id)
        {
            var result = await _bookingService.GetBookingByIdAsync(id);

            if (!result.Success)
            {
                ViewData["Feedback"] = result.Message;
                return View("Error", new ErrorViewModel());
            }
                
            // Deserialize the updated product from the response body.
            var updatedBookingResource = _mapper.Map<Booking, BookingResource>(result.Booking);
            return View(updatedBookingResource);

        }

        // POST: Profile/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        public async Task<IActionResult> Update([Bind("BookingId,Status")] BookingResource bookingResource)
        {
            var booking = _mapper.Map<BookingResource, Booking>(bookingResource);
            var result = await _bookingService.UpdateBookingAsync(bookingResource.BookingId, booking);

            if (!result.Success)
            {
                ViewData["Feedback"] = result.Message;
                return View("Error", new ErrorViewModel());
            }

            // Deserialize the updated product from the response body.
            var updatedBooking = result.Booking;

            // TODO: Return value to Information Dialog
            var updatedBookingResource = _mapper.Map<Booking, BookingResource>(updatedBooking);

            //var updateBooking = await res.Content.ReadAsAsync<BookingResource>();
            return RedirectToAction("Index", new { id = updatedBooking.BookingId });
        }

        // TODO: Create ViewModel for Delete from Book Details to Book Compressed
        [Route("Book/{id}/Booking")]
        public async Task<IActionResult> Booking(long id)
        {
            var bookRes = await _client.GetAsync(baseurl + $"Books/{id}");

            if (!bookRes.IsSuccessStatusCode)
                return View("Error", new ErrorViewModel());

            // Deserialize the book from the response body.
            var book = await bookRes.Content.ReadAsAsync<BookDetailsResource>();
            
            return View(book);
        }

        // POST: Profile/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        public async Task<IActionResult> Add([Bind("Name")][FromForm] BookingResource genre)
        {
            throw new NotImplementedException();
        }

        // GET: Profile/Delete/5
        [Route("Profile/Delete/{id}")]
        public async Task<IActionResult> Delete([FromRoute]long id)
        {
            throw new NotImplementedException();
        }

        // POST: Profile/Delete/5
        public async Task<IActionResult> DeleteConfirmed()
        {
            throw new NotImplementedException();     
        }

        // GET: Profile/Delete/5
        /*[Route("{genreName}/Books")]
        public async Task<IActionResult> Books([FromRoute]string genreName, long genreId)
        {
            genreId = Convert.ToInt64(ViewData["BookingId"]);
            HttpResponseMessage res = await _client.GetAsync(baseurl + $"Profile/{genreId}");

            if (!res.IsSuccessStatusCode)
                return View("Error", new ErrorViewModel());

            var books = await res.Content.ReadAsAsync<BookResource>();

            return View("Views/Books/Index.cshtml", books);
        }*/

        /*private bool BookingExists(long id)
        {
            return _context.Profile.Any(e => e.BookingId == id);
        }*/
    }
}
