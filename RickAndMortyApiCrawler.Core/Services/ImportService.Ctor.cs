using AutoMapper;

using RickAndMortyApiCrawler.Core.Clients;
using RickAndMortyApiCrawler.Core.Infrastructure;
using RickAndMortyApiCrawler.Core.Repositories;
using RickAndMortyApiCrawler.Core.Services.Abstractions;

namespace RickAndMortyApiCrawler.Core.Services;
public partial class ImportService(RickAndMortyApiSettings rickAndMortyApiSettings
    , ILocationRepository locationRepository
    , ICharacterRepository characterRepository
    , IMapper mapper
    , IHttpClientFactory clientFactory) : IImportService
{
    const int MaxRetries = 5;
}
