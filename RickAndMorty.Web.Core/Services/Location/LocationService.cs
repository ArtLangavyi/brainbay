
using RickAndMorty.Shared;
using RickAndMorty.Web.Core.Clients;
using RickAndMorty.Web.Core.Settings;
using RickAndMorty.Web.Models;

namespace RickAndMorty.Web.Core.Services;
public partial class LocationService(IRickAndMortyWebApiFactory rickAndMortyWebApiFactory, RickAndMortyWebApiSettings rickAndMortyWebApiSettings) : ILocationService
{
    public async Task<LocationResponse[]> GetAllPlanetsAsync(CancellationToken cancellationToken = default)
    {
        using var _httpClient = rickAndMortyWebApiFactory.MakeHttpClient();
        var planetsResponse = await rickAndMortyWebApiFactory.SendAsync(_httpClient, rickAndMortyWebApiSettings.GetPlanetsEndpoint, cancellationToken);

        if (planetsResponse is not null)
        {

            var planets = await ConversionHelper.ConvertResponseToObjectAsync<LocationResponse[]>(planetsResponse);
            return planets;
        }

        return [];
    }
}
