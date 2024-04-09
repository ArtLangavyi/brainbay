
using RickAndMorty.Net.Api.Models.Dto;

namespace RickAndMortyApiCrawler.Core.Repositories;
public interface ILocationRepository
{
    Task UpdateLocationsListAsync(CharacterLocationDto[] locationDtos);
}

