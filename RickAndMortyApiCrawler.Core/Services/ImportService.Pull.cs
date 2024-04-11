using RickAndMorty.Net.Api.Models.Dto;

using RickAndMortyApiCrawler.Core.Clients.RickAndMortyApi.Models.Responses;
using RickAndMortyApiCrawler.Core.Helpers;
using RickAndMortyApiCrawler.Core.Services.Abstractions;

using System.Threading;

namespace RickAndMortyApiCrawler.Core.Services;
public partial class ImportService : IImportService
{
    public async Task<bool> CheckForManualRecordAsync(CancellationToken cancellationToken = default)
    {
        return await characterRepository.CheckForManualRecordAsync(cancellationToken);
    }

    public async Task ImportCharacterAsync(CancellationToken cancellationToken = default)
    {
        var charactersList = await LoadAndAddNewCharacterAsync(cancellationToken);
        
        await SaveCharactersToDb(charactersList, cancellationToken);
    }

    private async Task SaveCharactersToDb(List<CharacterResponseResult> charactersList, CancellationToken cancellationToken = default)
    {
        var charactersDTOs = charactersList.Select(obj => mapper.Map<CharacterDto>(obj)).ToArray();

        if (!await CheckForManualRecordAsync(cancellationToken))
        {
            await characterRepository.RemoveAllCharacterAsync(cancellationToken);

            await characterRepository.AddNewCharactersAsync(charactersDTOs, cancellationToken);
        }
    }

    public async Task ImportLocationsAsync(CancellationToken cancellationToken = default)
    {
        await locationRepository.RemoveAllLocationsAsync(cancellationToken);

        await LoadAndAddNewLocationsAsync(cancellationToken);
    }

    private async Task<List<CharacterResponseResult>> LoadAndAddNewCharacterAsync(CancellationToken cancellationToken = default)
    {
        using var _httpClient = rickAndMortyApiFactory.MakeHttpClient();
        var url = rickAndMortyApiSettings.CharactersEndpoint;
        var charactersList = new List<CharacterResponseResult>();

        while (!string.IsNullOrEmpty(url))
        {
            var characters = await SendAsync(_httpClient, url.Replace(rickAndMortyApiSettings.BaseUrl, ""), cancellationToken);
            if (characters is not null)
            {
                var charactersResponse = await ConversionHelper.ConvertResponseToObjectAsync<CharacterResponse>(characters);

                if (charactersResponse is not null)
                {
                    charactersList.AddRange(charactersResponse.results);

                    url = charactersResponse.info.next;
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

        return charactersList;
    }

    private async Task LoadAndAddNewLocationsAsync(CancellationToken cancellationToken = default)
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