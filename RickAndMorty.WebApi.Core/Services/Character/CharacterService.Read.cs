using RickAndMorty.WebApi.Core.Mappers;
using RickAndMorty.WebApi.Models.Responses.Characters;

namespace RickAndMorty.WebApi.Core.Services.Character;
public partial class CharacterService
{
    public async Task<CharacterResponse[]> GetAllCharactersAsync(string? planet = "", CancellationToken cancellationToken = default)
    {
        var character = await characterRepository.GetAllCharactersAsync(planet, cancellationToken);

        return character.Select(e => e.MapCharacterEntityToCharacterResponse()).ToArray();
    }
}
