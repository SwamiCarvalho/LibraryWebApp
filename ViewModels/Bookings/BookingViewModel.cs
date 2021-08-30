using LibraryWebApp.Resources;
using Microsoft.AspNetCore.Mvc;

namespace LibraryWebApp.Domain.Models
{
    public class BookingViewModel
    {
        public virtual BookingResource booking { get; set; }
        public virtual BookTitleResource book { get; set; }
        
    }
}
