using RickAndMorty.Net.Api.Models.Dto;

using System;

namespace RickAndMorty.Net.Api.Models.Responses;
internal class CharacterResponse
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public string? Status { get; set; }
    public string? Species { get; set; }

    public string? Type { get; set; }

    /// <summary>
    /// possible values ('Female', 'Male', 'Genderless' or 'unknown').
    /// </summary>
    public string? Gender { get; set; }

    public CharacterLocationResponse? Location { get; set; }

    public CharacterOriginResponse? Origin { get; set; }

    public string? Image { get; set; }
    public string[]? Episode { get; set; }
    public string? Url { get; set; }
    public string? Created { get; set; }
}
