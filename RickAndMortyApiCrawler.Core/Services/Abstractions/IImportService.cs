
using RickAndMortyApiCrawler.Core.Models.ImportCharacter;

namespace RickAndMortyApiCrawler.Core.Services.Abstractions;
public interface IImportService
{
    Task ImportCharacterAsync(ImportFilter? importFilter, CancellationToken cancellationToken = default);
    Task ImportLocationsAsync(CancellationToken cancellationToken = default);
    Task<bool> CheckForManualRecordAsync(CancellationToken cancellationToken = default);
}
