using Elastic.Apm.Api;

using RickAndMorty.Net.Api.Models.Dto;

using RickAndMortyApiCrawler.Core.Clients.RickAndMortyApi.Models.Responses;
using RickAndMortyApiCrawler.Core.Helpers;
using RickAndMortyApiCrawler.Core.Models.ImportCharacter;
using RickAndMortyApiCrawler.Core.Services.Abstractions;

using System.Collections.Specialized;
using System.Text;
using System.Threading;
using System.Web;

namespace RickAndMortyApiCrawler.Core.Services;
public partial class ImportService : IImportService
{
    public async Task<bool> CheckForManualRecordAsync(CancellationToken cancellationToken = default)
    {
        return await characterRepository.CheckForManualRecordAsync(cancellationToken);
    }

    public async Task ImportCharacterAsync(ImportFilter? importFilter, CancellationToken cancellationToken = default)
    {
        var charactersList = await LoadAndAddNewCharacterAsync(importFilter, cancellationToken);
        
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

    private async Task<List<CharacterResponseResult>> LoadAndAddNewCharacterAsync(ImportFilter? importFilter, CancellationToken cancellationToken = default)
    {
        using var _httpClient = rickAndMortyApiFactory.MakeHttpClient();
        var charactersList = new List<CharacterResponseResult>();
        var url = rickAndMortyApiSettings.CharactersEndpoint;
        
        url += ApplyFilterToQueryParameters(importFilter, rickAndMortyApiSettings.CharactersEndpoint);

        while (!string.IsNullOrEmpty(url))
        {
            var characters = await SendAsync(_httpClient, url, cancellationToken);
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

    private static string ApplyFilterToQueryParameters(ImportFilter? filter, string url)
    {
        if (filter is null || string.IsNullOrEmpty(url))
        {
            return url;
        }

        if(!url.Contains("http"))
        {
            url = $"http://doesnmatter.domain/{url}";
        }

        var uriBuilder = new UriBuilder(url);
        var queryParameters = HttpUtility.ParseQueryString(uriBuilder.Query);

        if (!string.IsNullOrEmpty(filter.Name))
        {
            queryParameters["name"] = filter.Name;
        }

        if (filter.Status.HasValue)
        {
            queryParameters["status"] = filter.Status.ToString();
        }

        if (!string.IsNullOrEmpty(filter.Species))
        {
            queryParameters["species"] = filter.Species;
        }

        if (!string.IsNullOrEmpty(filter.Type))
        {
            queryParameters["type"] = filter.Type;
        }

        uriBuilder.Query = queryParameters.ToString();

        return uriBuilder.Query;
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