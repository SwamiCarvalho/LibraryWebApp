using LibraryAPI.Resources;
using LibraryWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace LibraryWebApp.Controllers
{
    public class AuthorsController : Controller
    {
        // HttpClient is intended to be instantiated once per application, rather than per-use.
        private readonly HttpClient _client = new HttpClient();

        const string baseurl = "https://localhost:44351/api/";

        public AuthorsController(HttpClient client)
        {
            _client = client;
        }

        // GET: Authors
        public async Task<IActionResult> Index()
        {
            List<AuthorResource> authors = new List<AuthorResource>();

            //Sending request to find web api REST service resource GetAllAuthors using HttpClient  
            HttpResponseMessage Res = await _client.GetAsync(baseurl + "Authors");

            //Checking the response is successful or not which is sent using HttpClient  
            if (Res.IsSuccessStatusCode)
            {
                //Storing the response details recieved from web api   
                authors = await Res.Content.ReadAsAsync<List<AuthorResource>>();

            }
            //returning the authors list to view controller
            return View(authors);
        }


        // GET: Authors/Details/5
        public async Task<IActionResult> Details([FromRoute] long id)
        {
            AuthorResource author = new AuthorResource();

            HttpResponseMessage res = await _client.GetAsync(baseurl + $"Authors/{id}");

            if (res.IsSuccessStatusCode)
            {
                author = await res.Content.ReadAsAsync<AuthorResource>();
            }
            return View(author);
        }


        // GET: Authors/Edit/5
        public async Task<IActionResult> Edit([FromRoute] long id)
        {
            AuthorResource author = new AuthorResource();

            HttpResponseMessage res = await _client.GetAsync(baseurl + $"Authors/{id}");

            res.EnsureSuccessStatusCode();

            if (res.IsSuccessStatusCode)
            {
                // Deserialize the updated product from the response body.
                author = await res.Content.ReadAsAsync<AuthorResource>();
                return View(author);
            }
            return View(new ErrorViewModel());
        }


        // POST: Authors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        public async Task<IActionResult> Update([Bind("AuthorId,FirstName,LastName")] AuthorResource author)
        {
            HttpResponseMessage res = await _client.PutAsJsonAsync(baseurl + $"Authors/{author.AuthorId}", author);

            if (res.IsSuccessStatusCode)
            {
                //var updateAuthor = await res.Content.ReadAsAsync<AuthorResource>();
                return RedirectToAction("Details", new { id = author.AuthorId });
            }
            // Deserialize the updated author from the response body.

            return RedirectToAction(nameof(Edit), author.AuthorId);

        }
    }
}
