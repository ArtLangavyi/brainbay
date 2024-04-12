
using Microsoft.Extensions.Options;

using RickAndMorty.Shared;
using RickAndMorty.Web.Core.Clients;
using RickAndMorty.Web.Core.Settings;
using RickAndMorty.Web.Models;

namespace RickAndMorty.Web.Core.Services;
public partial class LocationService : ILocationService
{
    private readonly IRickAndMortyWebApiFactory _rickAndMortyWebApiFactory;
    private readonly RickAndMortyWebApiSettings _rickAndMortyWebApiSettings;
    public LocationService(IRickAndMortyWebApiFactory rickAndMortyWebApiFactory, IOptions<RickAndMortyWebApiSettings> rickAndMortyWebApiSettings)
    {
        _rickAndMortyWebApiFactory = rickAndMortyWebApiFactory;
        _rickAndMortyWebApiSettings = rickAndMortyWebApiSettings.Value;
    }
    public async Task<LocationResponse[]> GetAllPlanetsAsync(CancellationToken cancellationToken = default)
    {
        using var _httpClient = _rickAndMortyWebApiFactory.MakeHttpClient();
        var planetsResponse = await _rickAndMortyWebApiFactory.SendAsync(_httpClient, _rickAndMortyWebApiSettings.GetPlanetsEndpoint, cancellationToken);

        if (planetsResponse is not null)
        {

            var planets = await ConversionHelper.ConvertResponseToObjectAsync<LocationResponse[]>(planetsResponse);
            return planets;
        }

        return [];
    }
}
