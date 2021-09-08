using LibraryWebApp.Persistence;
using LibraryWebApp.Persistence.Contexts;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;

namespace LibraryWebApp
{
    public class Program
    {

        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            // If Data Model changes, delete database and update seed method, starting fresh with a new database
            CreateDbIfNotExists(host);

            host.Run();
        }

        private static void CreateDbIfNotExists(IHost host/*, UserManager<IdentityUser> userManager*/)
        {
            using var scope = host.Services.CreateScope();
            var services = scope.ServiceProvider;
            try
            {
                // Get a database context instance from the dependency injection container.
                var context = services.GetRequiredService<AppDbContext>();
                // Call the SeedData.Initialize method to seed Database
                context.Database.EnsureCreated();
                //SeedData.Initialize(userManager);
            }
            catch (Exception ex)
            {
                Console.WriteLine("IOException source: {0}", ex.Source);
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureLogging(logging =>
                {
                    logging.ClearProviders();
                    logging.AddConsole();
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
