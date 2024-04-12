

using RickAndMorty.Web.Models;

namespace RickAndMorty.Web.Core.Services;
public interface ILocationService
{
    Task<LocationResponse[]> GetAllPlanetsAsync(CancellationToken cancellationToken = default);
}

