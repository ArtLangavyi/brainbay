namespace RickAndMorty.Web.Core.Settings;
public class RickAndMortyWebApiSettings
{
    public string BaseUrl { get; set; }
    public string CharactersEndpoint { get; set; }
    public string GetPlanetsEndpoint { get; set; }
    public int HttpClientTimeoutSeconds { get; set; }
    public bool ProxyEnabled { get; set; }
    public string ProxyUri { get; set; }
}
