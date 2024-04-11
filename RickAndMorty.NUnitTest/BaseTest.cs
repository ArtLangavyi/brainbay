using AutoMapper;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;

using Moq;

using RickAndMortyApiCrawler.Core.Clients;
using RickAndMortyApiCrawler.Core.Infrastructure;
using RickAndMortyApiCrawler.Core.Mappers;
using RickAndMortyApiCrawler.Core.Repositories;
using RickAndMortyApiCrawler.Core.Services;
using RickAndMortyApiCrawler.Data.Context;

namespace RickAndMorty.NUnitTest;

[TestFixture]
public class BaseTest
{
    private readonly MapperConfiguration mapperConfig = new(cfg => { cfg.AddProfile<MappingProfile>(); });

    protected DbContextOptions<ApiCrawlerDbContext> _options { get; set; }
    protected ApiCrawlerDbContext _dbContext { get; set; }
    protected ImportService _importService { get; set; }
    protected LocationRepository _locationRepository { get; set; }
    protected CharacterRepository _characterRepository { get; set; }
    protected IMapper _mapper { get; set; }
    protected RickAndMortyApiSettings _rickAndMortyApiSettings { get; set; }

    [SetUp]
    public void Setup()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", true);

        var config = builder.Build();

        _options = new DbContextOptionsBuilder<ApiCrawlerDbContext>()
                    .UseInMemoryDatabase(Guid.NewGuid().ToString())
                    .ConfigureWarnings(b => b.Ignore(InMemoryEventId.TransactionIgnoredWarning))
                    .Options;

        _dbContext = new ApiCrawlerDbContext(_options);
        _locationRepository = new LocationRepository(_dbContext);
        _characterRepository = new CharacterRepository(_dbContext);
        _mapper = mapperConfig.CreateMapper();
        _rickAndMortyApiSettings = config.GetSection("RickAndMortyApiSettings").Get<RickAndMortyApiSettings>();

        var httpClient = new HttpClient();
        httpClient.BaseAddress = new Uri(_rickAndMortyApiSettings.BaseUrl);
        httpClient.Timeout = TimeSpan.FromSeconds(_rickAndMortyApiSettings.HttpClientTimeoutSeconds);

        var clientFactoryMock = new Mock<IHttpClientFactory>();
        clientFactoryMock.Setup(c => c.CreateClient(It.IsAny<string>())).Returns(httpClient);

        var rickAndMortyApiFactory = new RickAndMortyApiFactory(clientFactoryMock.Object, config);

        _importService = new ImportService(rickAndMortyApiFactory, _rickAndMortyApiSettings, _locationRepository, _characterRepository, _mapper);
    }

    [TearDown]
    public void TearDown()
    {
        // Dispose of _dbContext here
        _dbContext?.Dispose();
    }
}
