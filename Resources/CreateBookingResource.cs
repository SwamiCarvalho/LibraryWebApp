using System;
using System.ComponentModel;

namespace LibraryWebApp.Resources
{
    public class CreateBookingResource
    {
        [DisplayName("Booking Date")]
        public DateTime StartDate { get; set; } = DateTime.Now;
        [DisplayName("Delivery Deadline")]
        public DateTime EndDate
        {
            get
            {
                return StartDate.AddDays(24);
            }
            set
            {
                DateTime datetime = DateTime.Now;
                datetime.AddDays(24);
            }
        }
        public string Status { get; set; }
        public long BookId { get; set; }
        public long ReaderId { get; set; }
        public DateTime? DeliveryDate { get; set; }
    }
}