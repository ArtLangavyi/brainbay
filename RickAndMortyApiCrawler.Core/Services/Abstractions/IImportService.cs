
using RickAndMortyApiCrawler.Core.Clients.RickAndMortyApi.Models.Responses;
using RickAndMortyApiCrawler.Core.Models.ImportCharacter;

namespace RickAndMortyApiCrawler.Core.Services.Abstractions;
public interface IImportService
{
    Task ImportCharacterAsync(ImportFilter? importFilter, CancellationToken cancellationToken = default);
    Task ImportLocationsAsync(CancellationToken cancellationToken = default);
    Task<bool> CheckForManualRecordAsync(CancellationToken cancellationToken = default);
    Task<List<CharacterResponseResult>> LoadAndAddNewCharacterAsync(ImportFilter? importFilter, CancellationToken cancellationToken = default);
    Task SaveCharactersToDb(List<CharacterResponseResult> charactersList, CancellationToken cancellationToken = default);
}
