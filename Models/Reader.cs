using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace LibraryWebApp.Models
{
    public class Reader
    {
        public long ReaderId { get; set; }
        [Required]
        [DisplayName("Citzen Card Number")]
        public string NumberCC { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        [DisplayName("Full Name")]
        public string FullName { get; set; }
        public IList<Favourite> Favourites { get; set; }
        public IList<Requisition> Requisitions { get; set; }

    }
}