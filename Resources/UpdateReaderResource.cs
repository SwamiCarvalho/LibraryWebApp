using System.ComponentModel;

namespace LibraryWebApp.Resources
{
    public class UpdateReaderResource
    {
        public string Address { get; set; }
        [DisplayName("Full Name")]
        public string FullName { get; set; }
        public string Email { get; set; }
    }
}
