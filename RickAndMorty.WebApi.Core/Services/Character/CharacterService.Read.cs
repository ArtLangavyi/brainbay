using RickAndMorty.WebApi.Core.Mappers;
using RickAndMorty.WebApi.Models.Responses.Characters;

namespace RickAndMorty.WebApi.Core.Services.Character;
public partial class CharacterService
{
    public async Task<CharacterResponseBase> GetAllCharactersAsync(string? planet = "", int pageNumber = 1, int pageSize = 10, string baseUrl = "", CancellationToken cancellationToken = default)
    {
        var (listOfCharacters, totalPages) = await characterRepository.GetAllCharactersAsync(planet, pageNumber, pageSize, cancellationToken);

        string nextPageUrl = null;
        string previousPageUrl = null;
        if (pageNumber < totalPages)
        {
            nextPageUrl = $"{baseUrl}?pageNumber={pageNumber + 1}";
        }

        if (pageNumber < totalPages && pageNumber > 1)
        {
            previousPageUrl = $"{baseUrl}?pageNumber={pageNumber + 1}";
        }

        return new CharacterResponseBase()
        {
            Characters = listOfCharacters.Select(e => e.MapCharacterEntityToCharacterResponse()).ToArray(),
            NextPageUrl = nextPageUrl,
            PreviousPageUrl = previousPageUrl
        };
    }
}
