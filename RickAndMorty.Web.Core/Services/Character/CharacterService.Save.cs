

using RickAndMorty.Shared;
using RickAndMorty.Web.Models;
using RickAndMorty.WebApi.Models.Responses.Characters;

using System;

namespace RickAndMorty.Web.Core.Services;
public partial class CharacterService
{
    public async Task<int> AddCharacterAsync(AddCharacterRequest request, CancellationToken cancellationToken = default)
    {
        using var _httpClient = _rickAndMortyWebApiFactory.MakeHttpClient();
        var requestBody = ConversionHelper.CreateJsonHttpContent(request);
        var createCharacterResponse = await _rickAndMortyWebApiFactory.SendAsync(_httpClient, _rickAndMortyWebApiSettings.CharactersEndpoint, HttpMethod.Post, requestBody, cancellationToken);
        if (createCharacterResponse is not null)
        {
            var newCharacterId = await ConversionHelper.ConvertResponseToObjectAsync<int>(createCharacterResponse);

            return newCharacterId;
        }

        return 0;
    }
}
