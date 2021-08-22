using LibraryAPI.Domain.Models;
using LibraryAPI.Resources;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.WebPages.Html;

namespace LibraryWebApp.Controllers
{
    public class BooksController : Controller
    {
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

            //Sending request to find web api REST service resource GetAllBooks using HttpClient  
            HttpResponseMessage res = await _client.GetAsync(baseurl + "Books");

            //Checking the response is successful or not which is sent using HttpClient  
            if (!res.IsSuccessStatusCode)
                return View("Error", new ErrorViewModel());

            //Storing the response details received from web api   
            var books = await res.Content.ReadAsAsync<List<BookResource>>();

            //returning the books list to view controller
            return View(books);

        }

        // GET: Books/Details/5
        public async Task<IActionResult> Details([FromRoute]long id)
        {
            HttpResponseMessage res = await _client.GetAsync(baseurl + $"Books/{id}");

            if (!res.IsSuccessStatusCode)
                return View("Error", new ErrorViewModel());

            var book = await res.Content.ReadAsAsync<BookDetailsResource>();
            return View(book);

        }

        public async Task<IActionResult> Edit([FromRoute] long id)
        {
            HttpResponseMessage res = await _client.GetAsync(baseurl + $"Books/{id}");

            if (!res.IsSuccessStatusCode)
                return View("Error", new ErrorViewModel());

            // Deserialize the updated product from the response body.
            var book = await res.Content.ReadAsAsync<BookDetailsResource>();
            return View(book);
        }

        public async Task<IActionResult> Update([Bind("BookId,Title,OgTitle,PublicationYear,Edition,Notes,PhysicalDescription")] BookDetailsResource book)
        {
            HttpResponseMessage res = await _client.PutAsJsonAsync(baseurl + $"Books/{book.BookId}", book);

            if (!res.IsSuccessStatusCode)
                return RedirectToAction(nameof(Edit), book.BookId);

            return RedirectToAction("Details", new { id = book.BookId });  
        }

        // POST: Books/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        public async Task<IActionResult> Create()
        {

            //////////////////////// GET GENRE NAMES TO POPULATE DROPDOWN LIST ///////////////////////////////////////

            //Sending request to find web api REST service resource GetAllGenres using HttpClient  
            HttpResponseMessage genresRes = await _client.GetAsync(baseurl + "Genres");

            //Checking the response is successful or not which is sent using HttpClient  
            if (genresRes.IsSuccessStatusCode)
            {
                //Storing the response details received from web api   
                var genres = await genresRes.Content.ReadAsAsync<List<GenreResource>>();

                /*IList<SelectListItem> genresList = new List<SelectListItem>();
                foreach (var genre in genres)
                {
                    var genreListItem = new SelectListItem { Text = genre.Name, Value = genre.GenreId.ToString() };
                    genresList.Add(genreListItem);
                }*/

                ViewBag.GenresList = genres;
            }

            //////////////////////// GET AUTHOR NAMES TO POPULATE DROPDOWN LIST ///////////////////////////////////////

            //Sending request to find web api REST service resource GetAllAuthors using HttpClient  
            HttpResponseMessage authorsRes = await _client.GetAsync(baseurl + "Authors");

            //Checking the response is successful or not which is sent using HttpClient  
            if (authorsRes.IsSuccessStatusCode)
            {
                //Storing the response details received from web api   
                var authors = await authorsRes.Content.ReadAsAsync<List<AuthorResource>>();

                /*IList<SelectListItem> authorsList = new List<SelectListItem>();
                foreach (var author in authors)
                {
                    var genreListItem = new SelectListItem { Text = author.FullName, Value = author.AuthorId.ToString() };
                    authorsList.Add(genreListItem);
                }*/

                ViewBag.AuthorsList = authors;
            }
            return View();
        }


        public async Task<IActionResult> Add([Bind("Title,OgTitle,PublicationYear,Edition,Notes,PhysicalDescription")][FromForm] SaveBookResource book)
        {
            /*var genres = Request.Form["Genres"];
            foreach (var genreIdString in genres)
            {
                var genreId = Convert.ToInt64(genreIdString);
                HttpResponseMessage genreRes = await _client.GetAsync(baseurl + $"Genres/{genreId}");

                if (!genreRes.IsSuccessStatusCode)
                    return View("Error", new ErrorViewModel());

                var genre = await genreRes.Content.ReadAsAsync<GenreResource>();
                book.Genres.Add(genre);
            }
            

            var authors = Request.Form["Authors"];
            foreach (var authorIdString in authors)
            {
                var authorId = Convert.ToInt64(authorIdString);
                HttpResponseMessage authorRes = await _client.GetAsync(baseurl + $"Authors/{authorId}");

                if (!authorRes.IsSuccessStatusCode)
                    return View("Error", new ErrorViewModel());

                var author = await authorRes.Content.ReadAsAsync<AuthorResource>();
                book.Authors.Add(author);
            }*/

            HttpResponseMessage res = await _client.PostAsJsonAsync(baseurl + "Books", book);

            if (!res.IsSuccessStatusCode)
            {
                ViewData["Feedback"] = "Sorry, book wasn't Created";
                return View("Error", new ErrorViewModel());
            }

            var newBook = await res.Content.ReadAsAsync<BookResource>();
            return RedirectToAction(nameof(Details), new { id = newBook.BookId });
            //return CreatedAtRoute("DefaultApi", new { id = book.Id }, dto);
        }

        // GET: Books/Delete/5
        // TODO: Create ViewModel for Delete from Book Details to Book Compressed
        [Route("Books/Delete/{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            var bookRes = await _client.GetAsync(baseurl + $"Books/{id}");

            if (!bookRes.IsSuccessStatusCode)
                return View("Error", new ErrorViewModel());

            // Deserialize the updated product from the response body.
            var book = await bookRes.Content.ReadAsAsync<BookDetailsResource>();
            return View(book);
        }

        // POST: Books/Delete/5
        public async Task<IActionResult> DeleteConfirmed()
        {
            var bookIdString = Request.Form["BookId"];
            var id = Convert.ToInt64(bookIdString);

            var res = await _client.DeleteAsync(baseurl + $"Books/{id}");

            if (!res.IsSuccessStatusCode)
            {
                ViewData["Feedback"] = "Sorry, book wasn't Deleted";
                return View("Error", new ErrorViewModel());
            }
            
            return RedirectToAction(nameof(Index));

        }

    }
}
