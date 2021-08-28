﻿using System.Threading.Tasks;
using System;
using LibraryWebApp.Domain.Repositories;
using LibraryWebApp.Domain.Models;
using LibraryWebApp.Domain.Services.Communication;
using System.Collections.Generic;
using LibraryWebApp.Resources;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using System.Net.Http;
using LibraryAPI.Domain.Services.Communication;
using LibraryAPI.Resources;
using System.Linq;

namespace LibraryWebApp.Services
{
    public class BookingService : IBookingService
    {

        public IBookingRepository _bookingRepository;
        public readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        // HttpClient is intended to be instantiated once per application, rather than per-use.
        private readonly HttpClient _client = new HttpClient();

        const string baseurl = "https://localhost:44351/api/";

        public BookingService(IBookingRepository bookingRepository, IUnitOfWork unitOfWork, IMapper mapper, HttpClient client)
        {
            _bookingRepository = bookingRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _client = client;
        }
        public async Task<BookingResponse> GetAllBookingsAsync()
        {
            var bookings = await _bookingRepository.ListAsync();

            if (!bookings.Any())
                return new BookingResponse("You dont have any Bookings yet, \r\n feel free to book a book from us ;D");

            var bookingsResource = _mapper.Map<IEnumerable<Booking>, IEnumerable<BookingResource>>(bookings);

            return new BookingResponse(bookingsResource);
        }

        public async Task<BookingResponse> GetUserBookingsAsync(long id)
        {
            var userBookings = await _bookingRepository.ListUserBookingsAsync(id);

            if (userBookings == null)
                return new BookingResponse("There is no bookings.");

            var bookingsResource = _mapper.Map<IEnumerable<Booking>, IEnumerable<BookingResource>>(userBookings);

            return new BookingResponse(bookingsResource);
        }

        public async Task<BookingResponse> GetBookingByIdAsync(long id)
        {
            var booking = await _bookingRepository.GetBookingByIdAsync(id);

            if (booking == null)
                return new BookingResponse("Booking not found.");

            var bookingsResource = _mapper.Map<Booking, BookingResource>(booking);

            return new BookingResponse(bookingsResource);
        }

        public async Task<BookingResponse> SaveBookingAsync(CreateBookingResource bookingResource)
        {
            try
            {
                var booking = _mapper.Map<CreateBookingResource, Booking>(bookingResource);
                _bookingRepository.AddBooking(booking);
                await _unitOfWork.CompleteAsync();

                var newBookingResource = _mapper.Map<Booking, BookingResource>(booking);

                return new BookingResponse(newBookingResource);
            }
            catch (Exception ex)
            {
                return new BookingResponse($"An error occurred when saving the booking: {ex.Message}");
            }
        }

        public async Task<BookingResponse> UpdateBookingAsync(long id, UpdateBookingResource bookingResource)
        {
            var existingBooking = await _bookingRepository.GetBookingByIdAsync(id);

            if (existingBooking == null)
                return new BookingResponse("Booking not found.");


            existingBooking.StartDate = bookingResource.StartDate;
            existingBooking.EndDate = bookingResource.EndDate;
            existingBooking.Status = bookingResource.Status;

            try
            {
                _bookingRepository.UpdateBooking(existingBooking);
                await _unitOfWork.CompleteAsync();

                var updatedBooking = _mapper.Map<Booking, BookingResource>(existingBooking);

                return new BookingResponse(updatedBooking);
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

                var deletedBooking = _mapper.Map<Booking, BookingResource>(existingBooking);

                return new BookingResponse(deletedBooking);
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