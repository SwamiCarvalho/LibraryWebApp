using LibraryWebApp.Persistence.Contexts;

namespace LibraryWebApp.Persistence
{
    public class SeedData
    {
        public static void Initialize(AppDbContext context)
        {
            // Automatically creates the database
            context.Database.EnsureCreated();

            
        }
    }
}
