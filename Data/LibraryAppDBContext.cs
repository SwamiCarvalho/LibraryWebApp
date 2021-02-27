using LibraryAPI.Models;
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

        public DbSet<Reader> Reader { get; set; }
        public DbSet<Requisition> Requisition { get; set; }
        public DbSet<Favourites> Favourites { get; set; }
        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Requisition>()
                .HasKey(r => new { r.ReaderId, r.BookId });

            modelBuilder.Entity<Favourites>()
                .HasKey(f => new { f.ReaderId, f.BookId });
        }
    }
}
