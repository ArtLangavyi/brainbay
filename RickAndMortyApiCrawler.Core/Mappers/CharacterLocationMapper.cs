using RickAndMorty.Net.Api.Models.Dto;

using RickAndMortyApiCrawler.Domain.Models;


namespace RickAndMortyApiCrawler.Core.Mappers;
public static class CharacterLocationMapper
{

    public static Location MapToLocation(this CharacterLocationDto model) => new()
    {
        ExternalId = model.ExternalId,
        Name = model.Name,
        Type = model.Type,
        Dimension = model.Dimension,
        LinksToResidents = model.LinksToResidents,
        Url = model.Url,
        Created = model.Created,
    };
}
