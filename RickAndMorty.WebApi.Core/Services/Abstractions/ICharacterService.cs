
using RickAndMorty.WebApi.Models.Requests.Characters;
using RickAndMorty.WebApi.Models.Responses.Characters;

namespace RickAndMorty.WebApi.Core.Services.Abstractions;
public interface ICharacterService
{
    Task<int> AddCharacterAsync(AddCharactersRequest request, CancellationToken cancellationToken = default);
    Task<CharacterResponseBase> GetAllCharactersAsync(string? planet = "", int pageNumber = 1, int pageSize = 10, string baseUrl = "", CancellationToken cancellationToken = default);
}

