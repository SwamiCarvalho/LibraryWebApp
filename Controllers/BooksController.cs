using Microsoft.AspNetCore.Mvc;
using LibraryAPI.Models;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Net;
using LibraryWebApp.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using LibraryAPI.Resources;
using System;

namespace LibraryWebApp.Controllers
{
    public class BooksController : Controller
    {
        /*private readonly LibraryAppDBContext _context;

        public BooksController(LibraryAppDBContext context)
        {
            _context = context;
        }*/

        // HttpClient is intended to be instantiated once per application, rather than per-use.
        private readonly HttpClient _client = new HttpClient();

        const string baseurl = "https://localhost:44351/api/";

        public BooksController(HttpClient client)
        {
            _client = client; 
        }


        // GET: Books
        public async Task<IActionResult> Index([FromQuery] string? searchTerm, [FromQuery] string? genre, [FromQuery] string? author)
        {

            ViewData["searchTerm"] = searchTerm;
            ViewData["genreFilter"] = genre;
            ViewData["authorFilter"] = author;


            List<BookResource> books = new List<BookResource>();

            //Sending request to find web api REST service resource GetAllBooks using HttpClient  
            HttpResponseMessage Res = await _client.GetAsync(baseurl + "Books");

            //Checking the response is successful or not which is sent using HttpClient  
            if (Res.IsSuccessStatusCode)
            {
                //Storing the response details recieved from web api   
                books = await Res.Content.ReadAsAsync<List<BookResource>>();

            }
            //returning the books list to view controller
            return View(books);

        }

        // GET: Books/Details/5
        public async Task<IActionResult> Details([FromRoute] long id)
        {
            BookDetailsResource book = new BookDetailsResource();

            HttpResponseMessage res = await _client.GetAsync(baseurl + $"Books/{id}");

            if (res.IsSuccessStatusCode)
            {
                book = await res.Content.ReadAsAsync<BookDetailsResource>();
            }
            return View(book);
        }


        // POST: Books/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.


        public async Task<ActionResult> Create()
        {

            //////////////////////// GET GENRE NAMES TO POPULATE DROPDOWN LIST ///////////////////////////////////////
            List<GenreResource> genres = new List<GenreResource>();

            //Sending request to find web api REST service resource GetAllGenres using HttpClient  
            HttpResponseMessage genresRes = await _client.GetAsync(baseurl + "Genres");

            //Checking the response is successful or not which is sent using HttpClient  
            if (genresRes.IsSuccessStatusCode)
            {
                //Storing the response details received from web api   
                genres = await genresRes.Content.ReadAsAsync<List<GenreResource>>();
                ViewBag.GenresList = genres;
            }

            //////////////////////// GET AUTHOR NAMES TO POPULATE DROPDOWN LIST ///////////////////////////////////////
            List<AuthorResource> authors = new List<AuthorResource>();

            //Sending request to find web api REST service resource GetAllAuthors using HttpClient  
            HttpResponseMessage authorsRes = await _client.GetAsync(baseurl + "Authors");

            //Checking the response is successful or not which is sent using HttpClient  
            if (authorsRes.IsSuccessStatusCode)
            {
                //Storing the response details received from web api   
                authors = await authorsRes.Content.ReadAsAsync<List<AuthorResource>>();
                ViewBag.AuthorsList = authors;
            }
            return View();
        }


        public async Task<ActionResult> Add([Bind("BookId,Title,OgTitle,PublicationYear,Edition,Notes,PhysicalDescription")] Book book)
        {
            Book newBook = new Book();

            HttpResponseMessage res = await _client.PostAsJsonAsync(baseurl + "Books", book);

            res.EnsureSuccessStatusCode();

            if (res.IsSuccessStatusCode)
            {

                newBook = await res.Content.ReadAsAsync<Book>();
                return RedirectToAction(nameof(Details), new { id = newBook.BookId });
                //return CreatedAtRoute("DefaultApi", new { id = book.Id }, dto);
            }

            ViewData["Feedback"] = "Sorry, book wasn't Created";
            return RedirectToAction("Error");
        }


        public async Task<ActionResult> Edit([FromRoute] long id)
        {
            BookDetailsResource book = new BookDetailsResource();

            HttpResponseMessage res = await _client.GetAsync(baseurl + $"Books/{id}");

            res.EnsureSuccessStatusCode();

            if (res.IsSuccessStatusCode)
            {
                // Deserialize the updated product from the response body.
                book = await res.Content.ReadAsAsync<BookDetailsResource>();
                return View(book);
            }
            return View(new ErrorViewModel());
        }


        // Put Updated Book
        public async Task<ActionResult> Update([Bind("BookBookId,Title,OgTitle,PublicationYear,Edition,Notes,PhysicalDescription")] Book book)
        {

            HttpResponseMessage res = await _client.PutAsJsonAsync(baseurl + $"Books/{book.BookId}", book);

            if (res.IsSuccessStatusCode)
            {
                return RedirectToAction("Details", new { id = book.BookId });
            }


            // Deserialize the updated product from the response body.
            //book = await res.Content.ReadAsAsync<Book>();
            return View(nameof(Edit), book.BookId);
            
        }
    

        // GET: Books/Delete/5
        // TODO: Create ViewModel for Delete from Book Details to Book Compressed

        public async Task<ActionResult> Delete(long id)
        {
            BookDetailsResource book = new BookDetailsResource();

            HttpResponseMessage bookRes = new HttpResponseMessage();

            bookRes = await _client.GetAsync(baseurl + $"Books/{id}");

            if (bookRes.IsSuccessStatusCode)
            {
                // Deserialize the updated product from the response body.
                book = await bookRes.Content.ReadAsAsync<BookDetailsResource>();
                return View(book);
            }
            return View(new ErrorViewModel());
        }

        // POST: Books/Delete/5
        public async Task<ActionResult> DeleteConfirmed(long id)
        {
            await _client.DeleteAsync(baseurl + $"Books/{id}");
            return RedirectToAction("Index");
        }

    }
}
