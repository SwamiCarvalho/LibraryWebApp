using LibraryAPI.Domain.Models;

namespace LibraryWebApp.Domain.Models
{
    public class Favourite
    {
        public long FavouriteId { get; set; }
        public Reader Reader { get; set; }
        public virtual Book Book { get; set; }
    }
}
