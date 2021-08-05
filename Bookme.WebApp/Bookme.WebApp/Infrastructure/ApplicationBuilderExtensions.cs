using Bookme.Data;
using Bookme.Data.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
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
                new ServiceCategory { Name = "IT Services", ImageUrl = "https://serviceware-se.com/fileadmin/user_upload/itsm-graphic.png"},
                new ServiceCategory { Name = "Beauty & Spa", ImageUrl = "https://st2.depositphotos.com/3153751/6567/v/950/depositphotos_65673765-stock-illustration-beauty-salon-spa-and-wellness.jpg" },
                new ServiceCategory { Name = "Education", ImageUrl = "https://www.appway.com/resource/object/academy_Img1/LatestCommittedFilter/Appway-Academy-Canvas.png" },
                new ServiceCategory { Name = "Sport", ImageUrl = "https://anglecds.files.wordpress.com/2017/10/sports.jpg?w=640" },
                new ServiceCategory { Name = "Finance", ImageUrl = "https://images.squarespace-cdn.com/content/v1/5dc49baf2fbe195d883a2b07/1611605002923-DUAVMQPH06BVBY1HYGMO/OOTB_solutions_financial_services_illustration_two_people_running_finance.png" },
                new ServiceCategory { Name = "Cleaning Services", ImageUrl = "https://previews.123rf.com/images/drogatnev/drogatnev1604/drogatnev160400098/55054160-cleaning-service-flat-illustration-poster-template-for-house-cleaning-services-with-various-cleaning.jpg" },
                new ServiceCategory { Name = "Construction", ImageUrl = "https://toppng.com/uploads/preview/construction-icons-construction-icon-free-11553457510i6dcbo387u.png" },
                new ServiceCategory { Name = "Agriculture", ImageUrl = "https://www.uidownload.com/files/1012/721/214/farming-vector-illustration.jpg" },
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

            if (!data.Users.Any())
            {
                var user = new ApplicationUser() { UserName = "admin@bookme.com", Email = "admin@bookme.com" };
                var password = user.UserName;

                var result = await userManager.CreateAsync(user, password);
            }

            ApplicationUser admin = await userManager.FindByEmailAsync("admin@bookme.com");
            foreach (string role in roles)
            {
                await userManager.AddToRoleAsync(admin, role);
            }
        }

        
    }
}
