using Microsoft.EntityFrameworkCore;

using RickAndMorty.WebApi.Data.Context;
using RickAndMorty.WebApi.Data.Mappers;
using RickAndMorty.WebApi.Data.Models;
using RickAndMorty.WebApi.Data.Repositories.Abstractions;
using RickAndMorty.WebApi.Models.Requests.Characters;

using System.Xml;

namespace RickAndMorty.WebApi.Data.Repositories;
public class CharacterRepository(RickAndMortyContext context) : ICharacterRepository
{
    const string PlanetLocationType = "Planet";
    private readonly RickAndMortyContext _context = context;

    public async Task<(Character[] listOfCharacters, int totalPages)> GetAllCharactersAsync(string? planet = "", int pageNumber = 1, int pageSize = 10, CancellationToken cancellationToken = default)
    {
        IQueryable<Character> query = _context.Characters.AsNoTracking()
                                            .Include(e => e.Location);

        if (!string.IsNullOrEmpty(planet))
        {
            query = query
                .Where(e => e.Location != null && e.Location.Type == PlanetLocationType && e.Location.Name == planet);
        }

        var skip = (pageNumber - 1) * pageSize;

        var result = await query.Skip(skip)
                             .Take(pageSize).ToArrayAsync(cancellationToken);

        var totalCount = await query.CountAsync();
        var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

        return (result, totalPages);
    }

    public async Task<int> AddCharacterAsync(AddCharactersRequest request, CancellationToken cancellationToken = default)
    {
        try
        {
            var entity = request.MapToCharacter(isAddedManual: true);

            await _context.Characters.AddAsync(entity, cancellationToken);

            await _context.SaveChangesAsync(cancellationToken);

            return entity.Id;
        }
        catch(Exception ex)
        {
            throw new Exception("Error while adding character", ex);
        }
    }
}
