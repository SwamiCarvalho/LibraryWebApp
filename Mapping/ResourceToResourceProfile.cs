using AutoMapper;
using LibraryAPI.Resources;
using LibraryWebApp.Resources;

namespace LibraryWebApp.Mapping
{
    public class ResourceToResourceProfile : Profile
    {
        public ResourceToResourceProfile()
        {
            CreateMap<BookDetailsResource, BookResource>();
            CreateMap<BookDetailsResource, BookTitleResource>();
        }
    }
}
