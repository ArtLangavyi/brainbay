﻿namespace RickAndMorty.Net.Api.Models.Dto;
public class CharacterDto
{
    public int ExternalId { get; set; }
    public string Name { get; set; }
    public string? Status { get; set; }
    public string? Species { get; set; }

    public string? Type { get; set; }

    /// <summary>
    /// possible values ('Female', 'Male', 'Genderless' or 'unknown').
    /// </summary>
    public string? Gender { get; set; }

    public CharacterLocationDto? Location { get; set; }

    public string? Image { get; set; }
    public string[]? Episode { get; set; }
    public string? Url { get; set; }
    public string? Created { get; set; }
}
