namespace RickAndMorty.Web.Models;
public record CharactersListViewModel(string NextPageUrl, string PreviousPageUrl, CharacterViewModel[] Characters);
