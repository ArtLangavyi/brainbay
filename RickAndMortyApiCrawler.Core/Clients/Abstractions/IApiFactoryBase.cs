
namespace RickAndMortyApiCrawler.Core.Clients
{
    public interface IApiFactoryBase
    {
        HttpClient MakeHttpClient(string? clientName = null);
    }
}
