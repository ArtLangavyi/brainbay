using Microsoft.EntityFrameworkCore;

using RickAndMorty.Net.Api.Models.Dto;

using RickAndMortyApiCrawler.Core.Mappers;
using RickAndMortyApiCrawler.Data.Context;

namespace RickAndMortyApiCrawler.Core.Repositories;
public class LocationRepository(ApiCrawlerDbContext context) : ILocationRepository
{
    private readonly ApiCrawlerDbContext _context = context;

    public async Task RemoveAllLocationsAsync(CancellationToken cancellationToken = default)
    {
        using var transaction = _context.Database.BeginTransaction(System.Data.IsolationLevel.ReadCommitted);
        try
        {
            await _context.Locations.ExecuteDeleteAsync(cancellationToken);

            await transaction.CommitAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync(cancellationToken);
            throw;
        }
    }

    public async Task AddNewLocationsAsync(CharacterLocationDto[] locationDtos, CancellationToken cancellationToken = default)
    {
        using var transaction = _context.Database.BeginTransaction(System.Data.IsolationLevel.ReadCommitted);
        try
        {
            var idsList = await _context.Locations.AsNoTracking().Select(obj => obj.ExternalId).ToListAsync(cancellationToken);

            var newEntities = locationDtos
                .Select(obj => obj.MapToLocation())
                .ToList();

            if (newEntities.Count != 0)
            {
                _context.Locations.AddRange(newEntities);

                await _context.SaveChangesAsync(cancellationToken);
            }

            await transaction.CommitAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync(cancellationToken);
            throw;
        }
    }
}
