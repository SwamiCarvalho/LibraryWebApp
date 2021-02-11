using LibraryWebApp.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryWebApp.Models
{
    public class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new LibraryAppDBContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<LibraryAppDBContext>>()))
            {

                ////////////////// BOOKS //////////////////////

                // Look for any Books.
                if (context.Books.Any())
                {
                    return;   // DB has been seeded
                }


                // Seed Books Table
                var books = new Book[]
                {
                    new Book(){
                    Title = "Decidir num piscar de olhos",
                    OgTitle = "Blink!",
                    PublicationYear = 2009,
                    Edition = 4,
                    PhysicalDescription = "263 p. ; 24 cm"
                    },

                    new Book(){
                    Title = "Intuição",
                    OgTitle = "Intuition: knowing beyond logic",
                    PublicationYear = 2006,
                    PhysicalDescription = "196 p. ; 22 cm"
                    },

                    new Book(){
                    Title = "Criatividade : libertar as forças interiores",
                    PublicationYear = 2006,
                    PhysicalDescription = "196 p. ; 22 cm"
                    }
                };

                foreach (Book book in books)
                {
                    context.Books.Add(book);
                }
                context.SaveChanges();
            }
        }
    }
}
