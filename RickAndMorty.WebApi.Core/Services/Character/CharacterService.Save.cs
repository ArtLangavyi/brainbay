﻿using RickAndMorty.WebApi.Models.Requests.Characters;

namespace RickAndMorty.WebApi.Core.Services.Character;
public partial class CharacterService
{
    public async Task<int> AddCharacterAsync(AddCharactersRequest request, CancellationToken cancellationToken = default)
    {
        return await characterRepository.AddCharacterAsync(request, cancellationToken);
    }
}
