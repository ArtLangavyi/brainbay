
namespace RickAndMortyApiCrawler.Domain.Abstractions;

public interface IBase<IdType>
{
    IdType Id { get; set; }
}
