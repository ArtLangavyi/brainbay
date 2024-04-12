using RickAndMorty.WebApi.Data.Models;
using RickAndMorty.WebApi.Models.Requests.Characters;

namespace RickAndMorty.WebApi.Data.Repositories.Abstractions;
public interface ICharacterRepository
{
    Task<Character[]> GetAllCharactersAsync(string? planet = "", CancellationToken cancellationToken = default);
    Task<int> AddCharacterAsync(AddCharactersRequest request, CancellationToken cancellationToken = default);
}
