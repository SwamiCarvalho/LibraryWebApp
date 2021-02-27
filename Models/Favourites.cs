using LibraryAPI.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryWebApp.Models
{
    public class Favourites
    {
        public long Id { get; set; }

        [ForeignKey("ReaderId")]
        public long ReaderId { get; set; }
        [ForeignKey("BookId")]
        public long BookId { get; set; }
        public virtual Reader Reader { get; set; }
        public virtual Book Book { get; set; }
    }
}
