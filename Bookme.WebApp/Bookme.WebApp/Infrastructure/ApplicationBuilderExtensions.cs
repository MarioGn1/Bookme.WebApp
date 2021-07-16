using Bookme.Data;
using Bookme.Data.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;

namespace Bookme.WebApp.Infrastructure
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder PrepareDatabase(this IApplicationBuilder app)
        {
            using var scopedServices = app.ApplicationServices.CreateScope();

            var data = scopedServices.ServiceProvider.GetService<BookmeDbContext>();

            data.Database.Migrate();

            SeedCategories(data);
            SeedConfirmationTypes(data);
            SeedVisitataionTypes(data);

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
                new ServiceCategorie { Name = "IT Services" },
                new ServiceCategorie { Name = "Beauty & Spa" },
                new ServiceCategorie { Name = "Education" },
                new ServiceCategorie { Name = "Sport" },
                new ServiceCategorie { Name = "Finance" },
                new ServiceCategorie { Name = "Cleaning Services" },
                new ServiceCategorie { Name = "Construction" },
                new ServiceCategorie { Name = "Agriculture" },
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
    }
}
