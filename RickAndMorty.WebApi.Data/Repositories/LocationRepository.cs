using Microsoft.EntityFrameworkCore;

using RickAndMorty.WebApi.Data.Context;
using RickAndMorty.WebApi.Data.Models;
using RickAndMorty.WebApi.Data.Repositories.Abstractions;

namespace RickAndMorty.WebApi.Data.Repositories;
public class LocationRepository(RickAndMortyContext context) : ILocationRepository
{
    private readonly RickAndMortyContext _context = context;

    public async Task<Location[]> GetAllPlanetsAsync(CancellationToken cancellationToken = default)
    {
        var result = await _context.Locations.AsNoTracking()
            .Where(e => e.Type == "Planet")
            .OrderBy(e => e.Name)
            .ToArrayAsync(cancellationToken);

        return result;
    }
}