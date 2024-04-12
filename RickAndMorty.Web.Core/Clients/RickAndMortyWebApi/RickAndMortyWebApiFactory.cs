using Microsoft.Extensions.Configuration;

namespace RickAndMorty.Web.Core.Clients;
public class RickAndMortyWebApiFactory(IHttpClientFactory clientFactory, IConfiguration config) : ApiFactoryBase(ClientName, clientFactory, config), IRickAndMortyWebApiFactory
{
    public static string ClientName => "RickAndMortyWebApi-http-client";
}
