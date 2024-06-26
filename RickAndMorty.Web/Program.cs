using Elastic.Apm.NetCoreAll;

using RickAndMorty.Shared.Services;
using RickAndMorty.Web.Core.Clients;
using RickAndMorty.Web.Core.Services;
using RickAndMorty.Web.Core.Settings;

using Serilog;
var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((hostingContext, loggerConfiguration) =>
{
    loggerConfiguration
        .ReadFrom.Configuration(hostingContext.Configuration)
        .Enrich.FromLogContext()
        .WriteTo.Console();
});

var socketsHttpHandler = new SocketsHttpHandler()
{
    SslOptions = new System.Net.Security.SslClientAuthenticationOptions()
    {
        EnabledSslProtocols = System.Security.Authentication.SslProtocols.Tls12 | System.Security.Authentication.SslProtocols.Tls13,
    },
    MaxConnectionsPerServer = int.MaxValue,
    EnableMultipleHttp2Connections = true
};

var rickAndMortyWebApiSettings = builder.Configuration.GetSection("RickAndMortyWebApiSettings").Get<RickAndMortyWebApiSettings>();

builder.Services.AddHttpClient(RickAndMortyWebApiFactory.ClientName, o =>
{
    o.Timeout = TimeSpan.FromSeconds(rickAndMortyWebApiSettings!.HttpClientTimeoutSeconds);
    o.BaseAddress = new Uri(rickAndMortyWebApiSettings.BaseUrl);
}).ConfigurePrimaryHttpMessageHandler(() => socketsHttpHandler);

builder.Services.Configure<RickAndMortyWebApiSettings>(builder.Configuration.GetSection("RickAndMortyWebApiSettings"));

builder.Services.AddTransient<IRickAndMortyWebApiFactory, RickAndMortyWebApiFactory>();
builder.Services.AddTransient<ILocationService, LocationService>();
builder.Services.AddTransient<ICharacterService, CharacterService>();

var logger = LogService.AddLogger(builder.Configuration, "RickAndMorty_Web");
Log.Logger = logger.CreateLogger();

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

if (builder.Configuration.GetSection("ElasticApm").GetValue<bool>("Enabled"))
{
    app.UseHttpsRedirection();

    app.UseAllElasticApm(builder.Configuration);
}


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
