
namespace RickAndMortyApiCrawler.Domain.Models;
public class CharacterOrigin : BaseEntity
{
    [Column(TypeName = "nvarchar(255)")]
    public required string Name { get; set; }

    [Column(TypeName = "nvarchar(2550)")]
    public string? Uri { get; set; }

    public virtual ICollection<Character> Characters { get; set; }
}
