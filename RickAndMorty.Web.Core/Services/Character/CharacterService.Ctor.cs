
using Microsoft.Extensions.Options;

using RickAndMorty.Web.Core.Clients;
using RickAndMorty.Web.Core.Settings;

namespace RickAndMorty.Web.Core.Services;
public partial class CharacterService : ICharacterService
{
    private readonly IRickAndMortyWebApiFactory _rickAndMortyWebApiFactory;
    private readonly RickAndMortyWebApiSettings _rickAndMortyWebApiSettings;
    public CharacterService(IRickAndMortyWebApiFactory rickAndMortyWebApiFactory, IOptions<RickAndMortyWebApiSettings> rickAndMortyWebApiSettings)
    {
        _rickAndMortyWebApiFactory = rickAndMortyWebApiFactory;
        _rickAndMortyWebApiSettings = rickAndMortyWebApiSettings.Value;
    }
}
