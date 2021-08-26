using LibraryAPI.Domain.Models;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace LibraryWebApp.Domain.Models
{
    public class Booking
    {
        [Key]
        public long BookingId { get; set; }
        [DisplayName("Booking Date")]
        public DateTime StartDate { get; set; } = DateTime.Today;
        [DisplayName("Delivery Deadline")]
        public DateTime EndDate 
        {
            get 
            {
                return StartDate.AddDays(24);
            }
            set
            {
                DateTime datetime = DateTime.Today;
                datetime.AddDays(24);
            }
        }

        // Devolvido / Em Posse 
        public string Status { get; set; }
        public virtual Reader Reader { get; set; }
        public virtual Book Book { get; set; }
    }
}
