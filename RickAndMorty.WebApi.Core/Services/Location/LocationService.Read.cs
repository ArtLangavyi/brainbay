using RickAndMorty.WebApi.Core.Mappers;
using RickAndMorty.WebApi.Models.Responses.Locations;

namespace RickAndMorty.WebApi.Core.Services.Character;
public partial class LocationService
{
    public async Task<LocationResponse[]> GetAllPlanetsAsync(CancellationToken cancellationToken = default)
    {
        var locations = await locationRepository.GetAllPlanetsAsync(cancellationToken);

        return locations.Select(e => e.MapLocationEntityToLocationsResponse()).ToArray();
    }
}
