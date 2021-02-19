using LibraryAPI.Models;
using LibraryAPI.Models.DTOs;
using LibraryWebApp.Interfaces;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace LibraryWebApp.Services
{
    public class BooksServices : IBooksServices
    {
        // HttpClient is intended to be instantiated once per application, rather than per-use.
        private readonly HttpClient _client = new HttpClient();

        const string baseurl = "https://localhost:44351/api/";


        public BooksServices(HttpClient client)
        {
            //Hosted web API REST Service base url  
            client.BaseAddress = new Uri(baseurl);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            _client = client;
        }


        // GET: Books
        public async Task<List<BookDTO>> GetBooksAsync()
        {
            List<BookDTO> books = new List<BookDTO>();
            books = null;

            //Sending request to find web api REST service resource GetAllBooks using HttpClient  
            HttpResponseMessage Res = await _client.GetAsync("Books");

            //Checking the response is successful or not which is sent using HttpClient  
            if (Res.IsSuccessStatusCode)
            {
                //Storing the response details recieved from web api   
                books = await Res.Content.ReadAsAsync<List<BookDTO>>();

            }
            //returning the books list to view controller
            return books;
            

        }

        // GET: Books/Details/5
        public async Task<Book> GetBookByIdAsync(long id)
        {
            Book book = new Book();

            HttpResponseMessage res = await _client.GetAsync($"Books/{id}");

            if (res.IsSuccessStatusCode)
            {
                var BookResponse = res.Content.ReadAsAsync<Book>();
            }
            return book;
        }


        // POST: Books/Create
        public async Task<Uri> CreateBookAsync(Book book)
        {
            HttpResponseMessage res = await _client.PostAsJsonAsync("Books", book);

            res.EnsureSuccessStatusCode();

            return res.Headers.Location;
        }

        // Put: Books/Edit/5
        public async Task<Book> UpdateBookAsync(Book book)
        {
            HttpResponseMessage res = await _client.PutAsJsonAsync($"Books/{book.Id}", book);

            res.EnsureSuccessStatusCode();

            // Deserialize the updated product from the response body.
            book = await res.Content.ReadAsAsync<Book>();

            return book;
            
        }


        // GET: Books/Delete/5
        public async Task<HttpStatusCode> DeleteBookAsync(long id)
        {
            HttpResponseMessage res = await _client.DeleteAsync($"Books/{id}");

            return res.StatusCode;
            
        }

        /*// GET: Books/Delete/5
        public async Task<HttpStatusCode> DeleteBookConfirmed(long id)
        {
            HttpResponseMessage res = await _client.DeleteAsync($"Books/{id}");

            return res.StatusCode;

        }*/
    }
}

