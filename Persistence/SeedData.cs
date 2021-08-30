using LibraryWebApp.Persistence.Contexts;

namespace LibraryWebApp.Persistence
{
    public class SeedData
    {
        public static void Initialize(AppDbContext context)
        {
            // Automatically creates the database
            context.Database.EnsureCreated();
            
                /*///////////////////// GENRES /////////////////////////

                // Look for any Genres.
                if (context.Genres.Any())
                {
                    return;   // DB has been seeded
                }

                // Seed Genres Table
                var intuition = new Genre { Name = "Intuição" };
                var personalDevelopment = new Genre { Name = "Desenvolvimento Pessoal" };
                var creativity = new Genre { Name = "Criatividade" };
                context.Genres.AddRange(intuition, personalDevelopment, creativity);
                context.SaveChanges();


                ////////////////// BOOKS //////////////////////

                // Look for any Books.
                if (context.Books.Any())
                {
                    return;   // DB has been seeded
                }


                // Seed Books Table
                var blink_gladwell = new Book
                {
                    Title = "Decidir num piscar de olhos",
                    OgTitle = "Blink!",
                    PublicationYear = 2009,
                    Edition = 4,
                    PhysicalDescription = "263 p. ; 24 cm",
                };

                var intuition_osho = new Book
                {
                    Title = "Intuição",
                    OgTitle = "Intuition: knowing beyond logic",
                    PublicationYear = 2006,
                    PhysicalDescription = "196 p. ; 22 cm",
                };

                var creativity_osho = new Book
                {
                    Title = "Criatividade : libertar as forças interiores",
                    PublicationYear = 2006
                };

                context.Books.AddRange(blink_gladwell, intuition_osho, creativity_osho);
                context.SaveChanges();


                ////////// SEED BOOKSGENRES /////////////

                // Look for any Books.
                if (context.BooksGenres.Any())
                {
                    return;   // DB has been seeded
                }

                var booksGenres = new BooksGenres[]
                {
                new BooksGenres { BookId = blink_gladwell.Id, GenreId = intuition.Id },
                new BooksGenres { BookId = intuition_osho.Id, GenreId = personalDevelopment.Id },
                new BooksGenres { BookId = intuition_osho.Id, GenreId = creativity.Id },
                new BooksGenres { BookId = creativity_osho.Id, GenreId = creativity.Id }
                };

                foreach (BooksGenres bookGenres in booksGenres)
                {
                    context.BooksGenres.Add(bookGenres);
                }
                context.SaveChanges();*/
            
        }
    }
}
