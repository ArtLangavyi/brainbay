
namespace RickAndMortyApiCrawler.Core.Clients.RickAndMortyApi.Models.Responses;
public class CharacterLocationResponse : BaseResponse
{
    public ResponseInfo info { get; set; }
    public List<CharacterLocationResponseResult> results { get; set; }

}
