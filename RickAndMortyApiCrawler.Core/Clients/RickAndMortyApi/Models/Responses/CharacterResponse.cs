namespace RickAndMortyApiCrawler.Core.Clients.RickAndMortyApi.Models.Responses;
internal class CharacterResponse
{
    public ResponseInfo info { get; set; }
    public List<CharacterResponseResult> results { get; set; }

}
