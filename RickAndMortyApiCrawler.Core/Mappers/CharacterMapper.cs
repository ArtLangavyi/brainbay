using RickAndMorty.Net.Api.Models.Dto;

using RickAndMortyApiCrawler.Domain.Abstractions.Enums;
using RickAndMortyApiCrawler.Domain.Models;


namespace RickAndMortyApiCrawler.Core.Mappers;
public static class CharacterMapper
{
    public static Character MapToCharacter(this CharacterDto model, Dictionary<string, int> locations)
    {
        return new Character()
        {
            Name = model.Name,
            Status = !string.IsNullOrEmpty(model.Status) ? (CharacterStatus)Enum.Parse(typeof(CharacterStatus), model.Status) : CharacterStatus.unknown,
            Species = model.Species,
            Type = model.Type,
            Gender = !string.IsNullOrEmpty(model.Gender) ? (CharacterGender)Enum.Parse(typeof(CharacterGender), model.Gender) : CharacterGender.unknown,
            Image = model.Image,
            LinksToEpisode = model.Episode,
            Url = model.Url,
            Created = !string.IsNullOrEmpty(model.Created) ? DateTime.Parse(model.Created) : null,
            ExternalId = model.ExternalId,
            LocationId = model.Location != null && locations.ContainsKey(model.Location.Name) ? locations[model.Location.Name] : (int?)null,
        };
    }

}
