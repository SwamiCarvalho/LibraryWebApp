using LibraryWebApp.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using System.Collections.Generic;

namespace LibraryWebApp.Persistence.Contexts
{

    public class AppDbContext : IdentityDbContext<IdentityUser>
    {
        public AppDbContext (DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Reader> Reader { get; set; }
        public DbSet<Librarian> Librarian { get; set; }
        public DbSet<Favourite> Favourites { get; set; }
        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);



            // Seed Roles
            modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole { Name = "Administrator", NormalizedName = "ADMINISTRATOR" });
            modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole { Name = "Reader", NormalizedName = "READER" });
            modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole { Name = "Librarian", NormalizedName = "LIBRARIAN" });

        }
    }
}
