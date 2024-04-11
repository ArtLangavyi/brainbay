
namespace RickAndMortyApiCrawler.Core.Services.Abstractions;
public interface IImportService
{
    Task ImportCharacterAsync(CancellationToken cancellationToken = default);
    Task ImportLocationsAsync(CancellationToken cancellationToken = default);
}
