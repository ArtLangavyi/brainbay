using System.ComponentModel.DataAnnotations.Schema;

namespace RickAndMorty.Net.Api.Models.Dto;
public class CharacterLocationDto
{
    public int ExternalId { get; set; }
    public string? Name { get; set; }
    public string Type { get; set; }
    public string? Dimension { get; set; }

    public string[]? LinksToResidents { get; set; }

    public string? Url { get; set; }

    public DateTime? Created { get; set; }
}
