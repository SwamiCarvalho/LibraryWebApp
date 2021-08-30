using LibraryWebApp.Domain.Models;
using LibraryWebApp.Domain.Repositories;
using LibraryWebApp.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LibraryWebApp.Persistence.Repositories
{
    public class BookingRepository : RepositoryBase<Booking>, IBookingRepository
    {

        public BookingRepository(AppDbContext context) : base(context) { }

        public async Task<IEnumerable<Booking>> ListAsync()
        {
            return await FindAll().ToListAsync();
        }

        public async Task<IEnumerable<Booking>> ListUserBookingsAsync(long id)
        {
            return await FindByCondition(b => b.Reader.ReaderId == id).ToListAsync();
        }

        public void AddBooking(Booking booking)
        {
            Create(booking);
        }
        public void UpdateBooking(Booking booking)
        {
            Update(booking);
        }
        public void DeleteBooking(Booking booking)
        {
            Delete(booking);
        }
        public async Task<Booking> GetBookingByIdAsync(long id)
        {
            return await FindByCondition(b => b.BookingId == id).FirstOrDefaultAsync();
        }

        public async Task<Booking> GetBookingByBookIdAsync(long id)
        {
            return await FindByCondition(b => b.BookId == id && b.Status != "Delivered").FirstOrDefaultAsync();
        }
    }
}