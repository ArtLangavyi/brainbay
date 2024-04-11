using RickAndMortyApiCrawler.Domain.Abstractions.Enums;

using System.ComponentModel.DataAnnotations;

namespace RickAndMortyApiCrawler.Domain.Models;
public class Character
{
    [Key]
    public int Id { get; set; }
    [Column(TypeName = "nvarchar(255)")]
    public string? Name { get; set; }
    public CharacterStatus Status { get; set; }

    [Column(TypeName = "nvarchar(255)")]
    public string? Species { get; set; }

    [Column(TypeName = "nvarchar(255)")]
    public string? Type { get; set; }

    public CharacterGender Gender { get; set; }
    public int? LocationId { get; set; }
    public virtual Location? Location { get; set; }

    [Column(TypeName = "nvarchar(2550)")]
    public string? Image { get; set; }
    public string[]? LinksToEpisode { get; set; }

    public string? Url { get; set; }
    public DateTime? Created { get; set; }
    public int ExternalId { get; set; }
    public bool IsAddedManual { get; set; }
}
