using AutoMapper;
using LibraryWebApp.Domain.Models;
using LibraryWebApp.Resources;

namespace LibraryWebApp.Mapping
{
    public class ResourceToModelProfile : Profile
    {
        public ResourceToModelProfile()
        {
            CreateMap<BookingResource, Booking>();
        }
    }
}
