using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using LibraryAPI.Resources;
using System;
using Microsoft.AspNetCore.Authorization;
using LibraryWebApp.Domain.Models.ViewModels;

namespace LibraryWebApp.Controllers
{
    public class GenresController : Controller
    {

        // HttpClient is intended to be instantiated once per application, rather than per-use.
        private readonly HttpClient _client = new HttpClient();

        const string baseurl = "https://localhost:44351/api/";

        public GenresController(HttpClient client)
        {
            _client = client;
        }

        // GET: Genres
        public async Task<IActionResult> Index()
        {
            ViewData["Title"] = "Genres";

            //Sending request to find web api REST service resource GetAllGenres using HttpClient  
            HttpResponseMessage res = await _client.GetAsync(baseurl + "Genres");

            //Checking the response is successful or not which is sent using HttpClient  
            if (!res.IsSuccessStatusCode)
                View("Error", new ErrorViewModel());

            //Storing the response details recieved from web api   
            var genres = await res.Content.ReadAsAsync<List<GenreResource>>();

            //returning the genres list to view controller
            return View(genres);

        }

        // GET: Genres/Details/5
        public async Task<IActionResult> Details([FromRoute]long id)
        {
            HttpResponseMessage res = await _client.GetAsync(baseurl + $"Genres/{id}");

            if (!res.IsSuccessStatusCode)
                return View("Error", new ErrorViewModel());
            
            var genre = await res.Content.ReadAsAsync<GenreResource>();

            return View(genre);
        }

        // GET: Genres/Edit/5
        [Authorize(Roles = "Librarian,Administrator")]
        public async Task<IActionResult> Edit([FromRoute] long id)
        {
            HttpResponseMessage res = await _client.GetAsync(baseurl + $"Genres/{id}");

            if (!res.IsSuccessStatusCode)
                return View("Error", new ErrorViewModel());

            // Deserialize the updated product from the response body.
            var genre = await res.Content.ReadAsAsync<GenreResource>();
            return View(genre);

        }

        // POST: Genres/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Librarian,Administrator")]
        public async Task<IActionResult> Update([Bind("GenreId,Name")] GenreResource genre)
        {
            HttpResponseMessage res = await _client.PutAsJsonAsync(baseurl + $"Genres/{genre.GenreId}", genre);

            if (!res.IsSuccessStatusCode)
                return RedirectToAction(nameof(Edit), genre.GenreId);

            //var updateGenre = await res.Content.ReadAsAsync<GenreResource>();
            return RedirectToAction("Details", new { id = genre.GenreId });
        }

        // GET: Genres/Create
        [Authorize(Roles = "Librarian,Administrator")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Genres/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Librarian,Administrator")]
        public async Task<IActionResult> Add([Bind("Name")][FromForm] SaveGenreResource genre)
        {
            HttpResponseMessage res = await _client.PostAsJsonAsync(baseurl + "Genres", genre);

            if (!res.IsSuccessStatusCode)
            {
                ViewData["Feedback"] = "Sorry, genre wasn't Created";
                return View("Error", new ErrorViewModel());
                // return RedirectToAction("Error");
            }
                
            ViewBag.Feedback = "GenreResource Created Successfully";
            var newGenre = await res.Content.ReadAsAsync<GenreResource>();
            return RedirectToAction("Details", new { id = newGenre.GenreId });
        }

        // GET: Genres/Delete/5
        [Route("Genres/Delete/{id}")]
        [Authorize(Roles = "Librarian,Administrator")]
        public async Task<IActionResult> Delete([FromRoute]long id)
        {
            HttpResponseMessage genreRes = await _client.GetAsync(baseurl + $"Genres/{id}");

            if (!genreRes.IsSuccessStatusCode)
                return View("Error", new ErrorViewModel());

            var genre = await genreRes.Content.ReadAsAsync<GenreResource>();
            return View(genre);

        }

        // POST: Genres/Delete/5
        [Authorize(Roles = "Librarian,Administrator")]
        public async Task<IActionResult> DeleteConfirmed()
        {
            var genreIdString = Request.Form["GenreId"];
            var id = Convert.ToInt64(genreIdString);

            HttpResponseMessage genreRes = await _client.DeleteAsync(baseurl + $"Genres/{id}");
            
            if (!genreRes.IsSuccessStatusCode)
            {
                ViewData["Feedback"] = "Sorry, genre wasn't Deleted";
                return View("Error", new ErrorViewModel());
            }

            var genre = await genreRes.Content.ReadAsAsync<GenreResource>();
            //TempData["message"] = "The Genre was deleted successfully.";
            //TempData["object"] = genre;
            return RedirectToAction(nameof(Index));      
        }

        // GET: Genres/Delete/5
        /*[Route("{genreName}/Books")]
        public async Task<IActionResult> Books([FromRoute]string genreName, long genreId)
        {
            genreId = Convert.ToInt64(ViewData["GenreId"]);
            HttpResponseMessage res = await _client.GetAsync(baseurl + $"Genres/{genreId}");

            if (!res.IsSuccessStatusCode)
                return View("Error", new ErrorViewModel());

            var books = await res.Content.ReadAsAsync<BookResource>();

            return View("Views/Books/Index.cshtml", books);
        }*/

        /*private bool GenreExists(long id)
        {
            return _context.Genres.Any(e => e.GenreId == id);
        }*/
    }
}
