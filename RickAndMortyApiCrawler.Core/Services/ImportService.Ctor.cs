using AutoMapper;

using RickAndMortyApiCrawler.Core.Clients;
using RickAndMortyApiCrawler.Core.Infrastructure;
using RickAndMortyApiCrawler.Core.Repositories;
using RickAndMortyApiCrawler.Core.Services.Abstractions;

namespace RickAndMortyApiCrawler.Core.Services;
public partial class ImportService(IRickAndMortyApiFactory rickAndMortyApiFactory
    , RickAndMortyApiSettings rickAndMortyApiSettings
    , ILocationRepository locationRepository
    , ICharacterRepository characterRepository
    , IMapper mapper) : IImportService
{
    const int MaxRetries = 5;
}
