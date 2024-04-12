
using RickAndMorty.WebApi.Models.Responses.Locations;

namespace RickAndMorty.WebApi.Core.Services.Abstractions;
public interface ILocationService
{
    Task<LocationResponse[]> GetAllPlanetsAsync(CancellationToken cancellationToken = default);
}

