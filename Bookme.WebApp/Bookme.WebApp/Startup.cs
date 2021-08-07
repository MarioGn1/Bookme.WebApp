using Bookme.Data;
using Bookme.Data.Models;
using Bookme.Services;
using Bookme.Services.Contracts;
using Bookme.WebApp.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Threading.Tasks;

namespace Bookme.WebApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
            => Configuration = configuration;


        public IConfiguration Configuration { get; }


        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddDbContext<BookmeDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services
                .AddDatabaseDeveloperPageExceptionFilter();

            services
                .AddDefaultIdentity<ApplicationUser>(options =>
                {
                    options.Password.RequireDigit = false;
                    options.Password.RequireLowercase = false;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireUppercase = false;
                })
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<BookmeDbContext>();

            services
                .AddControllersWithViews(options =>
                options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute()));

            services.AddAutoMapper(typeof(BookmeProfile));

            services
                .AddTransient<IBusinessOffersService, BusinessOffersService>()
                .AddTransient<IHomeService, HomeService>()
                .AddTransient<IBusinessService, BusinessService>()
                .AddTransient<IBookingConfigurationService, BookingConfigurationService>()
                .AddTransient<ICategoryService, CategoryService>()
                .AddTransient<IBookingService, BookingService>();
        }


        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            Task.Run(() => app.PrepareDatabase())
                .Wait();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
            app.UseHttpsRedirection()
                .UseStaticFiles()
                .UseRouting()
                .UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "Areas",
                    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}/{date?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
