using Microsoft.EntityFrameworkCore;

using RickAndMorty.Net.Api.Models.Dto;

using RickAndMortyApiCrawler.Core.Mappers;
using RickAndMortyApiCrawler.Data.Context;

namespace RickAndMortyApiCrawler.Core.Repositories;
public class CharacterRepository(ApiCrawlerDbContext context) : ICharacterRepository
{
    private readonly ApiCrawlerDbContext _context = context;

    public async Task RemoveAllCharacterAsync(CancellationToken cancellationToken = default)
    {
        using var transaction = _context.Database.BeginTransaction(System.Data.IsolationLevel.ReadCommitted);
        try
        {
            await _context.Characters.ExecuteDeleteAsync(cancellationToken);

            await transaction.CommitAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync(cancellationToken);
            throw;
        }
    }

    public async Task AddNewCharactersAsync(CharacterDto[] characterDto, CancellationToken cancellationToken = default)
    {
        using var transaction = _context.Database.BeginTransaction(System.Data.IsolationLevel.ReadCommitted);
        try
        {
            var locations = await _context.Locations.AsNoTracking().ToDictionaryAsync(key => key.Name, value => value.Id, cancellationToken);

            var newEntities = characterDto
                .Select(obj => obj.MapToCharacter(locations))
                .ToList();

            if (newEntities.Count != 0)
            {
                _context.Characters.AddRange(newEntities);

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

    public async Task<bool> CheckForManualRecordAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Characters.AnyAsync(e => e.IsAddedManual, cancellationToken);
    }
}
