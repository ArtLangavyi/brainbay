
namespace RickAndMorty.Web.Models;
public class LocationResponse
{
    public int ExternalId { get; set; }
    public string Name { get; set; }
    public string Type { get; set; }
    public string? Dimension { get; set; }

    public string[]? LinksToResidents { get; set; }

    public string? Url { get; set; }

    public DateTime? Created { get; set; }
}

