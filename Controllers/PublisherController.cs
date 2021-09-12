using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Net;
using LibraryAPI.Resources;
using Microsoft.AspNetCore.Authorization;

namespace LibraryWebApp.Controllers
{
    public class PublisherController : Controller
    {

        // HttpClient is intended to be instantiated once per application, rather than per-use.
        private readonly HttpClient _client = new HttpClient();

        const string baseurl = "https://localhost:44351/api/";

        public PublisherController(HttpClient client)
        {
            _client = client;
        }

        // GET: Publishers
        public async Task<IActionResult> Index()
        {
            List<PublisherResource> genres = new List<PublisherResource>();

            //Sending request to find web api REST service resource GetAllPublishers using HttpClient  
            HttpResponseMessage Res = await _client.GetAsync(baseurl + "Publishers");

            //Checking the response is successful or not which is sent using HttpClient  
            if (Res.IsSuccessStatusCode)
            {
                //Storing the response details recieved from web api   
                genres = await Res.Content.ReadAsAsync<List<PublisherResource>>();
            }
            //returning the genres list to view controller
            return View(genres);
        }

        // GET: Publisher/Details/5
        public async Task<IActionResult> Details([FromRoute]long id)
        {
            PublisherResource publisher = new PublisherResource();

            HttpResponseMessage res = await _client.GetAsync(baseurl + $"Publishers/{id}");

            if (res.IsSuccessStatusCode)
            {
                 publisher = await res.Content.ReadAsAsync<PublisherResource>();
            }
            return View(publisher);
        }

        // GET: Publishers/Create
        [Authorize(Roles = "Librarian,Administrator")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Publishers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Librarian,Administrator")]
        public async Task<IActionResult> Add([Bind("PublisherId,Name")] PublisherResource publisher)
        {
            PublisherResource updatedPublisher = new PublisherResource();

            HttpResponseMessage res = await _client.PostAsJsonAsync(baseurl + "Publishers", publisher);

            res.EnsureSuccessStatusCode();

            if (res.IsSuccessStatusCode)
            {
                ViewBag.Feedback = "PublisherResource Created Successfully";
                updatedPublisher = await res.Content.ReadAsAsync<PublisherResource>();
                return RedirectToAction("Details", new { id = updatedPublisher.PublisherId });
            }

            ViewData["Feedback"] = "Sorry, publisher wasn't Created";
            return RedirectToAction("Error");
        }

        // GET: Publishers/Edit/5
        [Authorize(Roles = "Librarian,Administrator")]
        public async Task<IActionResult> Edit([FromRoute]long id)
        {
            PublisherResource publisher = new PublisherResource();

            HttpResponseMessage res = await _client.GetAsync(baseurl + $"Publishers/{id}");

            res.EnsureSuccessStatusCode();

            if (res.IsSuccessStatusCode)
            {
                // Deserialize the updated product from the response body.
                publisher = await res.Content.ReadAsAsync<PublisherResource>();
                return View(publisher);
            }
            return View("Error");
        }


        // POST: Publishers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Librarian,Administrator")]
        public async Task<IActionResult> Update([Bind("PublisherId,Name")] PublisherResource publisher)
        {
            //var json = JsonConvert.SerializeObject(publisher);

            HttpResponseMessage res = await _client.PutAsJsonAsync(baseurl + $"Publishers/{publisher.PublisherId}", publisher);

            if (res.IsSuccessStatusCode)
            {
                //var updatePublisher = await res.Content.ReadAsAsync<PublisherResource>();
                return RedirectToAction("Details", new { id = publisher.PublisherId });
            }
            // Deserialize the updated publisher from the response body.
            
            return View(nameof(Edit), publisher.PublisherId);
            
        }

        // GET: Publishers/Delete/5
        [Authorize(Roles = "Librarian,Administrator")]
        public async Task<HttpStatusCode> Delete([FromRoute]long id)
        {
            HttpResponseMessage res = await _client.DeleteAsync(baseurl + $"Publishers/{id}");

            return res.StatusCode;
        }

        /*// POST: Publishers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var publisher = await _context.Publishers.FindAsync(id);
            _context.Publishers.Remove(publisher);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PublisherExists(long id)
        {
            return _context.Publishers.Any(e => e.PublisherId == id);
        }*/
    }
}
