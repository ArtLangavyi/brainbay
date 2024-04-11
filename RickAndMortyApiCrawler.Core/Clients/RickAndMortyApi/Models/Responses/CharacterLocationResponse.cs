using RickAndMortyApiCrawler.Core.Clients.RickAndMortyApi.Models.Responses;

namespace RickAndMorty.Net.Api.Models.Dto;
public class CharacterLocationResponse : BaseResponse
{
    public ResponseInfo info { get; set; }
    public List<CharacterLocationResponseResult> results { get; set; }

}

public class CharacterLocationResponseResult
{
    public int id { get; set; }
    public string name { get; set; }
    public string type { get; set; }
    public string dimension { get; set; }
    public List<string> residents { get; set; }
    public string url { get; set; }
    public DateTime created { get; set; }
}
