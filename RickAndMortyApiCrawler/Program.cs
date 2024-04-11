using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using RickAndMortyApiCrawler;
using RickAndMortyApiCrawler.Core.Clients;
using RickAndMortyApiCrawler.Core.Infrastructure;
using RickAndMortyApiCrawler.Core.Mappers;
using RickAndMortyApiCrawler.Core.Repositories;
using RickAndMortyApiCrawler.Core.Services;
using RickAndMortyApiCrawler.Core.Services.Abstractions;
using RickAndMortyApiCrawler.Data.Context;

using System.Net;


Console.WriteLine("Hello, World!");

var stopSemaphore = new SemaphoreSlim(0, 1);
IServiceProvider serviceProvider = null;
var timeoutForStopLoadDataInMinutes = 5;

var builder = Host.CreateDefaultBuilder(args)
    .ConfigureServices((Action<HostBuilderContext, IServiceCollection>)((hostContext, services) =>
    {

        services.AddDbContext<ApiCrawlerDbContext>(db => db.UseSqlServer(hostContext.Configuration.GetConnectionString("DefaultConnection"), b =>
        {
            b.MigrationsAssembly("RickAndMortyApiCrawler.Data");
        }), ServiceLifetime.Singleton);

        services.AddAutoMapper(typeof(MappingProfile).Assembly);

        var rickAndMortyApiSettings = hostContext.Configuration.GetSection("RickAndMortyApiSettings").Get<RickAndMortyApiSettings>();

        timeoutForStopLoadDataInMinutes = hostContext.Configuration.GetValue<int>("AppSettings:TimeoutForStopLoadDataInMinutes");

        ConfigureHttpClientForRickAndMortyApi(services, rickAndMortyApiSettings);

        services.AddSingleton(rickAndMortyApiSettings!);

        services.AddTransient<IRickAndMortyApiFactory, RickAndMortyApiFactory>();
        services.AddTransient<IImportService, ImportService>();

        services.AddScoped<ILocationRepository, LocationRepository>();
        services.AddScoped<ICharacterRepository, CharacterRepository>();

        serviceProvider = services.BuildServiceProvider();

    }));

var app = builder.Build();

app.EnsureDatabaseUpdated();

var cancellationToken = new CancellationToken();

var monitorForManualRecordTask = Task.Run(MonitorForManualRecordAsync);

while (true)
{
    if (serviceProvider != null)
    {
        var importService = app.Services.GetRequiredService<IImportService>();

        await importService.ImportLocationsAsync(cancellationToken);

        await importService.ImportCharacterAsync(cancellationToken);

        if (await stopSemaphore.WaitAsync(TimeSpan.FromMinutes(timeoutForStopLoadDataInMinutes)))
        {
            Console.WriteLine("Manual record detected or operation stopped. Exiting loop.");
            break;
        }
    }
}


await monitorForManualRecordTask;


async Task MonitorForManualRecordAsync()
{
    while (true)
    {
        if (await CheckForManualRecordAsync())
        {
            stopSemaphore.Release();
            break;
        }

        // Check every minute for a manual record
        await Task.Delay(TimeSpan.FromMinutes(1));
    }
}

async Task<bool> CheckForManualRecordAsync()
{
    if (serviceProvider != null)
    {
        using (var scope = serviceProvider.CreateScope())
        {
            var importService = scope.ServiceProvider.GetRequiredService<IImportService>();
            return await importService.CheckForManualRecordAsync(cancellationToken);
        }
    }
    return false;
}


static void ConfigureHttpClientForRickAndMortyApi(IServiceCollection services, RickAndMortyApiSettings? rickAndMortyApiSettings)
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

