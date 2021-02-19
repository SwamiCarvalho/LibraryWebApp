using Microsoft.AspNetCore.Mvc;
using LibraryAPI.Models;
using LibraryWebApp.Interfaces;

namespace LibraryWebApp.Controllers
{
    public class BooksController : Controller
    {
        /*private readonly LibraryAppDBContext _context;

        public BooksController(LibraryAppDBContext context)
        {
            _context = context;
        }*/

        private IBooksServices _services;

        public BooksController(IBooksServices services)
        {
            _services = services;
        }

        // GET: Books
        public IActionResult Index([FromQuery] string searchTerm, [FromQuery] string genre, [FromQuery] string author)
        {
            ViewData["searchTerm"] = searchTerm;
            ViewData["genreFilter"] = genre;
            ViewData["authorFilter"] = author;

            var books = _services.GetBooksAsync();

            return View(books);
        
        }

        // GET: Books/Details/5
        public IActionResult Details([FromRoute] long id)
        {
            var book = _services.GetBookByIdAsync(id);

            return View(book);
        }


        // POST: Books/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.

        public IActionResult Create([FromBody] Book book)
        {
            var newBook = _services.CreateBookAsync(book);
            return CreatedAtAction("Details", new { id = newBook.Id }, newBook);
        }


        public IActionResult Edit([FromRoute] long id)
        {
            var book = _services.GetBookByIdAsync(id);
            return View(book);
        }


        // Post Updated Book
        // POST 
        public IActionResult Update([FromRoute] long id, [Bind("Id,Title,OgTitle,PublicationYear,Edition,Notes,PhysicalDescription")] Book book)
        {
            var updatedBook = _services.UpdateBookAsync(book);
            return CreatedAtAction("Details", new { id = updatedBook.Id }, updatedBook);
        }
    

        // GET: Books/Delete/5
        public IActionResult Delete([FromRoute]long id)
        {
            var deletedBook = _services.DeleteBookAsync(id);
            return RedirectToAction(nameof(Index));
        }

        /*// POST: Books/Delete/5
        public IActionResult DeleteConfirmed([FromRoute] long id)
        {
            var deletedBook = _services.DeleteBookConfirmed(id);
            return RedirectToAction(nameof(Index));
        }*/

    }
}
