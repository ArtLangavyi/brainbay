
using RickAndMorty.Net.Api.Models.Dto;

namespace RickAndMortyApiCrawler.Core.Repositories;
public interface ILocationRepository
{
    Task AddNewLocationsAsync(CharacterLocationDto[] locationDtos, CancellationToken cancellationToken = default);
    Task RemoveAllLocationsAsync(CancellationToken cancellationToken = default);
}

