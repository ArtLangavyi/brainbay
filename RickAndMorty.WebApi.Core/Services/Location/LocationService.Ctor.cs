using RickAndMorty.WebApi.Core.Services.Abstractions;
using RickAndMorty.WebApi.Data.Repositories.Abstractions;

namespace RickAndMorty.WebApi.Core.Services.Character;
public partial class LocationService(ILocationRepository locationRepository) : ILocationService
{

}
