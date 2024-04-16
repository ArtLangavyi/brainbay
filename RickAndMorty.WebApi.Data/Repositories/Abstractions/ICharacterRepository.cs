using RickAndMorty.WebApi.Data.Models;
using RickAndMorty.WebApi.Models.Requests.Characters;

namespace RickAndMorty.WebApi.Data.Repositories.Abstractions;
public interface ICharacterRepository
{
    Task<(Character[] listOfCharacters, int totalPages)> GetAllCharactersAsync(string? planet = "", int pageNumber = 1, int pageSize = 10, CancellationToken cancellationToken = default);
    Task<int> AddCharacterAsync(AddCharactersRequest request, CancellationToken cancellationToken = default);
}
