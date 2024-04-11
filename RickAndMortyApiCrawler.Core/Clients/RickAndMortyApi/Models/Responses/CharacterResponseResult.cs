namespace RickAndMortyApiCrawler.Core.Clients.RickAndMortyApi.Models.Responses;
internal class CharacterResponseResult
{
    public int id { get; set; }
    public string name { get; set; }
    public string? status { get; set; }
    public string? species { get; set; }

    public string? type { get; set; }

    /// <summary>
    /// possible values ('Female', 'Male', 'Genderless' or 'unknown').
    /// </summary>
    public string? gender { get; set; }

    public CharacterLocationResponseResult? location { get; set; }

    public string? image { get; set; }
    public string[]? episode { get; set; }
    public string? url { get; set; }
    public string? created { get; set; }
}