namespace RickAndMorty.Web.Core.Settings;
public class RickAndMortyWebApiSettings
{
    public string BaseUrl { get; set; } = null!;
    public string CharactersEndpoint { get; set; } = null!;
    public string GetPlanetsEndpoint { get; set; } = null!;
    public int HttpClientTimeoutSeconds { get; set; }
    public bool ProxyEnabled {get;set;}
    public string ProxyUri = null!;
}
