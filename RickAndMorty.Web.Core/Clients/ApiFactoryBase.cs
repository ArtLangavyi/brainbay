using Microsoft.Extensions.Configuration;

namespace RickAndMorty.Web.Core.Clients;
public abstract class ApiFactoryBase(string clientName, IHttpClientFactory clientFactory, IConfiguration config) : IApiFactoryBase
{
    private readonly string _clientName = clientName;
    private IConfiguration _config { get; init; } = config;
    private readonly IHttpClientFactory _clientFactory = clientFactory;

    public HttpClient MakeHttpClient(string? clientName = null)
    {
        return _clientFactory.CreateClient(clientName ?? _clientName);
    }

    public async Task<HttpResponseMessage?> SendAsync(HttpClient _httpClient, string url, CancellationToken cancellationToken)
    {
        for (int i = 0; i < 5; i++)
        {
            HttpResponseMessage? response = await _httpClient.GetAsync(url, cancellationToken);

            if (response.StatusCode == System.Net.HttpStatusCode.TooManyRequests)
            {
                var sleepTimer = 10;

                if (response.Headers.Contains("Retry-After"))
                    sleepTimer = int.Parse(response.Headers.GetValues("Retry-After").First());

                Thread.Sleep(sleepTimer * 1000);

                await SendAsync(_httpClient, url, cancellationToken);
            }

            if (response.IsSuccessStatusCode)
            {
                return response;
            }
        }

        return default;
    }
}