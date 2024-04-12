namespace RickAndMorty.WebApi.Data.Models;

public partial class Character
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public int Status { get; set; }

    public string? Species { get; set; }

    public string? Type { get; set; }

    public int Gender { get; set; }

    public int? LocationId { get; set; }

    public string? Image { get; set; }

    public string? LinksToEpisodeJson { get; set; }

    public string? Url { get; set; }

    public DateTime? Created { get; set; }

    public int ExternalId { get; set; }

    public bool IsAddedManual { get; set; }

    public virtual Location? Location { get; set; }
}
