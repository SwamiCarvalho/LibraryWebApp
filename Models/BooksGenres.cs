using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryWebApp.Models
{
    public class BooksGenres
    {

        [ForeignKey("BookId")]
        public long BookId { get; set; }
        [ForeignKey("GenreId")]
        public long GenreId { get; set; }
        public Book Book { get; set; }
        public Genre Genre { get; set; }
    }
}
