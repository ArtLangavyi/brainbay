namespace RickAndMorty.WebApi.Data.Models;

public partial class Location
{
    public int Id { get; set; }

    public int ExternalId { get; set; }

    public string Name { get; set; } = null!;

    public string Type { get; set; } = null!;

    public string? Dimension { get; set; }

    public string? LinksToResidentsJson { get; set; }

    public string? Url { get; set; }

    public DateTime? Created { get; set; }

    public virtual ICollection<Character> Characters { get; set; } = new List<Character>();
}
