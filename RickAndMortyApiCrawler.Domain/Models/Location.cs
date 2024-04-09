
namespace RickAndMortyApiCrawler.Domain.Models;
public class Location : BaseEntity
{
    [Column(TypeName = "nvarchar(255)")]
    public required string Name { get; set; }
    [Column(TypeName = "nvarchar(255)")]
    public required string Type { get; set; }
    [Column(TypeName = "nvarchar(255)")]
    public string? Dimension { get; set; }

    public string[]? LinksToResidents { get; set; }

    [Column(TypeName = "nvarchar(2550)")]
    public string? Url { get; set; }

    public DateTime? Created { get; set; }
    public virtual ICollection<Character> Characters { get; set; }
}
