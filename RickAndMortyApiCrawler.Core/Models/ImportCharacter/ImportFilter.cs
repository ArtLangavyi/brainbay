using RickAndMortyApiCrawler.Core.Models.ImportCharacter.Enums;

namespace RickAndMortyApiCrawler.Core.Models.ImportCharacter;
public class ImportFilter
{
    public string? Name { get; set; }
    public ImportFilterStatusEnum? Status { get; set; }
    public string? Species { get; set; }
    public string? Type { get; set; }
}
