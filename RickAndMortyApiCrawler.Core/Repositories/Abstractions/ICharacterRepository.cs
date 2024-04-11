using RickAndMorty.Net.Api.Models.Dto;

namespace RickAndMortyApiCrawler.Core.Repositories;
public interface ICharacterRepository
{
    Task RemoveAllCharacterAsync(CancellationToken cancellationToken = default);
    Task AddNewCharactersAsync(CharacterDto[] characterDto, CancellationToken cancellationToken = default);
    Task<bool> CheckForManualRecordAsync(CancellationToken cancellationToken = default);
}
