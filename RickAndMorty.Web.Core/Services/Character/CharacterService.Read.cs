

using RickAndMorty.Shared;
using RickAndMorty.Web.Models;

namespace RickAndMorty.Web.Core.Services;
public partial class CharacterService
{
    public async Task<CharacterResponse[]> GetAllCharactersAsync(string? planet = "", CancellationToken cancellationToken = default)
    {
        using var _httpClient = _rickAndMortyWebApiFactory.MakeHttpClient();

        var url = BuildUrl(planet);

        var charactersResponse = await _rickAndMortyWebApiFactory.SendAsync(_httpClient, url, HttpMethod.Get, null, cancellationToken);

        if (charactersResponse is not null)
        {
            var characters = await ConversionHelper.ConvertResponseToObjectAsync<CharacterResponse[]>(charactersResponse);
            return characters;
        }

        return [];

        string BuildUrl(string? planet)
        {
            var url = _rickAndMortyWebApiSettings.CharactersEndpoint;
            if (!string.IsNullOrWhiteSpace(planet))
            {
                url += $"?planet={planet}";
            }

            return url;
        }
    }
}
