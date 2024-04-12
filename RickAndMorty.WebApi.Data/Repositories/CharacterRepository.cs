using Microsoft.EntityFrameworkCore;

using RickAndMorty.WebApi.Data.Context;
using RickAndMorty.WebApi.Data.Mappers;
using RickAndMorty.WebApi.Data.Models;
using RickAndMorty.WebApi.Data.Repositories.Abstractions;
using RickAndMorty.WebApi.Models.Requests.Characters;

namespace RickAndMorty.WebApi.Data.Repositories;
public class CharacterRepository(RickAndMortyContext context) : ICharacterRepository
{
    const string PlanetLocationType = "Planet";
    private readonly RickAndMortyContext _context = context;

    public async Task<Character[]> GetAllCharactersAsync(string? planet = "", CancellationToken cancellationToken = default)
    {
        IQueryable<Character> query = _context.Characters.AsNoTracking()
                                            .Include(e => e.Location);

        if (!string.IsNullOrEmpty(planet))
        {
            query = query
                .Where(e => e.Location.Type == PlanetLocationType && e.Location.Name == planet);
        }

        var result = await query.ToArrayAsync(cancellationToken);

        return result;
    }

    public async Task<int> AddCharacterAsync(AddCharactersRequest request, CancellationToken cancellationToken = default)
    {
        var entity = request.MapToCharacter();

        await _context.Characters.AddAsync(entity, cancellationToken);

        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}
