using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Net;
using LibraryAPI.Resources;
using LibraryWebApp.Models;

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
            List<GenreResource> genres = new List<GenreResource>();
            genres = null;

            //Sending request to find web api REST service resource GetAllGenres using HttpClient  
            HttpResponseMessage Res = await _client.GetAsync(baseurl + "Genres");

            //Checking the response is successful or not which is sent using HttpClient  
            if (Res.IsSuccessStatusCode)
            {
                //Storing the response details recieved from web api   
                genres = await Res.Content.ReadAsAsync<List<GenreResource>>();
            }
            //returning the genres list to view controller
            return View(genres);
        }

        // GET: Genres/Details/5
        public async Task<IActionResult> Details([FromRoute]long id)
        {
            GenreResource genre = new GenreResource();

            HttpResponseMessage res = await _client.GetAsync(baseurl + $"Genres/{id}");

            if (res.IsSuccessStatusCode)
            {
                 genre = await res.Content.ReadAsAsync<GenreResource>();
            }
            return View(genre);
        }

        // GET: Genres/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Genres/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        public async Task<IActionResult> Add([Bind("GenreId,Name")] GenreResource genre)
        {
            GenreResource updatedGenre = new GenreResource();

            HttpResponseMessage res = await _client.PostAsJsonAsync(baseurl + "Genres", genre);

            res.EnsureSuccessStatusCode();

            if (res.IsSuccessStatusCode)
            {
                ViewBag.Feedback = "GenreResource Created Successfully";
                updatedGenre = await res.Content.ReadAsAsync<GenreResource>();
                return RedirectToAction("Details", new { id = updatedGenre.GenreId });
            }

            ViewData["Feedback"] = "Sorry, genre wasn't Created";
            return RedirectToAction("Error");
        }

        // GET: Genres/Edit/5
        public async Task<IActionResult> Edit([FromRoute]long id)
        {
            GenreResource genre = new GenreResource();

            HttpResponseMessage res = await _client.GetAsync(baseurl + $"Genres/{id}");

            res.EnsureSuccessStatusCode();

            if (res.IsSuccessStatusCode)
            {
                // Deserialize the updated product from the response body.
                genre = await res.Content.ReadAsAsync<GenreResource>();
                return View(genre);
            }
            return View("Error");
        }


        // POST: Genres/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        public async Task<IActionResult> Update([Bind("GenreId,Name")] GenreResource genre)
        {
            //var json = JsonConvert.SerializeObject(genre);

            HttpResponseMessage res = await _client.PutAsJsonAsync(baseurl + $"Genres/{genre.GenreId}", genre);

            if (res.IsSuccessStatusCode)
            {
                //var updateGenre = await res.Content.ReadAsAsync<GenreResource>();
                return RedirectToAction("Details", new { id = genre.GenreId });
            }
            // Deserialize the updated genre from the response body.
            
            return View(nameof(Edit), genre.GenreId);
            
        }

        // GET: Genres/Delete/5
        [Route("Genres/Delete/{id}")]
        public async Task<IActionResult> Delete([FromRoute]long id)
        {
            GenreResource genre = new GenreResource();

            HttpResponseMessage genreRes = await _client.GetAsync(baseurl + $"Genres/{id}");

            genreRes.EnsureSuccessStatusCode();

            if (genreRes.IsSuccessStatusCode)
            {
                genre = await genreRes.Content.ReadAsAsync<GenreResource>();
                return View(genre);
            }
            return View("Error");
        }

        // POST: Genres/Delete/5
        [Route("Genres/{id}")]
        public async Task<IActionResult> DeleteConfirmed([FromRoute]long id)
        {
            GenreResource genre = new GenreResource();

            HttpResponseMessage genreRes = await _client.DeleteAsync(baseurl + $"Genres/{id}");

            if (genreRes.IsSuccessStatusCode)
            {
                genre = await genreRes.Content.ReadAsAsync<GenreResource>();
                TempData["message"] = "The Genre was deleted successfully.";
                TempData["object"] = genre;
                return RedirectToAction("Index");
            }
            else
            {
                ViewData["Feedback"] = genreRes.Content;
                TempData["message"] = genreRes.Content;
                return View("Error", new ErrorViewModel());
            }
            
            
        }

        /*private bool GenreExists(long id)
        {
            return _context.Genres.Any(e => e.GenreId == id);
        }*/
    }
}
