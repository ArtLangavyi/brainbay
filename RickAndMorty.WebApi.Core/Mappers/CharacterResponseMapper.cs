using RickAndMorty.WebApi.Data.Models;
using RickAndMorty.WebApi.Models.Responses.Characters;


namespace RickAndMorty.WebApi.Core.Mappers;
public static class CharacterResponseMapper
{
    public static CharacterResponse MapCharacterEntityToCharacterResponse(this Character character)
    {
        return new CharacterResponse
        {
            Id = character.ExternalId,
            Name = character.Name,
            Species = character.Species,
            Type = character.Type,
            Url = character.Url,
            Image = character.Image,
            Planet = (character.Location?.Type == "Planet") ? character.Location?.Name : null,
            Status = ((CharacterStatus)character.Status).ToString(),
        };
    }
}

