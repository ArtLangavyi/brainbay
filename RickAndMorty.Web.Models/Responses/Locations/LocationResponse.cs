
namespace RickAndMorty.Web.Models;
public class LocationResponse
{
    public int externalId { get; set; }
    public string name { get; set; }
    public string type { get; set; }
    public string? dimension { get; set; }
    public string[]? linksToResidents { get; set; }
    public string? url { get; set; }
    public DateTime? created { get; set; }
}

