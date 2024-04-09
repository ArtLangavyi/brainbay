using Microsoft.EntityFrameworkCore;

using RickAndMorty.Net.Api.Models.Dto;

using RickAndMortyApiCrawler.Data.Context;

namespace RickAndMortyApiCrawler.Core.Repositories;
public class LocationRepository : ILocationRepository
{
    private readonly ApiCrawlerDbContext _context;

    public LocationRepository(ApiCrawlerDbContext context) => _context = context;

    public async Task UpdateLocationsListAsync(CharacterLocationDto[] locationDtos)
    {
        await RemoveNotExistingLocationsAsync(locationDtos);
    }

    private async Task RemoveNotExistingLocationsAsync(IEnumerable<CharacterLocationDto> locationDtos)
    {
        using (var transaction = _context.Database.BeginTransaction(System.Data.IsolationLevel.ReadCommitted))
        {
            try
            {
                var namesInList = locationDtos.Select(obj => obj.Name).ToList();

                var entitiesToRemove = await _context.Locations
                    .Where(entity => !namesInList.Contains(entity.Name))
                    .ToListAsync();

                if(entitiesToRemove.Count != 0)
                {
                    _context.Locations.RemoveRange(entitiesToRemove);

                    await _context.SaveChangesAsync();
                }

                await transaction.CommitAsync();
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
    }
}
