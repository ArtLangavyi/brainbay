using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using RickAndMortyApiCrawler;
using RickAndMortyApiCrawler.Core.Clients;
using RickAndMortyApiCrawler.Core.Infrastructure;
using RickAndMortyApiCrawler.Core.Mappers;
using RickAndMortyApiCrawler.Core.Repositories;
using RickAndMortyApiCrawler.Data.Context;

using System.Net;


Console.WriteLine("Hello, World!");

var builder = Host.CreateDefaultBuilder(args)
    .ConfigureServices((Action<HostBuilderContext, IServiceCollection>)((hostContext, services) =>
    {

        services.AddDbContext<ApiCrawlerDbContext>(db => db.UseSqlServer(hostContext.Configuration.GetConnectionString("DefaultConnection"), b =>
        {
            b.MigrationsAssembly("RickAndMortyApiCrawler.Data");
        }), ServiceLifetime.Singleton);

        services.AddAutoMapper(typeof(MappingProfile).Assembly);

        var rickAndMortyApiSettings = hostContext.Configuration.GetSection("RickAndMortyApiSettings").Get<RickAndMortyApiSettings>();
        ConfigureHttpClientForRickAndMoprtyApi(services, rickAndMortyApiSettings);

        services.AddSingleton(rickAndMortyApiSettings!);
        services.AddTransient<IRickAndMortyApiFactory, RickAndMortyApiFactory>();

        services.AddScoped<ILocationRepository, LocationRepository>();

    }));

var app = builder.Build();

app.EnsureDatabaseUpdated();

static void ConfigureHttpClientForRickAndMoprtyApi(IServiceCollection services, RickAndMortyApiSettings? rickAndMortyApiSettings)
{
    var socketsHttpHandler = new SocketsHttpHandler()
    {
        SslOptions = new System.Net.Security.SslClientAuthenticationOptions()
        {
            EnabledSslProtocols = System.Security.Authentication.SslProtocols.Tls12 | System.Security.Authentication.SslProtocols.Tls13,
        },
        MaxConnectionsPerServer = int.MaxValue,
        EnableMultipleHttp2Connections = true
    };

    if (rickAndMortyApiSettings?.ProxyEnabled == true)
    {
        services.AddHttpClient(RickAndMortyApiFactory.ClientName, o => { o.BaseAddress = new Uri(rickAndMortyApiSettings.BaseUrl); })
        .ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler()
        {

            Proxy = new WebProxy(new Uri(rickAndMortyApiSettings.ProxyUri)),
            UseProxy = true
        }).ConfigurePrimaryHttpMessageHandler(() => socketsHttpHandler);
    }
    else if (rickAndMortyApiSettings != null)
    {
        services.AddHttpClient(RickAndMortyApiFactory.ClientName, o =>
        {
            o.Timeout = TimeSpan.FromSeconds(rickAndMortyApiSettings.HttpClientTimeoutSeconds);
            o.BaseAddress = new Uri(rickAndMortyApiSettings.BaseUrl);
        }).ConfigurePrimaryHttpMessageHandler(() => socketsHttpHandler);
    }
}