using LibraryWebApp.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryWebApp.Persistence.Contexts
{
    public class AppDbContext : DbContext
    {
        public AppDbContext (DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Reader> Reader { get; set; }
        public DbSet<Booking> Requisition { get; set; }
        public DbSet<Favourite> Favourites { get; set; }
        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

        }
    }
}
