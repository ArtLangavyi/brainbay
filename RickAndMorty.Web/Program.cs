using RickAndMorty.Web.Core.Clients;
using RickAndMorty.Web.Models.Settings;

var builder = WebApplication.CreateBuilder(args);

var socketsHttpHandler = new SocketsHttpHandler()
{
    SslOptions = new System.Net.Security.SslClientAuthenticationOptions()
    {
        EnabledSslProtocols = System.Security.Authentication.SslProtocols.Tls12 | System.Security.Authentication.SslProtocols.Tls13,
    },
    MaxConnectionsPerServer = int.MaxValue,
    EnableMultipleHttp2Connections = true
};

var RickAndMortyWebApiSettings = builder.Configuration.GetSection("RickAndMortyWebApiSettings").Get<RickAndMortyWebApiSettings>();

builder.Services.AddHttpClient(RickAndMortyWebApiFactory.ClientName, o =>
{
    o.Timeout = TimeSpan.FromSeconds(RickAndMortyWebApiSettings.HttpClientTimeoutSeconds);
    o.BaseAddress = new Uri(RickAndMortyWebApiSettings.BaseUrl);
}).ConfigurePrimaryHttpMessageHandler(() => socketsHttpHandler);

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

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
