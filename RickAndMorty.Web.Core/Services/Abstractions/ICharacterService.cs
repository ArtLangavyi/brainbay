
using RickAndMorty.Web.Models;

namespace RickAndMorty.Web.Core.Services;
public interface ICharacterService
{
    Task<int> AddCharacterAsync(AddCharacterRequest request, CancellationToken cancellationToken = default);
    Task<CharacterResponseBase?> GetAllCharactersAsync(int pageNumber = 1, string? planet = "", CancellationToken cancellationToken = default);
}

