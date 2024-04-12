namespace RickAndMorty.Web.Models.Settings;
public record RickAndMortyWebApiSettings(string BaseUrl, string CharactersEndpoint, string GetPlanetsEndpoint, int HttpClientTimeoutSeconds, bool ProxyEnabled, string ProxyUri);
