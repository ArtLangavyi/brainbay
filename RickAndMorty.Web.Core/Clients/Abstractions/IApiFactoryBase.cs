
namespace RickAndMorty.Web.Core.Clients;
public interface IApiFactoryBase
{
    HttpClient MakeHttpClient(string? clientName = null);
    Task<HttpResponseMessage?> SendAsync(HttpClient _httpClient, string url, HttpMethod method, HttpContent? content = null, CancellationToken cancellationToken = default);
}
