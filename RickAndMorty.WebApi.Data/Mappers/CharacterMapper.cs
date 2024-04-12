using RickAndMorty.WebApi.Data.Models;
using RickAndMorty.WebApi.Models.Requests.Characters;

namespace RickAndMorty.WebApi.Data.Mappers;
public static class CharacterMapper
{
    public static Character MapToCharacter(this AddCharactersRequest request)
    {
        return new Character
        {
            Name = request.Name
        };
    }
}

