using AutoMapper;
using LibraryAPI.Domain.Models;
using LibraryAPI.Resources;
using LibraryWebApp.Domain.Models;
using LibraryWebApp.Resources;
using System.Linq;

namespace LibraryWebApp.Mapping
{
    public class ModelToResourceProfile : Profile
    {
        public ModelToResourceProfile()
        {
            /*CreateMap<Book, BookResource>()
                .ForMember(dto => dto.Genres, opt => opt.MapFrom(x => x.Genres.ToList()))
                .ForMember(dto => dto.Authors, opt => opt.MapFrom(x => x.Authors.ToList()));*/

            CreateMap<Booking, BookingResource>();
            CreateMap<Reader, ReaderResource>();
            CreateMap<Librarian, LibrarianResource>();
            
        }
    }
}
