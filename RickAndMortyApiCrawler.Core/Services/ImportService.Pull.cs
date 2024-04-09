using RickAndMorty.Net.Api.Models.Dto;

using RickAndMortyApiCrawler.Core.Services.Abstractions;
using RickAndMortyApiCrawler.Core.Helpers;

namespace RickAndMortyApiCrawler.Core.Services;
public partial class ImportService : IImportService
{
    public async Task PullLocationsAsync(CancellationToken cancellationToken = default)
    {
        using var _httpClient = rickAndMortyApiFactory.MakeHttpClient();
        var locations = await SendAsync(_httpClient, rickAndMortyApiSettings.LocationsEndpoint, cancellationToken);

        if (locations is not null)
        {
            var locationsResponse = await ConversionHelper.ConvertResponseToObjectAsync<CharacterLocationResponse[]>(locations);

            if (locationsResponse is not null)
            {
                var locationDtos = locationsResponse.Select(obj => mapper.Map<CharacterLocationDto>(obj)).ToArray();

                await locationRepository.UpdateLocationsListAsync(locationDtos);
            }
        }
    }

    private async Task<HttpResponseMessage?> SendAsync(HttpClient _httpClient, string url, CancellationToken cancellationToken)
    {
        HttpResponseMessage? response = default;
        for (int i = 0; i < MaxRetries; i++)
        {
            response = await _httpClient.GetAsync(url, cancellationToken);

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

        return response;
    }
}