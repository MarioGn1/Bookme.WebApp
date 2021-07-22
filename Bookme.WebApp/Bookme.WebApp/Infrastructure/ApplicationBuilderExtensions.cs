using Bookme.Data;
using Bookme.Data.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Bookme.WebApp.Infrastructure
{
    public static class ApplicationBuilderExtensions
    {
        public static async Task<IApplicationBuilder> PrepareDatabase(this IApplicationBuilder app)
        {
            using var scopedServices = app.ApplicationServices.CreateScope();

            var data = scopedServices.ServiceProvider.GetService<BookmeDbContext>();

            var userManager = scopedServices.ServiceProvider.GetService<UserManager<ApplicationUser>>();

            var roleManager = scopedServices.ServiceProvider.GetService<RoleManager<IdentityRole>>();

            data.Database.Migrate();

            SeedCategories(data);
            SeedConfirmationTypes(data);
            SeedVisitataionTypes(data);
            await SeedRolesAndAdminUser(data, roleManager, userManager);

            return app;
        }

        private static void SeedCategories(BookmeDbContext data)
        {
            if (data.ServiceCategories.Any())
            {
                return;
            }

            data.ServiceCategories.AddRange(new[]
            {
                new ServiceCategory { Name = "IT Services" },
                new ServiceCategory { Name = "Beauty & Spa" },
                new ServiceCategory { Name = "Education" },
                new ServiceCategory { Name = "Sport" },
                new ServiceCategory { Name = "Finance" },
                new ServiceCategory { Name = "Cleaning Services" },
                new ServiceCategory { Name = "Construction" },
                new ServiceCategory { Name = "Agriculture" },
            });

            data.SaveChanges();
        }

        private static void SeedVisitataionTypes(BookmeDbContext data)
        {
            if (data.VisitationTypes.Any())
            {
                return;
            }

            data.VisitationTypes.AddRange(new[]
            {
                new VisitationType { Type = "Online"},
                new VisitationType { Type = "Company Office/Place"},
                new VisitationType { Type = "Home Visit"},
                new VisitationType { Type = "Third party place"},
            });

            data.SaveChanges();
        }

        private static void SeedConfirmationTypes(BookmeDbContext data)
        {
            if (data.ConfirmationTypes.Any())
            {
                return;
            }

            data.ConfirmationTypes.AddRange(new[]
            {
                new ConfirmationType { Type = "None"},
                new ConfirmationType { Type = "Pending"},
                new ConfirmationType { Type = "Confirmed"},
                new ConfirmationType { Type = "Rejected"},
            });

            data.SaveChanges();
        }

        private static async Task SeedRolesAndAdminUser(BookmeDbContext data, RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            var roles = new string[]
            {
                "Admin",
                "Client",
                "Business",
            };
            foreach (string role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }

            var user = new ApplicationUser() { UserName = "admin@bookme.com", Email = "admin@bookme.com" };
            var password = user.UserName;
            
            var result = await userManager.CreateAsync(user, password);            

            if (!data.Users.Any())
            {
                
            }

            ApplicationUser admin = await userManager.FindByEmailAsync("admin@bookme.com");
            foreach (string role in roles)
            {
                await userManager.AddToRoleAsync(admin, role);
            }
        }

        
    }
}
