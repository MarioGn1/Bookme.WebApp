using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace Bookme.Data
{
    public class BookmeDbContextDesignTimeFactory : IDesignTimeDbContextFactory<BookmeDbContext>
    {
        public BookmeDbContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var builder = new DbContextOptionsBuilder<BookmeDbContext>();
            builder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));

            return new BookmeDbContext(builder.Options);
        }
    }
}
