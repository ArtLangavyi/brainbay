using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using RickAndMortyApiCrawler.Data.Context;
using RickAndMortyApiCrawler.Data.Initializer;

namespace RickAndMortyApiCrawler;

internal static class ProgramExtensions
{
    internal static IHost EnsureDatabaseUpdated(this IHost host)
    {
        using (var scope = host.Services.CreateScope())
        {
            var services = scope.ServiceProvider;
            try
            {
                var apiCrawlerDbContext = services.GetRequiredService<ApiCrawlerDbContext>();

                DatabaseInitializer.EnsureApiCrawlerDbContexttSeed(apiCrawlerDbContext);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while migrating the database. Error messages is {ex.Message}");

                Environment.Exit(-1);
            }
        }

        return host;
    }
}

