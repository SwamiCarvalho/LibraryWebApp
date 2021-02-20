using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LibraryWebApp.Data;
using LibraryWebApp.Models;
using LibraryAPI.Models;
using System.Net.Http;
using LibraryAPI.Models.DTOs;
using System.Net.Http.Headers;
using System.Net;

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
            List<GenreDTO> genres = new List<GenreDTO>();
            genres = null;

            //Sending request to find web api REST service resource GetAllGenres using HttpClient  
            HttpResponseMessage Res = await _client.GetAsync(baseurl + "Genres");

            //Checking the response is successful or not which is sent using HttpClient  
            if (Res.IsSuccessStatusCode)
            {
                //Storing the response details recieved from web api   
                genres = await Res.Content.ReadAsAsync<List<GenreDTO>>();

            }
            //returning the genres list to view controller
            return View(genres);
        }

        // GET: Genres/Details/5
        public async Task<IActionResult> Details([FromRoute]long id)
        {
            Genre genre = new Genre();

            HttpResponseMessage res = await _client.GetAsync(baseurl + $"Genres/{id}");

            if (res.IsSuccessStatusCode)
            {
                 genre = await res.Content.ReadAsAsync<Genre>();
            }
            return View(genre);
        }

        // GET: Genres/Create
        public IActionResult Create()
        {
            return View();
        }

/*        // POST: Genres/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        public async Task<Uri> Create([Bind("Id,Name")] Genre genre)
        {
            HttpResponseMessage res = await _client.PostAsJsonAsync(baseurl + "Genres", genre);

            res.EnsureSuccessStatusCode();

            return res.Headers.Location;
        }*/

        // GET: Genres/Edit/5
        public async Task<IActionResult> Edit([FromRoute]long id)
        {
            Genre genre = new Genre();

            HttpResponseMessage res = await _client.GetAsync(baseurl + $"Genres/{id}");

            res.EnsureSuccessStatusCode();

            if (res.IsSuccessStatusCode)
            {
                // Deserialize the updated product from the response body.
                genre = await res.Content.ReadAsAsync<Genre>();
            }
            return View(genre);
        }


        // POST: Genres/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        public async Task<IActionResult> Update([FromRoute]long id, [Bind("Id,Name")] Genre genre)
        {
            HttpResponseMessage res = await _client.PutAsJsonAsync(baseurl + $"Genres/{genre.Id}", genre);

            res.EnsureSuccessStatusCode();

            // Deserialize the updated product from the response body.
            var updateGenre = await res.Content.ReadAsAsync<Genre>();

            return CreatedAtAction("Details", updateGenre.Id);
        }

        // GET: Genres/Delete/5
        public async Task<HttpStatusCode> Delete([FromRoute]long id)
        {
            HttpResponseMessage res = await _client.DeleteAsync(baseurl + $"Genres/{id}");

            return res.StatusCode;
        }

        /*// POST: Genres/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var genre = await _context.Genres.FindAsync(id);
            _context.Genres.Remove(genre);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GenreExists(long id)
        {
            return _context.Genres.Any(e => e.Id == id);
        }*/
    }
}
