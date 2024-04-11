using System.ComponentModel.DataAnnotations;

namespace RickAndMortyApiCrawler.Domain.Models;
public class Location
{
    [Key]
    public int Id { get; set; }
    public int ExternalId { get; set; }
    [Column(TypeName = "nvarchar(255)")]
    public string Name { get; set; }
    [Column(TypeName = "nvarchar(255)")]
    public string Type { get; set; }
    [Column(TypeName = "nvarchar(255)")]
    public string? Dimension { get; set; }

    public string[]? LinksToResidents { get; set; }

    [Column(TypeName = "nvarchar(2550)")]
    public string? Url { get; set; }

    public DateTime? Created { get; set; }
    public virtual ICollection<Character>? Characters { get; set; }
}
