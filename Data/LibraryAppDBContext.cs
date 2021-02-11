

using LibraryWebApp.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryWebApp.Data
{
    public class LibraryAppDBContext : DbContext
    {
        public LibraryAppDBContext (DbContextOptions<LibraryAppDBContext> options)
            : base(options)
        {
        }

        public DbSet<Book> Books { get; set; }
    }
}
