
using RickAndMorty.Web.Models;

namespace RickAndMorty.Web.Core.Services;
public interface ICharacterService
{
    Task<int> AddCharacterAsync(AddCharacterRequest request, CancellationToken cancellationToken = default);
    Task<CharacterResponse[]> GetAllCharactersAsync(string? planet = "", CancellationToken cancellationToken = default);
}

