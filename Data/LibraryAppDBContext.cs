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
        public DbSet<Favourite> Favourites { get; set; }
        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }
    }
}
