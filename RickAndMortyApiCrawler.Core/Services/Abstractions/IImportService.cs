
namespace RickAndMortyApiCrawler.Core.Services.Abstractions;
public interface IImportService
{
    Task PullLocationsAsync(CancellationToken cancellationToken = default);
}
