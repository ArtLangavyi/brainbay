
namespace RickAndMortyApiCrawler.Core.Infrastracture;
public record class RickAndMortyApiSettings(string BaseUrl, string CharactersEndpoint, string LocationsEndpoint, int HttpClientTimeoutSeconds, bool ProxyEnabled, string ProxyUri);
