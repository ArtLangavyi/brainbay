using Microsoft.Extensions.Configuration;

namespace RickAndMortyApiCrawler.Core.Clients;
public abstract class ApiFactoryBase(string clientName, IHttpClientFactory clientFactory, IConfiguration config) : IApiFactoryBase
{
    private readonly string _clientName = clientName;
    private IConfiguration _config { get; init; } = config;
    private readonly IHttpClientFactory _clientFactory = clientFactory;

    public HttpClient MakeHttpClient(string? clientName = null)
    {
        return _clientFactory.CreateClient(clientName ?? _clientName);
    }
}