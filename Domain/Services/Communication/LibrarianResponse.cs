using LibraryWebApp.Domain.Models;
using LibraryWebApp.Resources;
using System.Collections.Generic;

namespace LibraryWebApp.Domain.Services.Communication
{
    public class LibrarianResponse : BaseResponse
    {
        public LibrarianResource Librarian { get; private set; }
        public IEnumerable<LibrarianResource> Librarians { get; private set; }

        private LibrarianResponse(bool success, string message, LibrarianResource librarian, IEnumerable<LibrarianResource> librarians) : base(success, message)
        {
            Librarian = librarian;
            Librarians = librarians;
        }

        /// <summary>
        /// Creates a success response.
        /// </summary>
        /// <param name="librarian">Saved category.</param>
        /// <returns>Response.</returns>
        public LibrarianResponse(LibrarianResource librarian) : this(true, string.Empty, librarian, null)
        { }

        /// <summary>
        /// Creates am error response.
        /// </summary>
        /// <param name="message">Error message.</param>
        /// <returns>Response.</returns>
        public LibrarianResponse(string message) : this(false, message, null, null)
        { }

        public LibrarianResponse(IEnumerable<LibrarianResource> librarians) : this(true, string.Empty, null, librarians)
        { }
    }
}