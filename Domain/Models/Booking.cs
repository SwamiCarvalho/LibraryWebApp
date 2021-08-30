using System;
using System.ComponentModel.DataAnnotations;

namespace LibraryWebApp.Domain.Models
{
    public class Booking
    {
        [Key]
        public long BookingId { get; set; }
        
        public DateTime StartDate { get; set; }
        
        public DateTime EndDate { get; set; }

        // Devolvido / Em Posse 
        public string Status { get; set; }
        public virtual Reader Reader { get; set; }
        public long BookId { get; set; }
        public DateTime? DeliveryDate { get; set; }
    }
}
