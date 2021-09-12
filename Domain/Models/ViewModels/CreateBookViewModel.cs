using LibraryAPI.Resources;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace LibraryWebApp.Domain.Models.ViewModels
{
    [AllowAnonymous]
    public class CreateBookViewModel : PageModel
    {
        // HttpClient is intended to be instantiated once per application, rather than per-use.
        private readonly HttpClient _client = new HttpClient();

        const string baseurl = "https://localhost:44351/api/";

        public CreateBookViewModel(HttpClient client) 
        {
            _client = client;
        }

        [BindProperty]
        public InputModel Input { get; set; }
        public SelectList GenresOptions { get; set; }
        public SelectList AuthorsOptions { get; set; }

        public class InputModel
        {
            [Required(ErrorMessage = "Please add the book title.")]
            public string Title { get; set; }

            [DisplayName("Original Title")]
            public string OgTitle { get; set; }

            [Required(ErrorMessage = "Please add the book publication year.")]
            [DisplayName("Publication Year")]
            public int PublicationYear { get; set; }

            public int Edition { get; set; }

            public string Notes { get; set; }

            public string PhysicalDescription { get; set; }

            [Required]
            public int[] Genres { get; set; }

            [Required]
            public int[] Authors { get; set; }
        }

        public async Task OnGetAsync()
        {
            var genres = await GetGenres();
            var authors = await GetAuthors();

            GenresOptions = new SelectList(genres, nameof(GenreResource.GenreId), nameof(GenreResource.Name));
            AuthorsOptions = new SelectList(authors, nameof(AuthorResource.AuthorId), nameof(AuthorResource.FullName));
            
        }

        public async Task<List<SelectListItem>> GetGenres()
        {
            //Sending request to find web api REST service resource GetAllGenres using HttpClient to populate Dropdown box.
            HttpResponseMessage resGenres = await _client.GetAsync(baseurl + "Genres");

            //Storing the response details recieved from web api   
            var genres = await resGenres.Content.ReadAsAsync<List<GenreResource>>();
            var genreOptions = genres.Select(g => new SelectListItem
            {
                Value = g.GenreId.ToString(),
                Text = g.Name
            }).ToList();
            return genreOptions;
        }

        public async Task<List<SelectListItem>> GetAuthors()
        {
            //Sending request to find web api REST service resource GetAllAuthors using HttpClient to populate Dropdown box.
            HttpResponseMessage resAuthors = await _client.GetAsync(baseurl + "Auhors");

            //Storing the response details recieved from web api   
            var authors = await resAuthors.Content.ReadAsAsync<List<AuthorResource>>();
            var authorOptions = authors.Select(a => new SelectListItem
            {
                Value = a.AuthorId.ToString(),
                Text = a.FullName
            }).ToList();
            return authorOptions;
        }
    }
}
