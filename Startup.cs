using LibraryWebApp.Domain.Repositories;
using LibraryWebApp.Persistence.Contexts;
using LibraryWebApp.Persistence.Repositories;
using LibraryWebApp.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
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

            services.AddDbContext<AppDbContext>(options =>
                                                options.UseSqlServer(Configuration["ConnectionString:LibraryAppDB"]));


            // Repository Services 
            services.AddScoped<IBookingRepository, BookingRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            // Services
            services.AddScoped<IBookingService, BookingService>();

            // Automapper Service
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());


            /*services.AddHttpClient();
            services.AddHttpClient("meta", c =>
            {
                c.BaseAddress = new Uri(Configuration.GetValue<string>("MetaAPI"));
                c.DefaultRequestHeaders.Accept.Clear();
                c.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
            });*/
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

            app.UseDefaultFiles();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                /*endpoints.MapControllerRoute(
                    name: "select",
                    pattern: "{controller=Home}/{genreName?}");*/
            });
        }
    }
}
