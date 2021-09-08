using LibraryWebApp.Domain.Repositories;
using LibraryWebApp.Persistence;
using LibraryWebApp.Persistence.Contexts;
using LibraryWebApp.Persistence.Repositories;
using LibraryWebApp.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Net.Http;
using System.Net.Http.Headers;

namespace LibraryWebApp
{
    public class Startup
    {
        static HttpClient client = new HttpClient();
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Update port # in the following line.
            client.BaseAddress = new Uri("http://localhost:44330/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            services.AddHttpClient();

            services.AddControllersWithViews();
            services.AddRazorPages();

            // Add Database Context.
            services.AddDbContext<AppDbContext>(options =>
                                                options.UseSqlServer(Configuration.GetConnectionString("LibraryWebAppDB")));

            // Add Cookies
            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            // Add Identity User with Roles
            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();

            services.Configure<IdentityOptions>(options =>
            {
                // Default SignIn settings.
                options.SignIn.RequireConfirmedEmail = false;
                options.SignIn.RequireConfirmedPhoneNumber = false;
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Latest)
                    .AddRazorPagesOptions(options =>
                    {
                        options.Conventions.AuthorizeAreaFolder("Identity", "/Account/Manage");
                        options.Conventions.AuthorizeAreaPage("Identity", "/Account/Logout");
                    });

            /*services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = $"/Identity/Account/Login";
                options.LogoutPath = $"/Identity/Account/Logout";
                options.AccessDeniedPath = $"/Identity/Account/AccessDenied";
            });*/

            // Repository Services 
            services.AddScoped<IBookingRepository, BookingRepository>();
            services.AddScoped<ILibrarianRepository, LibrarianRepository>();
            services.AddScoped<IReaderRepository, ReaderRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            // Services
            services.AddScoped<IBookingService, BookingService>();
            services.AddScoped<IUsersRolesService, UsersRolesService>();
            services.AddScoped<ILibrarianService, LibrarianService>();
            services.AddScoped<IReaderService, ReaderService>();
            services.AddScoped<IGeneralService, GeneralService>();

            // Automapper Service
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
