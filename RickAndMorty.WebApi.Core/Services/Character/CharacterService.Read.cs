using RickAndMorty.WebApi.Core.Mappers;
using RickAndMorty.WebApi.Models.Responses.Characters;

namespace RickAndMorty.WebApi.Core.Services.Character;
public partial class CharacterService
{
    public async Task<CharacterResponseBase> GetAllCharactersAsync(string? planet = "", int pageNumber = 1, int pageSize = 10, CancellationToken cancellationToken = default)
    {
        var (listOfCharacters, totalPages) = await characterRepository.GetAllCharactersAsync(planet, pageNumber, pageSize, cancellationToken);

        int? nextPageNumber = null;
        int? previousPageNumber = null;
        if (pageNumber < totalPages)
        {
            nextPageNumber = pageNumber + 1;
        }

        if (pageNumber < totalPages && pageNumber > 1)
        {
            previousPageNumber = pageNumber - 1;
        }

        return new CharacterResponseBase()
        {
            Characters = listOfCharacters.Select(e => e.MapCharacterEntityToCharacterResponse()).ToArray(),
            NextPageNumber = nextPageNumber,
            PreviousPageNumber = previousPageNumber
        };
    }
}
