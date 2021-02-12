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
        public DbSet<Genre> Genres { get; set; }
        public DbSet<BooksGenres> BooksGenres { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BooksGenres>()
                .HasKey(b => new { b.BookId, b.GenreId });
        }
    }
}
