using Microsoft.EntityFrameworkCore;

using RickAndMortyApiCrawler.Data.Context;


namespace RickAndMortyApiCrawler.Data.Initializer;
public class DatabaseInitializer
{
    public static void EnsureApiCrawlerDbContexttSeed(ApiCrawlerDbContext context)
    {
        // For migrations applied automatically at startup set much bigger CommandTimeout and restore it after all migrations are completed

        var stashedTimeout = context.Database.GetCommandTimeout();

        context.Database.SetCommandTimeout(TimeSpan.FromMinutes(10));

        context.Database.Migrate();

        context.Database.SetCommandTimeout(stashedTimeout);
    }
}
