
namespace RickAndMorty.Web.Models;
public class CharacterResponse
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string Status { get; set; }
    public string? Species { get; set; }

    public string? Type { get; set; }

    /// <summary>
    /// possible values ('Female', 'Male', 'Genderless' or 'unknown').
    /// </summary>
    public string? Gender { get; set; }

    public string? Planet { get; set; }

    public string? Image { get; set; }
    public string[]? Episode { get; set; }
    public string? Url { get; set; }
    public string? Created { get; set; }
}
