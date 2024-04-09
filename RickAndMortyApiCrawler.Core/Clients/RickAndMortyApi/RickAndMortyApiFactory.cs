using Microsoft.Extensions.Configuration;

namespace RickAndMortyApiCrawler.Core.Clients;
public class RickAndMortyApiFactory(IHttpClientFactory clientFactory, IConfiguration config) : ApiFactoryBase(ClientName, clientFactory, config), IRickAndMortyApiFactory
{
    public static string ClientName => "RickAndMortyApi-http-client";
}
