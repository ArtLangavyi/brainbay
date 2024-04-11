using RickAndMorty.Net.Api.Models.Dto;

using RickAndMortyApiCrawler.Core.Services.Abstractions;
using RickAndMortyApiCrawler.Core.Helpers;
using RickAndMortyApiCrawler.Core.Clients.RickAndMortyApi.Models.Responses;

namespace RickAndMortyApiCrawler.Core.Services;
public partial class ImportService : IImportService
{
    private static SemaphoreSlim semaphore = new SemaphoreSlim(1, 1);

    public async Task ImportCharacterAsync(CancellationToken cancellationToken = default)
    {
    }

    public async Task ImportLocationsAsync(CancellationToken cancellationToken = default)
    {
        await locationRepository.RemoveAllLocationsAsync(cancellationToken);

        await LoadAndAddNewLocationsAsync(cancellationToken);
    }

    public async Task LoadAndAddNewLocationsAsync(CancellationToken cancellationToken = default)
    {
        using var _httpClient = rickAndMortyApiFactory.MakeHttpClient();
        var url = rickAndMortyApiSettings.LocationsEndpoint;
        while (!string.IsNullOrEmpty(url))
        {
            var locations = await SendAsync(_httpClient, url.Replace(rickAndMortyApiSettings.BaseUrl, ""), cancellationToken);
            if (locations is not null)
            {
                var locationsResponse = await ConversionHelper.ConvertResponseToObjectAsync<CharacterLocationResponse>(locations);

                if (locationsResponse is not null)
                {
                    var locationDtos = locationsResponse.results.Select(obj => mapper.Map<CharacterLocationDto>(obj)).ToArray();

                    await locationRepository.AddNewLocationsAsync(locationDtos, cancellationToken);

                    url = locationsResponse.info.next;
                }
                else
                {
                    break;
                }

            }
            else
            {
                break;
            }
        }
    }

    private async Task<HttpResponseMessage?> SendAsync(HttpClient _httpClient, string url, CancellationToken cancellationToken)
    {
        for (int i = 0; i < MaxRetries; i++)
        {
            HttpResponseMessage? response = await _httpClient.GetAsync(url, cancellationToken);

            if (response.StatusCode == System.Net.HttpStatusCode.TooManyRequests)
            {
                var sleepTimer = 10;

                if (response.Headers.Contains("Retry-After"))
                    sleepTimer = int.Parse(response.Headers.GetValues("Retry-After").First());

                Thread.Sleep(sleepTimer * 1000);

                await SendAsync(_httpClient, url, cancellationToken);
            }

            if (response.IsSuccessStatusCode)
            {
                return response;
            }
        }

        return default;
    }
}