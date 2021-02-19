using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LibraryWebApp.Data;
using LibraryAPI.Models;
using System.Net.Http;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using LibraryAPI.DTOs;

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
        static readonly HttpClient client = new HttpClient();

        //Hosted web API REST Service base url  
        string Baseurl = "https://localhost:44351/";

        // GET: Books
        public async Task<IActionResult> Index(string? searchTerm = null, string? genre = null, string? author = null)
        {
            List<BookDTO> model = new List<BookDTO>();
            ViewData["searchTerm"] = searchTerm;
            ViewData["genreFilter"] = genre;
            ViewData["authorFilter"] = author;


            try
            {
                //Passing service base url  
                client.BaseAddress = new Uri(Baseurl);

                //Define request data format  
                //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //Sending request to find web api REST service resource GetAllBooks using HttpClient  
                HttpResponseMessage Res = await client.GetAsync("api/Books");

                //Checking the response is successful or not which is sent using HttpClient  
                if (Res.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api   
                    var BooksResponse = Res.Content.ReadAsStringAsync().Result;

                    //Deserializing the response recieved from web api and storing into the Employee list  
                    model = JsonConvert.DeserializeObject<List<BookDTO>>(BooksResponse);

                }
                //returning the employee list to view 
                return View(model);
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
                var msg = "Something is Wrong";
                return View(msg, e.Message);
            }
        }

        // GET: Books/Details/5
        public async Task<IActionResult> Details()
        {
            BookDTO model = new BookDTO();
            try
            {
                client.BaseAddress = new Uri(Baseurl);


                HttpResponseMessage res = await client.GetAsync("api/Books/{id}");

                if (res.IsSuccessStatusCode)
                {
                    var BookResponse = res.Content.ReadAsStringAsync().Result;

                    model = JsonConvert.DeserializeObject<BookDTO>(BookResponse);

                }
                return View(model);

            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
                var msg = "Something is Wrong";
                return View(msg, e.Message);

            }
        }


        // POST: Books/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,OgTitle,PublicationYear,Edition,Notes,PhysicalDescription")] Book book)
        {
            try
            {
                client.BaseAddress = new Uri(Baseurl);
                if (ModelState.IsValid)
                {
                    HttpResponseMessage res = await client.GetAsync("api/Books");

                    //_context.Add(book);
                    //await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                return View(book);
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
                var msg = "Something is Wrong";
                return View(msg, e.Message);
            }

        }

        // Put: Books/Edit/5
        [HttpPut]
        public async Task<IActionResult> Edit()
        {
            BookDTO model = new BookDTO();
            try
            {
                client.BaseAddress = new Uri(Baseurl);

                HttpResponseMessage res = await client.GetAsync("api/Books/{id}");

                if (res.IsSuccessStatusCode)
                {
                    var BookResponse = res.Content.ReadAsStringAsync().Result;
                    model = JsonConvert.DeserializeObject<BookDTO>(BookResponse);
                }
                return View(model);
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
                var msg = "Something is Wrong";
                return View(msg);
            }
        }

        // POST: Genres/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,Title,OgTitle,PublicationYear,Edition,Notes,PhysicalDescription")] Book book)
        {
            BookDTO model = new BookDTO();


            try
            {
                client.BaseAddress = new Uri(Baseurl);

                HttpResponseMessage res = await client.GetAsync("api/Books/{id}");

                if (res.IsSuccessStatusCode)
                {
                    var BookResponse = res.Content.ReadAsStringAsync().Result;
                    model = JsonConvert.DeserializeObject<BookDTO>(BookResponse);
                }
                return RedirectToAction(nameof(Details));
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
                var msg = "Something is Wrong";
                return View(msg);
            }
            
        }
            
    

        // GET: Books/Delete/5
        [HttpDelete]
        public async Task<IActionResult> Delete(long? id)
        {
            try
            {
                client.BaseAddress = new Uri(Baseurl);

                HttpResponseMessage res = await client.GetAsync("api/Books/{id}");

                if (res.IsSuccessStatusCode)
                {
                    var BookResponse = res.Content.ReadAsStringAsync().Result;
                }
                return RedirectToAction(nameof(Details));
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
                var msg = "Something is Wrong";
                return View(msg);
            }
        }

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            try
            {
                client.BaseAddress = new Uri(Baseurl);
                HttpResponseMessage res = await client.GetAsync("api/Books/{id}");

                if (res.IsSuccessStatusCode)
                {
                    var BookResponse = res.Content.ReadAsStringAsync().Result;
                }
                return View();
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
                var msg = "Something is Wrong";
                return View(msg);
            }
            //var book = await _context.Books.FindAsync(id);
            //_context.Books.Remove(book);
            //await _context.SaveChangesAsync();
            //return RedirectToAction(nameof(Index));
        }
    }
}
