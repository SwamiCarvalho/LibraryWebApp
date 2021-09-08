using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LibraryWebApp.Domain.Models
{
    public class Reader
    {
        [Key]
        public long ReaderId { get; set; }
        public string UserId { get; set; }
        public long CCNumber { get; set; }
        public string Address { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public virtual ICollection<Favourite> Favourites { get; set; }
        public virtual ICollection<Booking> Bookings { get; set; }

    }

    public class Librarian
    {
        [Key]
        public long LibrarianId { get; set; }
        public string UserId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }

    }
}