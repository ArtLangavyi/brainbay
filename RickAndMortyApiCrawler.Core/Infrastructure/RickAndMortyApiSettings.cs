
namespace RickAndMortyApiCrawler.Core.Infrastructure;
public record class RickAndMortyApiSettings(string BaseUrl, string CharactersEndpoint, string LocationsEndpoint, int HttpClientTimeoutSeconds, bool ProxyEnabled, string ProxyUri);
