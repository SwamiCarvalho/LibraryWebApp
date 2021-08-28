using LibraryAPI.Domain.Models;

namespace LibraryWebApp.Domain.Models
{
    public class Favourite
    {
        public long FavouriteId { get; set; }
        public virtual Reader Reader { get; set; }
        public long BookId { get; set; }
    }
}
