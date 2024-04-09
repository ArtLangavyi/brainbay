// See https://aka.ms/new-console-template for more information
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using RickAndMortyApiCrawler;
using RickAndMortyApiCrawler.Data.Context;


Console.WriteLine("Hello, World!");

var builder = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
{
    services.AddDbContext<ApiCrawlerDbContext>(db => db.UseSqlServer(hostContext.Configuration.GetConnectionString("DefaultConnection"), b =>
    {
        b.MigrationsAssembly("RickAndMortyApiCrawler.Data");
    }), ServiceLifetime.Singleton);
});

var app = builder.Build();

app.EnsureDatabaseUpdated();
