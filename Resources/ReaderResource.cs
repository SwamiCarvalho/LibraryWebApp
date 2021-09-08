using System.ComponentModel;

namespace LibraryWebApp.Resources
{
    public class ReaderResource
    {
        public long ReaderId { get; set; }
        [DisplayName("Citzen Card Number")]
        public long CCNumber { get; set; }
        [DisplayName("Delivery Deadline")]
        public string Address { get; set; }
        [DisplayName("Full Name")]
        public string FullName { get; set; }
        public string Email { get; set; }
    }
}
