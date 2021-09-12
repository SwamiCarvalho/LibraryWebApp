using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace LibraryWebApp.Resources
{
    public class BookingResource
    {
        [DisplayName("Booking ID")]
        public long BookingId { get; set; }
        [DisplayName("Booking Date")]
        public DateTime StartDate { get; set; }
        [DisplayName("Delivery Deadline")]
        public DateTime EndDate { get; set; }
        public string Status { get; set; }
        public long BookId { get; set; }
        public long ReaderId { get; set; }

        [DisplayName("Delivery Date")]
        [DisplayFormat(NullDisplayText = "Not Delivered")]
        public DateTime? DeliveryDate { get; set; }
        // public virtual BookTitleResource Book { get; set; }
    }
}
