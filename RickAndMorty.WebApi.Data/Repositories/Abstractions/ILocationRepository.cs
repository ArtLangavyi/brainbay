using RickAndMorty.WebApi.Data.Models;

namespace RickAndMorty.WebApi.Data.Repositories.Abstractions;
public interface ILocationRepository
{
    Task<Location[]> GetAllPlanetsAsync(CancellationToken cancellationToken = default);
}
