using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryWebApp.Models
{
    public class Reader
    {
        public long Id { get; set; }
        [Required]
        [DisplayName("Citzen Card Number")]
        public string NumberCC { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        [DisplayName("Full Name")]
        public string FullName { get; set; }

    }
}