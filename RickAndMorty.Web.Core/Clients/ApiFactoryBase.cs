using Microsoft.Extensions.Configuration;

using System.Net;
using System.Net.Http;

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

    public async Task<HttpResponseMessage?> SendAsync(HttpClient httpClient, string url, HttpMethod method, HttpContent? content = null, CancellationToken cancellationToken = default)
    {
        for (int i = 0; i < 5; i++)
        {
            HttpResponseMessage? response = null;

            if (method == HttpMethod.Get)
            {
                response = await httpClient.GetAsync(url, cancellationToken);
            }
            else if (method == HttpMethod.Post && content != null)
            {
                response = await httpClient.PostAsync(url, content, cancellationToken);
            }

            if (response?.StatusCode == HttpStatusCode.TooManyRequests)
            {
                var sleepTimer = 10;

                if (response.Headers.Contains("Retry-After"))
                    sleepTimer = int.Parse(response.Headers.GetValues("Retry-After").First());

                Thread.Sleep(sleepTimer * 1000);

                await SendAsync(httpClient, url, method, content, cancellationToken);
            }

            if (response.IsSuccessStatusCode)
            {
                return response;
            }
        }

        return default;
    }
}