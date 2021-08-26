using LibraryAPI.Resources;
using System;

namespace LibraryWebApp.Resources
{
    public class BookingResource
    {
        public long BookingId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Status { get; set; }
        public virtual BookResource Book { get; set; }
    }
}
