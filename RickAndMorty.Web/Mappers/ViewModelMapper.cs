using RickAndMorty.Web.Models;

namespace RickAndMorty.Web.Mappers;
public static class ViewModelMapper
{
    public static CharacterViewModel MapCharacterToViewModel(this CharacterResponse character)
    {
        return new CharacterViewModel(character.id, character.name, character.status, character.planet);
    }

    public static AddCharacterRequest MapAddCharacterViewModelToAddCharacterRequest(this AddCharacterViewModel character)
    {
        return new AddCharacterRequest
        {
            Name = character.Name,
        };
    }
}
