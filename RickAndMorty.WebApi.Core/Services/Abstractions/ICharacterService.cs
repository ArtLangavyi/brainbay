
using RickAndMorty.WebApi.Models.Requests.Characters;
using RickAndMorty.WebApi.Models.Responses.Characters;

namespace RickAndMorty.WebApi.Core.Services.Abstractions;
public interface ICharacterService
{
    Task<int> AddCharacterAsync(AddCharactersRequest request, CancellationToken cancellationToken = default);
    Task<CharacterResponse[]> GetAllCharactersAsync(string? planet = "", CancellationToken cancellationToken = default);
}

