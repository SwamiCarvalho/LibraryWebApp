using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System;
using LibraryWebApp.Domain.Models;
using LibraryWebApp.Resources;
using AutoMapper;
using LibraryWebApp.Services;
using LibraryAPI.Resources;

namespace LibraryWebApp.Controllers
{
    public class BookingsController : Controller
    {
        private readonly IBookingService _bookingService;
        private readonly IMapper _mapper;

        public BookingsController(IBookingService bookingService, IMapper mapper)
        {
            _bookingService = bookingService;
        }

        // GET: Profile
        [Route("Bookings")]
        public async Task<IActionResult> Index()
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

            //returning the bookings list to view controller
            return View(result.Bookings);
        }

        // GET: Profile/Edit/5
        public async Task<IActionResult> Renew([FromRoute]long id)
        {
            var result = await _bookingService.GetBookingByIdAsync(id);

            // Verify if it was possible to retrieve values.
            if (!result.Success)
            {
                ViewData["Feedback"] = result.Message;
                return View("Error", new ErrorViewModel());
            }

            // Check if it has been delivered.
            if (result.Booking.Status == "Delivered")
            {
                ViewData["FeedBack"] = "It was not possible to renew book, due to, its no longer in your possesion.";
                return View("Error", new ErrorViewModel());
            }

            // Set Date that the book was booked, the delivery deadline date and status.
            DateTime startDate = DateTime.Now;
            DateTime endDate = startDate.AddDays(24);
            result.Booking.EndDate = endDate;
            result.Booking.Status = "Renewed";
  
            return View(result.Booking);
        }

        public async Task<IActionResult> Deliver([FromRoute] long id)
        {
            var result = await _bookingService.GetBookingByIdAsync(id);

            // Verify if it was possible to retrieve values.
            if (!result.Success)
            {
                ViewData["Feedback"] = result.Message;
                return View("Error", new ErrorViewModel());
            }

            // Check if it has been already delivered.
            if (result.Booking.Status == "Delivered")
            {
                ViewData["FeedBack"] = "It was not possible to deliver book, due to, its no longer in your possesion.";
                return View("Error", new ErrorViewModel());
            }

            // Set Status and Delivery Date.
            DateTime? deliveryDate = DateTime.Now;
            result.Booking.Status = "Delivered";
            result.Booking.DeliveryDate = deliveryDate;

            return View(result.Booking);
        }

        // POST: Profile/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        public async Task<IActionResult> Update([Bind("BookingId,StartDate,EndDate,Status,DeliveryDate")] UpdateBookingResource bookingResource)
        {
            var result = await _bookingService.UpdateBookingAsync(bookingResource.BookingId, bookingResource);

            if (!result.Success)
            {
                ViewData["Feedback"] = result.Message;
                return View("Error", new ErrorViewModel());
            }

            //var updateBooking = await res.Content.ReadAsAsync<BookingResource>();
            return RedirectToAction("Index");
        }

        // TODO: Create ViewModel for Delete from Book Details to Book Compressed
        [Route("Book/{id}/Booking")]
        public IActionResult Booking(long id, [FromForm]BookDetailsResource book)
        {

            // TODO: Return also estimated delivery
            return View(book);
        }

        // POST: Profile/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        public async Task<IActionResult> Add([FromForm] BookDetailsResource book)
        {
            CreateBookingResource newBooking = new CreateBookingResource();
            newBooking.BookId = book.BookId;
            newBooking.Status = "Booked";
            newBooking.DeliveryDate = null;

            var result = await _bookingService.SaveBookingAsync(newBooking);

            if (!result.Success)
            {
                ViewData["Feedback"] = result.Message;
                return View("Error", new ErrorViewModel());
            }

            return RedirectToAction(nameof(Index));
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
