using LibraryAPI.Resources;
using LibraryWebApp.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
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
            //Sending request to find web api REST service resource GetAllAuthors using HttpClient  
            HttpResponseMessage res = await _client.GetAsync(baseurl + "Authors");

            //Checking the response is successful or not which is sent using HttpClient  
            if (!res.IsSuccessStatusCode)
                RedirectToAction("Error");

            //Storing the response details recieved from web api   
            var authors = await res.Content.ReadAsAsync<List<AuthorResource>>();

            //returning the authors list to view controller
            return View(authors);

        }


        // GET: Authors/Details/5
        public async Task<IActionResult> Details([FromRoute] long id)
        {
            HttpResponseMessage res = await _client.GetAsync(baseurl + $"Authors/{id}");

            if (!res.IsSuccessStatusCode)
                return View("Error", new ErrorViewModel());

            var author = await res.Content.ReadAsAsync<AuthorResource>();
            return View(author);
        }


        // GET: Authors/Edit/5
        [Authorize(Roles = "Librarian,Administrator")]
        public async Task<IActionResult> Edit([FromRoute] long id)
        {
            HttpResponseMessage res = await _client.GetAsync(baseurl + $"Authors/{id}");

            if (!res.IsSuccessStatusCode)
                RedirectToAction("Error");

            // Deserialize the updated product from the response body.
            var author = await res.Content.ReadAsAsync<AuthorResource>();
            return View(author);

        }


        // POST: Authors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Librarian,Administrator")]
        public async Task<IActionResult> Update([Bind("AuthorId,FirstName,LastName")] AuthorResource author)
        {
            HttpResponseMessage res = await _client.PutAsJsonAsync(baseurl + $"Authors/{author.AuthorId}", author);

            if (!res.IsSuccessStatusCode)
                return RedirectToAction(nameof(Edit), author.AuthorId);

            TempData["message"] = String.Format("Author {} Successfully Updated.", author.FullName);

            //var updateAuthor = await res.Content.ReadAsAsync<AuthorResource>();
            return RedirectToAction("Details", new { id = author.AuthorId });
        }

        [Authorize(Roles = "Librarian,Administrator")]
        public IActionResult Create()
        {
            return View();
        }

        [Authorize(Roles = "Librarian,Administrator")]
        public async Task<IActionResult> Add([Bind("FirstName,LastName")] SaveAuthorResource author)
        {
            HttpResponseMessage res = await _client.PostAsJsonAsync(baseurl + "Authors", author);

            if (!res.IsSuccessStatusCode)
            {
                ViewData["Feedback"] = "Sorry, author wasn't Created";
                return RedirectToAction("Error", new ErrorViewModel());
            }
                
            var newAuthor = await res.Content.ReadAsAsync<AuthorResource>();
            return RedirectToAction(nameof(Details), new { id = newAuthor.AuthorId });
            //return CreatedAtRoute("DefaultApi", new { id = author.Id }, dto);
        }

        [Authorize(Roles = "Librarian,Administrator")]
        [Route("Authors/Delete/{id}")]
        public async Task<IActionResult> Delete([FromRoute]long id)
        {
            var authorRes = await _client.GetAsync(baseurl + $"Authors/{id}");

            if (!authorRes.IsSuccessStatusCode)
                return View("Error", new ErrorViewModel());

            // Deserialize the updated author from the response body.
            var author = await authorRes.Content.ReadAsAsync<AuthorResource>();
            return View(author);
        }

        [Authorize(Roles = "Librarian,Administrator")]
        // POST: Authors/Delete/5
        public async Task<IActionResult> DeleteConfirmed()
        {
            var authorIdString = Request.Form["AuthorId"];
            var id = Convert.ToInt64(authorIdString);

            var res = await _client.DeleteAsync(baseurl + $"Authors/{id}");

            if (!res.IsSuccessStatusCode)
            {
                ViewData["Feedback"] = "Sorry, author wasn't Deleted";
                return View("Error", new ErrorViewModel());
            }

            // Deserialize the deleted author from the response body.
            var author = await res.Content.ReadAsAsync<AuthorResource>();
            TempData["message"] = String.Format("Author {} Successfully Updated.", author.FullName);

            return RedirectToAction(nameof(Index));
        }
    }
}   
