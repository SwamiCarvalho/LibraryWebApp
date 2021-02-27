using Microsoft.AspNetCore.Mvc;
using LibraryAPI.Models;
using System.Net.Http;
using LibraryAPI.Models.DTOs;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;
using System.Net;
using Newtonsoft.Json;
using System.Text;
using LibraryWebApp.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

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


            List<BookDTO> books = new List<BookDTO>();
            books = null;

            //Sending request to find web api REST service resource GetAllBooks using HttpClient  
            HttpResponseMessage Res = await _client.GetAsync(baseurl + "Books");

            //Checking the response is successful or not which is sent using HttpClient  
            if (Res.IsSuccessStatusCode)
            {
                //Storing the response details recieved from web api   
                books = await Res.Content.ReadAsAsync<List<BookDTO>>();

            }
            //returning the books list to view controller
            return View(books);

        }

        // GET: Books/Details/5
        public async Task<IActionResult> Details([FromRoute] long id)
        {
            BookDetailsDTO book = new BookDetailsDTO();

            HttpResponseMessage res = await _client.GetAsync(baseurl + $"Books/{id}");

            if (res.IsSuccessStatusCode)
            {
                book = await res.Content.ReadAsAsync<BookDetailsDTO>();
            }
            return View(book);
        }


        // POST: Books/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.


        public async Task<ActionResult> Create()
        {

            //////////////////////// GET GENRE NAMES TO POPULATE DROPDOWN LIST ///////////////////////////////////////
            List<GenreDTO> genres = new List<GenreDTO>();
            genres = null;

            //Sending request to find web api REST service resource GetAllGenres using HttpClient  
            HttpResponseMessage Res = await _client.GetAsync(baseurl + "Genres");

            //Checking the response is successful or not which is sent using HttpClient  
            if (Res.IsSuccessStatusCode)
            {
                //Storing the response details recieved from web api   
                genres = await Res.Content.ReadAsAsync<List<GenreDTO>>();

                List<SelectListItem> myGenres = new List<SelectListItem>();
                foreach(GenreDTO genre in genres)
                {
                    var item = new SelectListItem { Text = genre.Name, Value = genre.Id.ToString() };
                    myGenres.Add(item);
                }
                
                ViewBag.GenresList = myGenres;


            }
            return View();
        }


        public async Task<IActionResult> Add([Bind("Id,Title,OgTitle,PublicationYear,Edition,Notes,PhysicalDescription")] Book book)
        {
            Book newBook = new Book();

            HttpResponseMessage res = await _client.PostAsJsonAsync(baseurl + "Books", book);

            res.EnsureSuccessStatusCode();

            if (res.IsSuccessStatusCode)
            {

                newBook = await res.Content.ReadAsAsync<Book>();
                return RedirectToAction(nameof(Details), new { id = newBook.Id });
            }

            ViewData["Feedback"] = "Sorry, genre wasn't Created";
            return RedirectToAction("Error");
        }


        public async Task<IActionResult> Edit([FromRoute] long id)
        {
            Book book = new Book();

            HttpResponseMessage res = await _client.GetAsync(baseurl + $"Books/{id}");

            res.EnsureSuccessStatusCode();

            if (res.IsSuccessStatusCode)
            {
                // Deserialize the updated product from the response body.
                book = await res.Content.ReadAsAsync<Book>();
                return View(book);
            }
            return View(new ErrorViewModel());
        }


        // Put Updated Book
        public async Task<IActionResult> Update([Bind("Id,Title,OgTitle,PublicationYear,Edition,Notes,PhysicalDescription")] Book book)
        {

            HttpResponseMessage res = await _client.PutAsJsonAsync(baseurl + $"Books/{book.Id}", book);

            if (res.IsSuccessStatusCode)
            {
                return RedirectToAction("Details", new { id = book.Id });
            }


            // Deserialize the updated product from the response body.
            //book = await res.Content.ReadAsAsync<Book>();
            return View(nameof(Edit), book.Id);
            
        }
    

        // GET: Books/Delete/5
        public async Task<HttpStatusCode> Delete([FromRoute]long id)
        {
            HttpResponseMessage res = await _client.DeleteAsync(baseurl + $"Books/{id}");

            return res.StatusCode;
        }

        /*// POST: Books/Delete/5
        public IActionResult DeleteConfirmed([FromRoute] long id)
        {
            var deletedBook = _services.DeleteBookConfirmed(id);
            return RedirectToAction(nameof(Index));
        }*/

    }
}
