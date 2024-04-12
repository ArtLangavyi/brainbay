using RickAndMorty.Web.Models;

namespace RickAndMorty.Web.Mappers;
public static class ViewModelMapper
{
    public static CharacterViewModel MapCharacterToViewModel(this CharacterResponse character)
    {
        return new CharacterViewModel(character.Id, character.Name, character.Status, character.Planet);
    }
}
