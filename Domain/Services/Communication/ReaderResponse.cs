using LibraryWebApp.Domain.Models;
using LibraryWebApp.Resources;
using System.Collections.Generic;

namespace LibraryWebApp.Domain.Services.Communication
{
    public class ReaderResponse : BaseResponse
    {
        public ReaderResource Reader { get; private set; }
        public IEnumerable<ReaderResource> Readers { get; private set; }

        private ReaderResponse(bool success, string message, ReaderResource reader, IEnumerable<ReaderResource> readers) : base(success, message)
        {
            Reader = reader;
            Readers = readers;
        }

        /// <summary>
        /// Creates a success response.
        /// </summary>
        /// <param name="reader">Saved category.</param>
        /// <returns>Response.</returns>
        public ReaderResponse(ReaderResource reader) : this(true, string.Empty, reader, null)
        { }

        /// <summary>
        /// Creates am error response.
        /// </summary>
        /// <param name="message">Error message.</param>
        /// <returns>Response.</returns>
        public ReaderResponse(string message) : this(false, message, null, null)
        { }

        public ReaderResponse(IEnumerable<ReaderResource> readers) : this(true, string.Empty, null, readers)
        { }
    }
}