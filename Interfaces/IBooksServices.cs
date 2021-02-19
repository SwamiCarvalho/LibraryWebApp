using LibraryAPI.Models;
using LibraryAPI.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace LibraryWebApp.Interfaces
{
    public interface IBooksServices
    {
        Task<List<BookDTO>> GetBooksAsync();
        Task<Book> GetBookByIdAsync(long id);
        Task<Uri> CreateBookAsync(Book book);
        Task<Book> UpdateBookAsync(Book book);
        Task<HttpStatusCode> DeleteBookAsync(long id);
        //Task<HttpStatusCode> DeleteBookConfirmed(long id);
    }
}
