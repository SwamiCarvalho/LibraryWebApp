using LibraryAPI.Domain.Models;

namespace LibraryWebApp.Models
{
    public class Favourite
    {
        public long FavouriteId { get; set; }
        public Reader Reader { get; set; }
        public Book Book { get; set; }
    }
}
