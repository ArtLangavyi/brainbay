using RickAndMorty.Web.Models;

namespace RickAndMorty.Web.Mappers;
public static class ViewModelMapper
{
    public static CharacterViewModel MapCharacterToViewModel(this CharacterResponse character)
    {
        return new CharacterViewModel(character.id, character.name, character.status, character.planet);
    }

    public static CharactersListViewModel MapCharacterResponseBaseToCharactersListViewModel(this CharacterResponseBase characterResponseBase, string baseUrl)
    {
        var characters = characterResponseBase.characters.Select(e => e.MapCharacterToViewModel()).ToArray();

        var nextPageUrl = characterResponseBase.nextPageNumber.HasValue ? $"{baseUrl}?pageNumber={characterResponseBase.nextPageNumber}" : null;
        var previousPageUrl = characterResponseBase.previousPageNumber.HasValue ? $"{baseUrl}?pageNumber={characterResponseBase.previousPageNumber}" : null;

        return new CharactersListViewModel(nextPageUrl, previousPageUrl, characters);
    }


    public static AddCharacterRequest MapAddCharacterViewModelToAddCharacterRequest(this AddCharacterViewModel character)
    {
        return new AddCharacterRequest
        {
            Name = character.Name,
        };
    }
}
