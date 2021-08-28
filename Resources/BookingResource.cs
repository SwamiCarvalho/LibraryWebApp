using System;
using System.ComponentModel;

namespace LibraryWebApp.Resources
{
    public class BookingResource
    {
        public long BookingId { get; set; }
        [DisplayName("Booking Date")]
        public DateTime StartDate { get; set; }
        [DisplayName("Delivery Deadline")]
        public DateTime EndDate { get; set; }
        public string Status { get; set; }
        public long BookId { get; set; }
        [DisplayName("Delivery Date")]
        public DateTime DeliveryDate { get; set; }
        // public virtual BookTitleResource Book { get; set; }
    }
}
