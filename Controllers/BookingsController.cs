using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System;
using LibraryWebApp.Domain.Models;
using LibraryWebApp.Resources;
using LibraryWebApp.Services;
using LibraryAPI.Resources;
using LibraryWebApp.Domain.Services.Communication;

namespace LibraryWebApp.Controllers
{
    public class BookingsController : Controller
    {
        private readonly IBookingService _bookingService;
        public readonly IGeneralService _generalService;

        public BookingsController(IBookingService bookingService, IGeneralService generalService)
        {
            _bookingService = bookingService;
            _generalService = generalService;
        }

        // GET: All Bookings (For User Librarian)
        /*[Route("Bookings")]
        public async Task<IActionResult> Index()
        {
            //TODO: depending on the user 
            ViewData["Title"] = "My Bookings";

            var result = await _bookingService.GetAllBookingsAsync();
            //var user = await _generalService.GetReader(this.User);
            // Get all Bookings
            //var result = await _bookingService.GetUserBookingsAsync(user.Reader.ReaderId);
            //Check if it retrieved anything
            if (!result.Success || result.Bookings == null)
            {
                ViewData["Feedback"] = result.Message;
                View("Error", new ErrorViewModel());
            }

            //returning the bookings list to view controller
            return View(result.Bookings);
        }*/

        // GET: User Bookings (For User Reader)
        [Route("Bookings")]
        public async Task<IActionResult> Index()
        {

            BookingResponse result;
            var authenticated = this.User.Identity.IsAuthenticated;

            if (!authenticated)
            {
                ViewData["Feedback"] = "You should not have access to this! :o";
                return View("Error", new ErrorViewModel());
            }

            if (this.User.IsInRole("Reader"))
            {
                ViewData["Title"] = "My Bookings";
                // Run this segment if user is authenticated.
                var user = await _generalService.GetReader(this.User);
                result = await _bookingService.GetUserBookingsAsync(user.Reader.ReaderId);
            }
            else
            {
                ViewData["Title"] = "All Bookings From Users";
                result = await _bookingService.GetAllBookingsAsync();
            }
            

            if (!result.Success)
            {
                ViewData["Feedback"] = result.Message;
                View("Error", new ErrorViewModel());
            }

            if(result == null)
                ViewData["Feedback"] = result.Message;
            return View(result.Bookings);
            
        }

        // GET: Bookings/Edit/5
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

        // POST: Bookings/Edit/5
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
        public async Task<IActionResult> Booking([FromForm]BookDetailsResource book)
        {
            
            var result = await _bookingService.GetBookAvailability(book.BookId);

            if (!result.Success)
            {
                ViewData["Feedback"] = result.Message;
                return View("Error", new ErrorViewModel());
            }

            // TODO: Return also estimated delivery
            return View(book);
        }

        // POST: Bookings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        public async Task<IActionResult> Add([FromForm] BookDetailsResource book)
        {
            CreateBookingResource newBooking = new CreateBookingResource();
            newBooking.BookId = book.BookId;
            newBooking.Status = "Booked";
            newBooking.DeliveryDate = null;

            var reader = await _generalService.GetReader(this.User);

            if(!reader.Success)
            {
                ViewData["Feedback"] = reader.Message;
                return View("Error", new ErrorViewModel());
            }

            newBooking.ReaderId = reader.Reader.ReaderId;

            var result = await _bookingService.SaveBookingAsync(newBooking);

            if (!result.Success)
            {
                ViewData["Feedback"] = result.Message;
                return View("Error", new ErrorViewModel());
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
